using System; 
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations; 

namespace Belt.Models{
    public abstract class BaseEntity{
        public DateTime created_at {get; set;} = DateTime.Now; 
        public DateTime updated_at {get; set;} = DateTime.Now; 
    }
}