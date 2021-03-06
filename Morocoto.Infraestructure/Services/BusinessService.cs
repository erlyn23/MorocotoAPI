using Morocoto.Domain.Contracts;
using Morocoto.Domain.Models;
using Morocoto.Infraestructure.Dtos.Requests;
using Morocoto.Infraestructure.Implementations;
using Morocoto.Infraestructure.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Morocoto.Infraestructure.Services
{
    public class BusinessService : IBusinessService
    {
        private readonly IAsyncUnitOfWork _unitOfWork;

        public BusinessService(IAsyncUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> SaveBusinessAsync(BusinessRequest businessRequest)
        {
            Business businessEntity = new Business()
            {
                PartnerId = businessRequest.PartnerId,
                BusinessTypeId = businessRequest.BusinessTypeId,
                BusinessNumber = businessRequest.BusinessNumber.Remove(9, 3),
                BusinessName = businessRequest.BusinessName,
                BusinessCreditAvailable = 0
            };
            businessEntity.BusinessAddresses = new List<BusinessAddress>();
            foreach(var businessAddress in businessRequest.BusinessAddresses)
            {
                BusinessAddress businessAddressEntity = new BusinessAddress()
                {
                    Country = businessAddress.Country,
                    City = businessAddress.City,
                    Province = businessAddress.Province,
                    Street1 = businessAddress.Street1,
                    Street2 = businessAddress.Street2
                };
                businessEntity.BusinessAddresses.Add(businessAddressEntity);
            }
            businessEntity.BusinessPhoneNumbers = new List<BusinessPhoneNumber>();
            foreach (var businessPhoneNumber in businessRequest.BusinessPhoneNumbers)
            {
                BusinessPhoneNumber businessPhoneNumberEntity = new BusinessPhoneNumber()
                {
                    PhoneNumber = businessPhoneNumber.PhoneNumber
                };
                businessEntity.BusinessPhoneNumbers.Add(businessPhoneNumberEntity);
            }

            await _unitOfWork.BusinessRepository.AddElementAsync(businessEntity);
            await _unitOfWork.BusinessAddressRepository.AddElementsAsync(businessEntity.BusinessAddresses);
            await _unitOfWork.BusinessPhoneNumberRepository.AddElementsAsync(businessEntity.BusinessPhoneNumbers);
            var saveResult = await _unitOfWork.CompleteAsync();
            if(saveResult > 0)
            {
                await _unitOfWork.DisposeAsync();
            }
            return saveResult;
        }
    }
}
