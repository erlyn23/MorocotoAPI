using System;
using System.Collections.Generic;

#nullable disable

namespace Morocoto.Domain.Models
{
    public partial class Business
    {
        public Business()
        {
            BusinessAddresses = new HashSet<BusinessAddress>();
            BusinessBills = new HashSet<BusinessBill>();
            BusinessPhoneNumbers = new HashSet<BusinessPhoneNumber>();
            Requests = new HashSet<Request>();
        }

        public int Id { get; set; }
        public int PartnerId { get; set; }
        public int BusinessTypeId { get; set; }
        public int BusinessNumber { get; set; }
        public string BusinessName { get; set; }
        public decimal? BusinessCreditAvailable { get; set; }

        public virtual BusinessType BusinessType { get; set; }
        public virtual Partner Partner { get; set; }
        public virtual ICollection<BusinessAddress> BusinessAddresses { get; set; }
        public virtual ICollection<BusinessBill> BusinessBills { get; set; }
        public virtual ICollection<BusinessPhoneNumber> BusinessPhoneNumbers { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
    }
}
