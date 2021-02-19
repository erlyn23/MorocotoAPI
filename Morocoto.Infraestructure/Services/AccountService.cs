using Morocoto.Domain.Contracts;
using Morocoto.Domain.Models;
using Morocoto.Infraestructure.Dtos.Requests;
using Morocoto.Infraestructure.Dtos.Responses;
using Morocoto.Infraestructure.Services.Contracts;
using Morocoto.Infraestructure.Tools;
using System;
using System.Collections.Generic;
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
        public AccountService(
            IAsyncUnitOfWork unitOfWork, 
            IAsyncUserRepository userRepository, 
            IAsyncUserAddressRepository userAddressRepository, 
            IAsyncUserPhoneNumberRepository userPhoneNumberRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _userAddressRepository = userAddressRepository;
            _userPhoneNumberRepository = userPhoneNumberRepository;
        }

        public async Task<int> RegisterUserAsync(UserRequest user)
        {
            user.AccountNumber = AccountNumberGeneration.GenerateAccountNumber();
            User userEntity = new User()
            {
                FullName = user.FullName,
                AccountNumber = Encryptation.Encrypt(user.AccountNumber),
                UserPhone = user.UserPhone,
                OsPhone = user.OsPhone,
                IdentificationDocument = user.IdentificationDocument,
                Email = user.Email,
                BirthDate = user.BirthDate,
                UserPassword = Encryptation.Encrypt(user.UserPassword),
                Pin = Encryptation.Encrypt(user.Pin),
                SecurityAnswer = Encryptation.Encrypt(user.SecurityAnswer),
                UserTypeId = user.UserTypeId,
                SecurityQuestionId = user.SecurityQuestionId,
                UserAddresses = user.UserAddresses,
                UserPhoneNumbers = user.UserPhoneNumbers
            };
            await _userRepository.AddElementAsync(userEntity);
            await _userAddressRepository.AddElementsAsync(userEntity.UserAddresses);
            await _userPhoneNumberRepository.AddElementsAsync(userEntity.UserPhoneNumbers);

            if(await SendEmailConfirmationAsync())
                return await _unitOfWork.Complete();

            return 0;
        }

        public Task<bool> SendEmailConfirmationAsync()
        {
            throw new NotImplementedException();
        }
        public Task<UserResponse> SignInAsync(string identificationDocument, string password)
        {
            throw new NotImplementedException();
        }
        private string BuildToken(User user)
        {
            throw new NotImplementedException();
        }
    }
}
