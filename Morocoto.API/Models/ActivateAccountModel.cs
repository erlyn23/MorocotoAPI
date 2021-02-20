using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Morocoto.API.Models
{
    public class ActivateAccountModel
    {
        public string IdentificationDocument { get; set; }
        public string VerificationNumber { get; set; }
    }
}
