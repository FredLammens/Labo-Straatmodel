using StraatModel2;
using StraatModel2.Tool2;
using StraatModel2.Tool3;
using System.Collections.Generic;

namespace Labo
{
    class Program
    {
        static void Main(string[] args)
        {
            //DatabaseImporter db = new DatabaseImporter(@"Data Source=DESKTOP-OF28PIK\SQLEXPRESS;Initial Catalog=provincies; Integrated Security=True");
            //db.InsertAll(Factories.ProvincieFactory());
            //System.Console.WriteLine("done");
            DatabaseBevragingen dbv = new DatabaseBevragingen(@"Data Source=DESKTOP-OF28PIK\SQLEXPRESS;Initial Catalog=provincies; Integrated Security=True");
            System.Console.WriteLine(dbv.GeefStraat(3599));
        }
    }
}
