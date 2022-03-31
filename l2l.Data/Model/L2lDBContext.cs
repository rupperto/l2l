using Microsoft.EntityFrameworkCore;

namespace l2l.Data.Model
{
    public class L2lDBContext : DbContext
    {
        public L2lDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }
    }
}