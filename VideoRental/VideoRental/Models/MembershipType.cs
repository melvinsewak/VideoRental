﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VideoRental.Models
{
    public class MembershipType
    {
        public byte Id { get; set; }
        public string Name { get; set; }
        public decimal SignUpFee { get; set; }
        public byte DurationInMonths { get; set; }
        public float DiscountRate { get; set; }
    }
}