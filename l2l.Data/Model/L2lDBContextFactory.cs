using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace l2l.Data.Model
{
    public class L2lDBContextFactory : IDesignTimeDbContextFactory<L2lDBContext>
    {
        public L2lDBContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<L2lDBContext>();
            //TODO: setting to appsetting
            builder.UseSqlite("Data Source=l2l.db;");

            return new L2lDBContext(builder.Options); // visszadjuk a construktornak az option erteket
        }
    }
}