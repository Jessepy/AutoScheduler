using Microsoft.EntityFrameworkCore;
namespace AAS.DB.Data
{
    public class AggieAutoSchedulerContext : DbContext
    {
        public AggieAutoSchedulerContext (DbContextOptions<AggieAutoSchedulerContext> options)
            : base(options)
        {
        }

        public DbSet<AAS.DB.Course> Course { get; set; }
        public DbSet<AAS.DB.Exam> Exam { get; set; }
        public DbSet<AAS.DB.Period> Period { get; set; }
    }
}
