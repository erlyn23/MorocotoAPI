using System;
using System.Collections.Generic;
using System.Text;

namespace Morocoto.Infraestructure.Dtos.Requests
{
    public class TransactionsRequest
    {
        public string CustomerSenderAccountNumber { get; set; }
        public string CustomerRecieverAccountNumber { get; set; }
        public int CreditTransfered { get; set; }
        public string Pin { get; set; }


    }
}
