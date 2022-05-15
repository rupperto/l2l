using System;
using l2l.Data.Model;

namespace l2l.Data.Tests
{
    //kiszerveztuk az adatbazis elerhetoseget kulon osztalyba!!!
    public class DatabaseFixture : IDisposable
    {
        private readonly L2lDBContextFactory factory;

        //public L2lDBContext? db { get; private set; } // publiccal kivulrol is elerheto a letezo adatbazishoz valo hozzaferes



        public DatabaseFixture()
        {

            factory = new L2lDBContextFactory(); //dbcontextre szukseg van //antipattern
            //var db = factory.CreateDbContext(new string[] { }); //kerjuk el tole az aktualis dbcontextet, uj string tombot kell neki atadni, elmentjuk a db nevu localis valtozo
            var db = GetNewL2lDBContext();
            db.Database.EnsureCreated(); //biztositsa szamunkara, hogy az adatbazis lere lett hozva
        }


        public L2lDBContext GetNewL2lDBContext()
        {
            //redonly mezofel elkeszul a l2dbfactory 
            return factory.CreateDbContext(new string[] { });

        }


        public void Dispose()
        {
            //throw new NotImplementedException();
            var db = GetNewL2lDBContext();
            db.Database.EnsureDeleted();
            db.Dispose();


        }


    }
}
