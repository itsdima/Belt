using System; 
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations; 
using System.ComponentModel.DataAnnotations.Schema; 

namespace Belt.Models{

    public class Join: BaseEntity{

        [Key]
        public int JoinId {get; set; }
        [ForeignKey("user")]
        public int UsersId {get; set; }
        public User user {get; set; }
        [ForeignKey("activity")]
        public int ActivitiesId {get; set; }
        public Activity activity {get; set; }
    }
}