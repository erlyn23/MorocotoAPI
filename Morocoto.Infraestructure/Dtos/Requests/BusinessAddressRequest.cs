using System;
using System.Collections.Generic;
using System.Text;

namespace Morocoto.Infraestructure.Dtos.Requests
{
    public class BusinessAddressRequest
    {
        public int Id { get; set; }
        public int BusinessId { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Street1 { get; set; }
        public string Street2 { get; set; }
    }
}
