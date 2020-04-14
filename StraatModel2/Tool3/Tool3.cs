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
                Console.WriteLine("--------------------------------Het wegennetwerk Tool 1-------------------------");
                Console.WriteLine("| 1) Unzipper                                                                  |");
                Console.WriteLine("| 2) Rapport                                                                   |");
                Console.WriteLine("| 3) Databestand                                                               |");
                Console.WriteLine("| 4) Exit                                                                      |");
                Console.WriteLine("--------------------------------------------------------------------------------");
                while (!int.TryParse(Console.ReadLine(), out menuValue))
                    Console.WriteLine("Value is not valid , Please try again: ");
                if (menuValue == 1)
                    unzipPath = StartUnzipper();
                else if (menuValue == 2)
                    StartRapport(unzipPath);
                else if (menuValue == 3)
                    StartDatabestand(unzipPath);
                else if (menuValue == 4)
                {
                    return;
                }
                else
                {
                    Console.WriteLine("Value is not in menu range.");
                }
            }
        }
    }
}
