using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Morocoto.Infraestructure.Tools.Contracts
{
    public interface IEmailTools
    {
        Task<bool> SendEmailWithInfoAsync(string userEmail, string subject, string body);
    }
}
