using System;
using System.Collections.Generic;

#nullable disable

namespace Morocoto.Domain.Models
{
    public partial class Customer
    {
        public Customer()
        {
            BuyCredits = new HashSet<BuyCredit>();
            TransactionCustomerRecievers = new HashSet<Transaction>();
            TransactionCustomerSenders = new HashSet<Transaction>();
        }

        public int Id { get; set; }

        public virtual User IdNavigation { get; set; }
        public virtual ICollection<BuyCredit> BuyCredits { get; set; }
        public virtual ICollection<Transaction> TransactionCustomerRecievers { get; set; }
        public virtual ICollection<Transaction> TransactionCustomerSenders { get; set; }
    }
}
