using Morocoto.Infraestructure.Dtos.Requests;
using Morocoto.Infraestructure.Implementations;
using Morocoto.Infraestructure.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Morocoto.Infraestructure.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly UnitOfWork _unitOfWork;

        public CustomerService(UnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<string> ExecuteTransactionAsync(TransactionsRequest model)
        {
            try
            {
                var userSender = await _unitOfWork.UserRepository.FirstOrDefaultAsync(x => x.AccountNumber == model.CustomerSenderAccountNumber);
                var customerSender = await _unitOfWork.CustomerRepository.FirstOrDefaultAsync(x => x.Id == userSender.Id);

                var userReciever = await _unitOfWork.UserRepository.FirstOrDefaultAsync(x => x.AccountNumber == model.CustomerRecieverAccountNumber);
                var customerReciever = await _unitOfWork.CustomerRepository.FirstOrDefaultAsync(x => x.Id == userReciever.Id);

                bool isAbleForTransfer = await _unitOfWork.CustomerRepository.IsAbleForTransfer(model.CustomerSenderAccountNumber, model.CreditTransfered);
                if (isAbleForTransfer)
                {
                    string response = await _unitOfWork.TransactionRepository.Transaction(model.CustomerSenderAccountNumber, model.CustomerRecieverAccountNumber, model.CreditTransfered, model.Pin);
                    await _unitOfWork.CompleteAsync();
                    return response;
                }

                return "Credito insuficiente para realizar la transferencia";
            }
            catch (Exception ex)
            {
                return "Ha ocurrido un error" + ex.Message;
            }
            finally
            {
                await _unitOfWork.DisposeAsync();
            }
        }
    }
}
