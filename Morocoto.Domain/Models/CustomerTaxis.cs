using System;
using System.Collections.Generic;

#nullable disable

namespace Morocoto.Domain.Models
{
    public partial class CustomerTaxis
    {
        public CustomerTaxis()
        {
            BuyCredits = new HashSet<BuyCredit>();
            Transactions = new HashSet<Transaction>();
        }

        public int Id { get; set; }
        public decimal Tax { get; set; }

        public virtual ICollection<BuyCredit> BuyCredits { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
