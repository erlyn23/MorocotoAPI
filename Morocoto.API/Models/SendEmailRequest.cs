using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Morocoto.API.Models
{
    public class SendEmailRequest
    {
        public string IdentificationDocument { get; set; }
        public string UserEmail { get; set; }
        public string VerificationType { get; set; }
    }
}
