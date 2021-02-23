using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Morocoto.API.Models
{
    public class AuthModel
    {
        public string IdentificationDocument { get; set; }
        public string UserPassword { get; set; }
        public string UserPhone { get; set; }
        public string OsPhone { get; set; }
    }
}
