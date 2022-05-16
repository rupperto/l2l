using System;
using l2l.Data.Model;
using Microsoft.EntityFrameworkCore;

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
            //var db = factory.CreateDbContext(new string[] { }); //Anti pattern kerjuk el tole az aktualis dbcontextet, uj string tombot kell neki atadni, elmentjuk a db nevu localis valtozo
            var db = GetNewL2lDBContext(); //szuksegunk van egy olyan factory methodra ami mindig letrehoz egy uj peldanyt valamibol

            if (factory.IsInMemoryDb())
            {
                //in memory db
                db.Database.EnsureCreated(); //biztositsa szamunkara, hogy az adatbazis lere lett hozva
            }
            else
            {
            //migrate parancs csak file adatbaisban
            db.Database.Migrate();

            }
           

        }


        public L2lDBContext GetNewL2lDBContext()
        {
            //redonly mezofel elkeszul a l2dbfactory factory method
            //ezzel a fuggvennyel peldanyositunk egy uj db peldanyt
            return factory.CreateDbContext(new string[] { });

        }


        public void Dispose()
        {

            //throw new NotImplementedException();
            var db = GetNewL2lDBContext(); //lokálisan jó lesz ez is

            factory.Dispose(); //mielott torolnem dispsoljuk

            db.Database.EnsureDeleted(); //szeretnek torolni 

            db.Dispose();


        }


    }
}
