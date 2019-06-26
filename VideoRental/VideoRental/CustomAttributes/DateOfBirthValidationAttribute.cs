using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VideoRental.Models;

namespace VideoRental.CustomAttributes
{
    public class DateOfBirthValidationAttribute: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer = (Customer)validationContext.ObjectInstance;

            if (customer.MembershipTypeId == 0 || customer.MembershipTypeId == 1)
                return ValidationResult.Success;

            if(customer.BirthDate==null)
                return new ValidationResult("Enter Date of Birth");

            var age = DateTime.Now.Year - customer.BirthDate.Value.Year;

            return age >= 18 
                ? ValidationResult.Success 
                : new ValidationResult("Age must be 18 years or above for the selected membership plan");

        }
    }
}