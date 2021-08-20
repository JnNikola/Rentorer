using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Rentor.Models;
using Rentorer.Models;

namespace Rentorer.CustomValidations
{
    public class Min18IfMember : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer = (Customer) validationContext.ObjectInstance;

            if (customer.MembershipTypeId==MembershipType.Unknown || customer.MembershipTypeId==MembershipType.PayAsYouGo)
            {
                return ValidationResult.Success;
            }

            if (customer.Birthday == null)
            {
                return new ValidationResult("Birth Date Required for Membership");
            }

            var age = DateTime.Now.Year - customer.Birthday.Value.Year;
            return (age >= 18)
                ? ValidationResult.Success
                : new ValidationResult("Must be at least 18 to be member");


        }
    }
}