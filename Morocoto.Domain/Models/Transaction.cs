using System;
using System.Collections.Generic;

#nullable disable

namespace Morocoto.Domain.Models
{
    public partial class Transaction
    {
        public int Id { get; set; }
        public int CustomerSenderId { get; set; }
        public int CustomerRecieverId { get; set; }
        public DateTime DateCreation { get; set; }
        public decimal CreditTransfered { get; set; }
        public string ConfirmationNumber { get; set; }
        public int CustomerTaxesId { get; set; }

        public virtual Customer CustomerReciever { get; set; }
        public virtual Customer CustomerSender { get; set; }
        public virtual CustomerTaxis CustomerTaxes { get; set; }
    }
}
