using System;
using System.Collections.Generic;
using System.Text;

namespace Morocoto.Infraestructure.Dtos.Responses
{
    public class EmailVerificationResponse
    {
        public string UserEmail { get; set; }
        public string RandomCode { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
