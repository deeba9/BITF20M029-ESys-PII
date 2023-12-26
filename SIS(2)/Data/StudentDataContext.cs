using Microsoft.EntityFrameworkCore;
using SIS_2_.Models.Domain;

namespace SIS_2_.Data
{
    public class StudentDataContext: DbContext
    {
        public StudentDataContext(DbContextOptions<StudentDataContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Interest> Interests { get; set; }
        public DbSet<Account> Accounts { get; set; }

        public DbSet<ActivityLog> ActivityLogs { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
               modelBuilder.Entity<ActivityLog>().ToTable("ActivityLogs");

        }
        public StudentDataContext()
        {
            
        }


    }
}
