using System;
using System.Collections.Generic;

#nullable disable

namespace Morocoto.Domain.Models
{
    public partial class BuyCredit
    {
        public int Id { get; set; }
        public int PartnerId { get; set; }
        public int CustomerId { get; set; }
        public decimal CreditBought { get; set; }
        public DateTime CreditBoughtDate { get; set; }
        public string TransactionNumber { get; set; }
        public int CustomerTaxId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual CustomerTaxis CustomerTax { get; set; }
        public virtual Partner Partner { get; set; }
    }
}
