using Microsoft.EntityFrameworkCore; 

namespace Belt.Models{

    public class BeltContext: DbContext{

        public BeltContext(DbContextOptions<BeltContext> options) : base(options){}

        public DbSet<User> Users {get; set;}
        public DbSet<Activity> Activities {get; set;}
        public DbSet<Join> Joined {get; set; }
    }
}