using System; 
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;  

namespace Belt.Models{

    public class Activity: BaseEntity{

        [Key]
        public int ActivityId {get; set;}
        public string Title {get; set;}
        public string Description {get; set;}
        public DateTime Date {get; set;}
        public TimeSpan Time {get; set;}
        public TimeSpan Duration {get; set; }
        public List<Join> Joined {get; set; }
        [ForeignKey("user")]
        public int UsersId {get; set;}
        public User user {get; set;}

        public Activity(){
            Joined = new List<Join>();
        }
    }
}