using System;
using System.Collections.Generic;

#nullable disable

namespace Morocoto.Domain.Models
{
    public partial class SecurityQuestion
    {
        public SecurityQuestion()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string SecurityQuestion1 { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
