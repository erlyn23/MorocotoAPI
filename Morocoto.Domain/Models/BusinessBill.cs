using System;
using System.Collections.Generic;

#nullable disable

namespace Morocoto.Domain.Models
{
    public partial class BusinessBill
    {
        public int Id { get; set; }
        public int BusinessId { get; set; }
        public string PathFile { get; set; }
        public DateTime? DateCreation { get; set; }

        public virtual Business Business { get; set; }
    }
}
