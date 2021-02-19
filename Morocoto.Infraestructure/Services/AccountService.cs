using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Morocoto.Domain.Contracts;
using Morocoto.Domain.Models;
using Morocoto.Infraestructure.Dtos.Requests;
using Morocoto.Infraestructure.Dtos.Responses;
using Morocoto.Infraestructure.Services.Contracts;
using Morocoto.Infraestructure.Tools;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Morocoto.Infraestructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAsyncUnitOfWork _unitOfWork;
        private readonly IAsyncUserRepository _userRepository;
        private readonly IAsyncUserAddressRepository _userAddressRepository;
        private readonly IAsyncUserPhoneNumberRepository _userPhoneNumberRepository;
        private readonly IConfiguration _configuration;
        private EmailVerificationResponse emailVerification = new EmailVerificationResponse();
        public AccountService(
            IAsyncUnitOfWork unitOfWork, 
            IAsyncUserRepository userRepository, 
            IAsyncUserAddressRepository userAddressRepository, 
            IAsyncUserPhoneNumberRepository userPhoneNumberRepository,
            IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _userAddressRepository = userAddressRepository;
            _userPhoneNumberRepository = userPhoneNumberRepository;
            _configuration = configuration;
        }

        public async Task<int> RegisterUserAsync(UserRequest user)
        {
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
            foreach(var userAddress in user.UserAddresses)
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
            foreach(var userPhoneNumber in user.UserPhoneNumbers)
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
            await SendEmailConfirmationAsync(userEntity.Email);
            return await _unitOfWork.CompleteAsync();
        }

        public async Task<EmailVerificationResponse> SendEmailConfirmationAsync(string userEmail)
        {
            emailVerification.RandomCode = BuildConfirmationCode();
            emailVerification.ExpireDate = DateTime.UtcNow.AddMinutes(30);
            string email = _configuration["EmailAccount:AppEmail"];
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);

            smtpClient.Credentials = new NetworkCredential(email, _configuration["EmailAccount:AppEmailPassword"]);

            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;
            MailMessage mailMessage = new MailMessage();

            mailMessage.From = new MailAddress(email, "Morocoto App");
            mailMessage.To.Add(new MailAddress(userEmail));
            mailMessage.Subject = "Confirmación de cuenta MorocotoApp";
            mailMessage.Body = $"Hola, el número de verificación de tu cuenta de morocoto es {emailVerification.RandomCode}, y expira en los próximos 30 minutos.";

            try
            {

                await smtpClient.SendMailAsync(mailMessage);
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return emailVerification;
        }

        private string BuildConfirmationCode()
        {
            Random random = new Random();
            string randomCode = string.Empty;
            StringBuilder stringBuilder = new StringBuilder();

            for (int index = 0; index < 6; index++)
            {
                string randomNumber = random.Next(0, 9).ToString();
                randomCode = stringBuilder.Append(randomNumber).ToString();
            }
            return randomCode;
        }
        public async Task<bool> SetAccountActive(string identificationDocument, string verificationNumber)
        {
            DateTime expirationDate = emailVerification.ExpireDate;
            DateTime now = DateTime.UtcNow;

            TimeSpan difference = expirationDate - now;

            int minutes = difference.Minutes;

            if(minutes <= 30)
            {
                if(string.Equals(verificationNumber, emailVerification.RandomCode))
                {
                    var user = await _userRepository.FirstOrDefaultAsync(u => u.IdentificationDocument == identificationDocument);

                    user.Active = true;
                    var actived = await _unitOfWork.CompleteAsync();
                    if (actived > 0) return true;

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

        public async Task<UserResponse> SignInAsync(string identificationDocument, string password)
        {
            var user = await _userRepository.FirstOrDefaultAsync(user => user.IdentificationDocument == identificationDocument && user.UserPassword == Encryption.Encrypt(password));

            if(user != null)
            {
                if (!user.Active)
                    throw new Exception("El usuario está inactivo, por favor confirme su cuenta");
                
                return new UserResponse()
                {
                    Email = user.Email,
                    Token = BuildToken(user)
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

                    user.UserPassword = changePasswordRequest.Password1;
                    return await _unitOfWork.CompleteAsync();
                }
            }
            return 0;
        }

        private string BuildToken(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKey = Encoding.ASCII.GetBytes(_configuration["MySecretKey"]);

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.IdentificationDocument),
                new Claim("AccountNumber", user.AccountNumber),
                new Claim("Id", user.Id.ToString())
            };

            var securityTokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = jwtTokenHandler.CreateToken(securityTokenDescriptor);

            return jwtTokenHandler.WriteToken(token);
        }
    }
}
