﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Morocoto.API.Models
{
    public class SellCreditModel
    {
        public string BusinessAccountNumber { get; set; }
        public string CustomerAccountNumber { get; set; }
        public double CreditSelled { get; set; }
        public string Pin { get; set; }
    }
}