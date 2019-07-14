using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VideoRental.CustomAttributes;
using VideoRental.Models;

namespace VideoRental.ViewModels
{
    public class CustomerFormViewModel
    {
        public IEnumerable<MembershipTypeViewModel> MembershipTypes { get; set; }

        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Display(Name="Date of Birth")]
        public DateTime? BirthDate { get; set; }

        [Display(Name= "Is Subscribed To Newsletter?")]
        public bool IsSubscribedToNewsletter { get; set; }

        [Display(Name="Membership Type")]
        public byte MembershipTypeId { get; set; }

        public string Title
        { get
            {
                if (Id == 0)
                    return "New Customer";
                else
                    return "Edit Customer";
            }
        }

        public CustomerFormViewModel()
        {
            Id = 0;
        }

    }
}