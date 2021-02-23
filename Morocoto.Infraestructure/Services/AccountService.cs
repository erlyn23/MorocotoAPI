using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Morocoto.Domain.Contracts;
using Morocoto.Domain.Models;
using Morocoto.Infraestructure.Dtos.Requests;
using Morocoto.Infraestructure.Dtos.Responses;
using Morocoto.Infraestructure.Services.Contracts;
using Morocoto.Infraestructure.Tools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Morocoto.Infraestructure.Tools.Contracts;

namespace Morocoto.Infraestructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAsyncUnitOfWork _unitOfWork;
        private readonly IAsyncUserRepository _userRepository;
        private readonly IAsyncUserAddressRepository _userAddressRepository;
        private readonly IAsyncUserPhoneNumberRepository _userPhoneNumberRepository;
        private readonly string filePath = $"{Environment.CurrentDirectory}/confirmation_account_data.json";
        private readonly IAccountTools _accountTools;
        private readonly IEmailTools _emailTools;
        public AccountService(
            IAsyncUnitOfWork unitOfWork,
            IAsyncUserRepository userRepository,
            IAsyncUserAddressRepository userAddressRepository,
            IAsyncUserPhoneNumberRepository userPhoneNumberRepository,
            IAccountTools accountTools,
            IEmailTools emailTools)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _userAddressRepository = userAddressRepository;
            _userPhoneNumberRepository = userPhoneNumberRepository;
            _accountTools = accountTools;
            _emailTools = emailTools;
        }

        public async Task<int> RegisterUserAsync(UserRequest user)
        {
            var userInDb = await _userRepository.FirstOrDefaultAsync(u => u.IdentificationDocument == user.IdentificationDocument);

            if (userInDb != null)
                throw new Exception("El usuario con esta céudula ya está registrado en el sistema, intente con uno nuevo");
            
            User userEntity = new User()
            {
                FullName = user.FullName,
                AccountNumber = user.AccountNumber.Remove(9, 3),
                UserPhone = user.UserPhone,
                OsPhone = user.OsPhone,
                IdentificationDocument = user.IdentificationDocument,
                Email = user.Email,
                BirthDate = user.BirthDate,
                UserPassword = Encryption.Encrypt(user.UserPassword),
                Pin = Encryption.Encrypt(user.Pin),
                SecurityAnswer = Encryption.Encrypt(user.SecurityAnswer),
                UserTypeId = user.UserTypeId,
                SecurityQuestionId = user.SecurityQuestionId
            };
            userEntity.UserAddresses = new List<UserAddress>();
            foreach (var userAddress in user.UserAddresses)
            {
                userEntity.UserAddresses.Add(new UserAddress()
                {
                    City = userAddress.City,
                    Country = userAddress.Country,
                    Street1 = userAddress.Street1,
                    Street2 = userAddress.Street2,
                    Province = userAddress.Province,
                    UserId = userEntity.Id
                });
            };
            userEntity.UserPhoneNumbers = new List<UserPhoneNumber>();
            foreach (var userPhoneNumber in user.UserPhoneNumbers)
            {
                userEntity.UserPhoneNumbers.Add(new UserPhoneNumber()
                {
                    PhoneNumber = userPhoneNumber.PhoneNumber,
                    UserId = userPhoneNumber.Id
                });
            }
            await _userRepository.AddElementAsync(userEntity);
            await _userAddressRepository.AddElementsAsync(userEntity.UserAddresses);
            await _userPhoneNumberRepository.AddElementsAsync(userEntity.UserPhoneNumbers);
            await _accountTools.SendEmailConfirmationAsync(userEntity.Email);
            return await _unitOfWork.CompleteAsync();
        }
     
        public async Task<bool> SetAccountActive(string identificationDocument, string verificationNumber)
        {
            var user = await _userRepository.FirstOrDefaultAsync(u => u.IdentificationDocument == identificationDocument);

            var confirmationDataSaved = _accountTools.GetConfirmationDataFromJsonFile();

            var confirmationDataWanted = confirmationDataSaved.Where(e => e.UserEmail == user.Email).FirstOrDefault();

            DateTime now = DateTime.UtcNow;
            TimeSpan difference = confirmationDataWanted.ExpireDate - now;
            int minutes = difference.Minutes;

            if(minutes <= 30) 
            {
                if(string.Equals(verificationNumber, confirmationDataWanted.RandomCode)) 
                {
                    user.Active = true; 
                    var actived = await _unitOfWork.CompleteAsync();

                    if (actived > 0) 
                    {
                        int toDelete = confirmationDataSaved.IndexOf(confirmationDataWanted);
                        confirmationDataSaved.RemoveAt(toDelete);
                        string jsonString = JsonConvert.SerializeObject(confirmationDataSaved);
                        File.WriteAllText(filePath, jsonString);
                        return true;
                    }
                    return false;
                }
                else 
                {
                    throw new Exception("El código de verificación no es válido");
                }
            }
            else
            {
                throw new Exception("El código de verificación ya expiró");
            }
        }

        public async Task<UserResponse> SignInAsync(string identificationDocument, string password, string userPhone, string osPhone)
        {
            var user = await _userRepository.FirstOrDefaultAsync(user => user.IdentificationDocument == identificationDocument && user.UserPassword == Encryption.Encrypt(password));

            if(user != null)
            {
                if(!user.UserPhone.Equals(userPhone) && !user.OsPhone.Equals(osPhone)) 
                {
                    string body = $"<b style='color: red;'>ALERTA: </b> se ha iniciado sesión con un nuevo dispositivo no reconocido, {userPhone} - {osPhone}, si no reconoces este inicio de sesión, por favor verifica tu cuenta.";
                    string subject = "Inicio de sesión no reconocido en tu cuenta de Morocoto App";

                    await _emailTools.SendEmailWithInfoAsync(user.Email, subject, body);
                }
                    
                if (!user.Active)
                    throw new Exception("El usuario está inactivo, por favor confirme su cuenta");
                
                return new UserResponse()
                {
                    Email = user.Email,
                    Token = _accountTools.BuildToken(user)
                };
            }
            else
                throw new Exception("Usuario o contraseña incorrecta, por favor verifique sus datos");
        }

        public async Task<int> RecoverPasswordAsync(ChangePasswordRequest changePasswordRequest)
        {
            //TODO: Enviar correo de verificación cambio contraseña.
            var user = await _userRepository.FirstOrDefaultAsync(u => u.IdentificationDocument == changePasswordRequest.IdentificationDocument);
            if(user != null)
            {
                if (user.SecurityQuestionId == changePasswordRequest.SecurityQuestionId && user.SecurityAnswer == Encryption.Encrypt(changePasswordRequest.SecurityQuestionAnswer))
                {
                    if (!string.Equals(changePasswordRequest.Password1, changePasswordRequest.Password2))
                        throw new Exception("Las contraseñas no coinciden");
                    if (string.Equals(Encryption.Encrypt(changePasswordRequest.Password1), user.UserPassword))
                        throw new Exception("La contraseña no puede ser igual a la anterior");

                    user.UserPassword = Encryption.Encrypt(changePasswordRequest.Password1);
                    return await _unitOfWork.CompleteAsync();
                }
                else
                {
                    throw new Exception("La respuesta de seguridad es incorrecta");
                }
            }
            return 0;
        }
    }
}
