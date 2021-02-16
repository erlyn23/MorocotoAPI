using System;
using System.Collections.Generic;

#nullable disable

namespace Morocoto.Domain.Models
{
    public partial class Log
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string LogMessage { get; set; }
        public string UserDevice { get; set; }
    }
}
