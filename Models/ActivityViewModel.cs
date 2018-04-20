using System.ComponentModel.DataAnnotations; 
using Belt.Models.CustomValidations;
using System; 

namespace Belt.Models{

    public class ActivityViewModel{

        [Required(ErrorMessage = "Title is required")]
        [MinLength(2, ErrorMessage="Title must be at least 2 characters")]
        [Display(Name = "Title")]
        public string Title {get; set;}

        [Required(ErrorMessage = "Description is required")]
        [MinLength(10, ErrorMessage="Description must be at least 10 characters")]
        [Display(Name = "Description")]
        public string Description {get; set;}

        [Required(ErrorMessage = "Date is required")]
        [Display(Name = "Date")]
        [FutureDate]
        public  DateTime Date { get; set; }

        [Required(ErrorMessage = "Time is required")]
        [Display(Name = "Time")]
        public TimeSpan Time {get; set;}

        [Required(ErrorMessage = "Duration is required")]
        [Display(Name = "Duration")]
        [RegularExpression(@"^(?:0?[0-9]|1[0-9]|2[0-3]):[0-5][0-9]:[0-9][0-9]$", ErrorMessage="Duration format is HH:MM:SS please and thank you")]
        public TimeSpan Duration {get; set;}
    }
}