using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Rentor.Models;

namespace Rentorer.CustomValidations
{
    public class IsOlderThanNow : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var movie = (Movie) validationContext.ObjectInstance;

            if (DateTime.Now.Subtract(movie.DateAdded).Days < 1)
            {
                return new ValidationResult("Movie must be at least 1 day old");
            }

            return ValidationResult.Success;
        }
    }
}