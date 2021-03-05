using System;
using System.Collections.Generic;
using System.Text;

namespace Morocoto.Infraestructure.Dtos.Responses
{
    public class BusinessResponse
    {
        public int PartnerId { get; set; }
        public int BusinessTypeId {get;set;}
        public string BusinessNumber { get; set; }
        public string BusinessName { get; set; }
        public decimal BusinessCreditAvailable { get; set; }
    }
}
