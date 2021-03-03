using Morocoto.Domain.Models;
using Morocoto.Infraestructure.Dtos.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Morocoto.Infraestructure.Tools.Contracts
{
    public interface IAccountTools
    {
        void CreateJsonFileWithConfirmationData(EmailVerificationResponse emailVerificationResponse);
        List<EmailVerificationResponse> GetConfirmationDataFromJsonFile();
        Task<EmailVerificationResponse> SendEmailConfirmationAsync(string userEmail);
        string BuildToken(User user);
    }
}
