using System;
using System.Collections.Generic;

#nullable disable

namespace Morocoto.Domain.Models
{
    public partial class UserType
    {
        public UserType()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string UserType1 { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
