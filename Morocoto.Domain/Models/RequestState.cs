using System;
using System.Collections.Generic;

#nullable disable

namespace Morocoto.Domain.Models
{
    public partial class RequestState
    {
        public RequestState()
        {
            Requests = new HashSet<Request>();
        }

        public int Id { get; set; }
        public string RequestState1 { get; set; }

        public virtual ICollection<Request> Requests { get; set; }
    }
}
