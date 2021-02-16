using System;
using System.Collections.Generic;

#nullable disable

namespace Morocoto.Domain.Models
{
    public partial class Partner
    {
        public Partner()
        {
            Businesses = new HashSet<Business>();
            BuyCredits = new HashSet<BuyCredit>();
        }

        public int Id { get; set; }

        public virtual User IdNavigation { get; set; }
        public virtual ICollection<Business> Businesses { get; set; }
        public virtual ICollection<BuyCredit> BuyCredits { get; set; }
    }
}
