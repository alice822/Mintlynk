using MintLynk.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MintLynk.Application.Attributes
{
    public class UniqueAliasAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var alias = value as string;

            var service = validationContext.GetService(typeof(IMiniPageService)) as IMiniPageService;
            if (service != null && service.AliasExistsAsync(alias).Result)
            {
                return new ValidationResult("This alias is already taken. Please choose another.");
            }
            return ValidationResult.Success;
        }
    }
}
