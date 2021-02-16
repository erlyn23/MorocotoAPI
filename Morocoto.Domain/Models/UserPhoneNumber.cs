using System;
using System.Collections.Generic;

#nullable disable

namespace Morocoto.Domain.Models
{
    public partial class UserPhoneNumber
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; }
    }
}
