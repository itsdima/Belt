using System.ComponentModel.DataAnnotations; 
using System.Collections.Generic; 
using System; 

namespace Belt.Models.CustomValidations{

    public class FutureDateAttribute: ValidationAttribute{

        protected override ValidationResult IsValid(object value, ValidationContext validationContext){
            if(DateTime.Now > (DateTime)value){
                return new ValidationResult("Plan for the future!");
            }
            else{
                return ValidationResult.Success; 
            }
        }
    }
}