using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VideoRental.Models;

namespace VideoRental.ViewModels
{
    public class CustomerIndexViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public MembershipTypeViewModel MembershipType { get; set; }
    }
}