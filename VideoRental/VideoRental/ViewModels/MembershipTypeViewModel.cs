﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VideoRental.ViewModels
{
    public class MembershipTypeViewModel
    {

        public byte Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public decimal SignUpFee { get; set; }

        public byte DurationInMonths { get; set; }

        public float DiscountRate { get; set; }

    }
}