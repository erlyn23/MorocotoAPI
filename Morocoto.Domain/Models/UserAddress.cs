using System;
using System.Collections.Generic;

#nullable disable

namespace Morocoto.Domain.Models
{
    public partial class UserAddress
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; }
    }
}
