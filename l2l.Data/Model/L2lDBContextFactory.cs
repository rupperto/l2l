using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace l2l.Data.Model
{
    public class L2lDBContextFactory : IDesignTimeDbContextFactory<L2lDBContext>
    {
        public L2lDBContext CreateDbContext(string[] args)
        {
            var obuilder = new DbContextOptionsBuilder<L2lDBContext>();

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
            var cn = config.GetConnectionString(GlobalStrings.ConnectionName); //ebben a cn mar az appsettingsbol kiolvasott karakter lanc lesz

            //TODO: setting to appsetting
            //obuilder.UseSqlite("Data Source=l2l.db;");
            obuilder.UseSqlite(cn);

            return new L2lDBContext(obuilder.Options); // visszadjuk a construktornak az option erteket
        }
    }
}