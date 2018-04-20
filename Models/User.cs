using System; 
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations; 

namespace Belt.Models{

    public class User: BaseEntity{

        [Key]
        public int UserId {get; set;}
        public string FirstName {get;set;}
        public string LastName {get;set;}
        public string Email {get; set;}
        public string Password {get; set;}
        public List<Activity> Activities {get;set;}
        public List<Join> Joined {get;set;}

        public User(){
            Activities = new List<Activity>();
            Joined = new List<Join>();
        }
        
    }
}