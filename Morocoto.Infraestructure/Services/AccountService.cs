using Microsoft.Extensions.Configuration;
using Morocoto.Domain.Contracts;
using Morocoto.Domain.Models;
using Morocoto.Infraestructure.Dtos.Requests;
using Morocoto.Infraestructure.Dtos.Responses;
using Morocoto.Infraestructure.Services.Contracts;
using Morocoto.Infraestructure.Tools;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
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
                AccountNumber = user.AccountNumber,
                UserPhone = user.UserPhone,
                OsPhone = user.OsPhone,
                IdentificationDocument = user.IdentificationDocument,
                Email = user.Email,
                BirthDate = user.BirthDate,
                UserPassword = Encryption.Encrypt(user.UserPassword),
                Pin = Encryption.Encrypt(user.Pin),
                SecurityAnswer = Encryption.Encrypt(user.SecurityAnswer),
                UserTypeId = user.UserTypeId,
                SecurityQuestionId = user.SecurityQuestionId,
                UserAddresses = user.UserAddresses,
                UserPhoneNumbers = user.UserPhoneNumbers
            };
            await _userRepository.AddElementAsync(userEntity);
            await _userAddressRepository.AddElementsAsync(userEntity.UserAddresses);
            await _userPhoneNumberRepository.AddElementsAsync(userEntity.UserPhoneNumbers);
            //TODO: Enviar correo de verificación.

            return await _unitOfWork.Complete();
        }

        public Task<bool> SendEmailConfirmationAsync()
        {
            throw new NotImplementedException();
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

        public async Task<UserResponse> RecoverPasswordAsync(int userId, int securityQuestionId, string securityQuestionAnswer)
        {
            //TODO: Enviar correo de verificación cambio contraseña.
            var user = await _userRepository.FirstOrDefaultAsync(u => u.Id == userId);
            if(user.SecurityQuestionId == securityQuestionId && user.SecurityAnswer == Encryption.Encrypt(securityQuestionAnswer))
            {

            }
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
        }
    }
}
