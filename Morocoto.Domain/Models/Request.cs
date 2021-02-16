using System;
using System.Collections.Generic;

#nullable disable

namespace Morocoto.Domain.Models
{
    public partial class Request
    {
        public int Id { get; set; }
        public DateTime RequestDate { get; set; }
        public decimal RequestedCredit { get; set; }
        public int RequestStateId { get; set; }
        public int BusinessId { get; set; }

        public virtual Business Business { get; set; }
        public virtual RequestState RequestState { get; set; }
    }
}
