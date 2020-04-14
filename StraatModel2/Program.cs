using StraatModel2;
using StraatModel2.Tool2;
using StraatModel2.Tool3;
using System.Collections.Generic;
using System;
using StraatModel2.Tool1;

namespace Labo
{
    class Program
    {
        static void Main(string[] args)
        {
            //DatabaseImporter db = new DatabaseImporter(@"Data Source=DESKTOP-OF28PIK\SQLEXPRESS;Initial Catalog=provincies; Integrated Security=True");
                Console.WriteLine("--------------------------------Het wegennetwerk--------------------------------");
                Console.WriteLine("| 1) Tool1 [rapport/Databestand]                                               |");
                Console.WriteLine("| 2) Tool2 [SQLDatabase]                                                       |");
                Console.WriteLine("| 3) Tool3 [SQLDatabase bevragingen]                                           |");
                Console.WriteLine("| 4) Exit                                                                      |");
                Console.WriteLine("--------------------------------------------------------------------------------");
            int menuValue = Tool1.ValueChecker(4);
            if (menuValue == 1)
                Tool1.Start();
            else if (menuValue == 2)
                Tool2.Start();
            else if (menuValue == 3)
                Tool3.Start();
            else if (menuValue == 4)
                return;
            //Console.WriteLine(Inlezer.Unzipper(@"C:\Users\Biebem\Downloads"));
        }
    }
}
