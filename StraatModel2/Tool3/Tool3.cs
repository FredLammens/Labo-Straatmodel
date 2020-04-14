using System;
using System.Collections.Generic;
using System.Text;

namespace StraatModel2.Tool3
{
    class Tool3
    {
        public static void Start()
        {
            Console.Clear();
            string unzipPath = "";
            int menuValue = 0;
            while (menuValue != 4)
            {
                Console.WriteLine("--------------------------------Het wegennetwerk Tool 3-------------------------");
                Console.WriteLine("| 1) Unzipper                                                                  |");
                Console.WriteLine("| 2) Rapport                                                                   |");
                Console.WriteLine("| 3) Databestand                                                               |");
                Console.WriteLine("| 4) Exit                                                                      |");
                Console.WriteLine("--------------------------------------------------------------------------------");
            }
        }
    }
}
