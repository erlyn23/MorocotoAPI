using System;
using System.Collections.Generic;

#nullable disable

namespace Morocoto.Domain.Models
{
    public partial class BusinessPhoneNumber
    {
        public int Id { get; set; }
        public int BusinessId { get; set; }
        public string PhoneNumber { get; set; }

        public virtual Business Business { get; set; }
    }
}
