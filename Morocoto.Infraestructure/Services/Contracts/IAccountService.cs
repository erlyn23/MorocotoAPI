using Morocoto.Domain.Models;
using Morocoto.Infraestructure.Dtos.Requests;
using Morocoto.Infraestructure.Dtos.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Morocoto.Infraestructure.Services.Contracts
{
    public interface IAccountService
    {
        Task<int> RegisterUserAsync(UserRequest user);
        Task<bool> SendEmailConfirmationAsync();
        Task<UserResponse> SignInAsync(string identificationDocument, string password);
    }
}
