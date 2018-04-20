using System.ComponentModel.DataAnnotations; 
using Belt.Models.CustomValidations; 

namespace Belt.Models{

    public class UserViewModel{

        [Required(ErrorMessage = " First Name is required")]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage="Your name must be letters only")]
        [Display(Name = "First Name")]
        public string FirstName {get; set;}

        [Required(ErrorMessage = " Last Name is required")]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage="Your must be letters only")]
        [Display(Name = "Last Name")]
        public string LastName {get; set;}

        [EmailAddress(ErrorMessage= " Invalid Email")]
        [Required(ErrorMessage= " Email is Required")]
        [Display(Name = "Email")]
        [Unique]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage=" Email is not valid")]
        public string Email { get; set; }

        [Required(ErrorMessage = " Password is required")]
        [Display(Name = "Password")]
        [RegularExpression(@"^.*(?=.{8,})(?=.*\d)(?=.*[a-zA-Z])(?=.*[@#$!%^&+=]).*$", ErrorMessage="Password must contain a letter, number and special character")]
        [MinLength(8, ErrorMessage = " Password must be at least 8 characters")]
        public string Password {get; set; }

        [Required(ErrorMessage = " Confirm your passowrd")]
        [Display(Name = " Confirm Password")]
        [CompareAttribute("Password", ErrorMessage = " Passwords do not match")]
        public string Confirm {get; set;}
    }
}