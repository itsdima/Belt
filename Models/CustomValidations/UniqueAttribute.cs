using System.ComponentModel.DataAnnotations; 
using System; 
using System.Collections.Generic; 

namespace Belt.Models.CustomValidations{

    public class UniqueAttribute: ValidationAttribute{

        protected override ValidationResult IsValid(object value, ValidationContext validationContext){
            var service = (BeltContext) validationContext.GetService(typeof(BeltContext));
            var allusers = service.Users; 
            foreach(var user in allusers){
                if((string)value == (string)user.Email){
                    return new ValidationResult("Email is already taken!");
                }
            }
            return ValidationResult.Success;
        }
    }
}