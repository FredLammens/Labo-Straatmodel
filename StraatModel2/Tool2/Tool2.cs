using System;
using System.Collections.Generic;
using System.Text;

namespace StraatModel2.Tool2
{
    class Tool2
    {
        public static void Start()
        {
            Console.Clear();
            Console.WriteLine("--------------------------------Het wegennetwerk Tool 2-------------------------");
            Console.WriteLine("|                                   Deserialization                            |");
            Console.WriteLine("| Gelieve het pad van de input file in te geven: (provincies.dat)              |");
            Console.WriteLine("| exit om te stoppen                                                           |");
            Console.WriteLine("--------------------------------------------------------------------------------");
            string entered = Console.ReadLine();
            if (entered.ToLower().Trim() == "exit")
            {
                return;
            }
            else
            {
                string pathInput = @"" + entered;
                DatabaseImporter db = new DatabaseImporter(@"Data Source=DESKTOP-OF28PIK\SQLEXPRESS;Initial Catalog=provincies; Integrated Security=True");
                db.InsertAll(Serializatie.DeSerializeProvinciesBinary(pathInput));
                return;
            }

        }
    }
}
