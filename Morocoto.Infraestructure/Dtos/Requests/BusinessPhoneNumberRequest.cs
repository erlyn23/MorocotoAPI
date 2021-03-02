using System;
using System.Collections.Generic;
using System.Text;

namespace Morocoto.Infraestructure.Dtos.Requests
{
    public class BusinessPhoneNumberRequest
    {
        public int Id { get; set; }
        public int BusinessId { get; set; }
        public string PhoneNumber { get; set; }
    }
}
