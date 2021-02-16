using System;
using System.Collections.Generic;

#nullable disable

namespace Morocoto.Domain.Models
{
    public partial class BusinessType
    {
        public BusinessType()
        {
            Businesses = new HashSet<Business>();
        }

        public int Id { get; set; }
        public string BusinessType1 { get; set; }

        public virtual ICollection<Business> Businesses { get; set; }
    }
}
