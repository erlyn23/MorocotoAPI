using System;
using System.Collections.Generic;

#nullable disable

namespace Morocoto.Domain.Models
{
    public partial class User
    {
        public User()
        {
            UserAddresses = new HashSet<UserAddress>();
            UserPhoneNumbers = new HashSet<UserPhoneNumber>();
        }

        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public string FullName { get; set; }
        public string UserPhone { get; set; }
        public string OsPhone { get; set; }
        public string IdentificationDocument { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string UserPassword { get; set; }
        public string Pin { get; set; }
        public string SecurityAnswer { get; set; }
        public bool Active { get; set; }
        public int UserTypeId { get; set; }
        public int SecurityQuestionId { get; set; }

        public virtual SecurityQuestion SecurityQuestion { get; set; }
        public virtual UserType UserType { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Partner Partner { get; set; }
        public virtual ICollection<UserAddress> UserAddresses { get; set; }
        public virtual ICollection<UserPhoneNumber> UserPhoneNumbers { get; set; }
    }
}
