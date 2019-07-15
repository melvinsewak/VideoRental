﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VideoRental.CustomAttributes;

namespace VideoRental.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Display(Name ="Date of Birth")]
        [DateOfBirthValidationAttribute]
        public DateTime? BirthDate { get; set; }

        public bool IsSubscribedToNewsletter { get; set; }

        //NOTE: If you mark this property as virtual it will be loaded lazily creating N+1 issue
        public MembershipType MembershipType { get; set; } 

        [Display(Name = "Membership Type")]
        public byte MembershipTypeId { get; set; }
    }
}