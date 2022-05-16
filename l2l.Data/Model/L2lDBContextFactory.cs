using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace l2l.Data.Model
{
    public class L2lDBContextFactory : IDesignTimeDbContextFactory<L2lDBContext>, IDisposable
    {
        private readonly string cn;
        private readonly SqliteConnection connection;

        public L2lDBContextFactory()//parameter nelkuli ctor
        {
            var basePath = Directory.GetCurrentDirectory(); //visszaadja az aktualis konyvtar nevet

            //TODO: Antipattern
            var enviroment = Environment.GetEnvironmentVariable(GlobalStrings.AspNetCoreEnviroment);//Kiolvassuk a kornyezeti valtozot

            //dotnet programok ebben a kornyezeti valtozban taroljak, hogy milyen kornyezetben futnak
            /// <summary>
            /// Igy hozhatunk letre egy buildert
            /// </summary>
            /// <returns></returns>
            var cbuilder = new ConfigurationBuilder()
                            .SetBasePath(basePath)
                            .AddJsonFile("appsettings.json")
                            .AddJsonFile($"appsettings.{enviroment}.json", true)
                            .AddEnvironmentVariables()
                            ;

            //builder hasznalata:
            var config = cbuilder.Build(); //visszaad egy konfig peldanyat


            //TODO: Antipattern, a sok szovegezes, globalString osztályba berakjuk
            //var cn = config.GetConnectionString(GlobalStrings.ConnectionName); //ebben a cn mar az appsettingsbol kiolvasott karakter lanc lesz
            cn = config.GetConnectionString(GlobalStrings.ConnectionName); //ebben a cn mar az appsettingsbol kiolvasott karakter lanc lesz
            //itt tudjuk megtenni a kulonbseget hogy inMemory vagy fajl db ben vagyunk

            if (IsInMemoryDb())
            {
                //amikor ez a kod lefut mindig ujraepul a connection
                connection = new SqliteConnection(cn);
                connection.Open();

            }
        }

        public bool IsInMemoryDb()
        {

            var cb = new SqlConnectionStringBuilder(cn);
            if (!cb.ContainsKey(GlobalStrings.DataSource))
            {

                throw new ArgumentException("Missing property from ConnectionString: Data Source", "connectionstring");
            }

            return GlobalStrings.SqlMemoryDb.Equals((string)cb[GlobalStrings.DataSource], StringComparison.OrdinalIgnoreCase);

        }

        public L2lDBContext CreateDbContext(string[] args)
        {
            var obuilder = new DbContextOptionsBuilder<L2lDBContext>();



            //TODO: setting to appsetting
            //obuilder.UseSqlite("Data Source=l2l.db;");
            if (IsInMemoryDb())
            {
                obuilder.UseSqlite(connection);
            }
            else
            {
                obuilder.UseSqlite(cn);

            }

            return new L2lDBContext(obuilder.Options); // visszadjuk a construktornak az option erteket
        }

        public void Dispose()
        {
            if (connection != null)
            {
                connection.Dispose();
            }
        }
    }
}