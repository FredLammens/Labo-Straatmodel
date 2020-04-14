using System;

namespace StraatModel2.Tool3
{
    class Tool3
    {
        public static void Start()
        {
            Console.Clear();
            int menuValue = 0;
            while (menuValue != 4)
            {
                Console.WriteLine("--------------------------------Het wegennetwerk Tool 3-------------------------");
                Console.WriteLine("|                                Database bevragingen                          |");
                Console.WriteLine("| 1) straatIDs voor opgegeven gemeentenaam                                     |");
                Console.WriteLine("| 2) straat op basis van meegegeven straatID                                   |");
                Console.WriteLine("| 3) straat op basis van straatnaam en gemeentenaam                            |");
                Console.WriteLine("| 4) alle straatnamen van gemeente alfabetisch gesorteerd                      |");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("| 5) overzichtsrapport van bepaalde provincie (comming soon)                   |");
                Console.WriteLine("| 6) alle straten die grenzen aan opgegeven straat (comming soon)              |");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("--------------------------------------------------------------------------------");
                int entered = StraatModel2.Tool1.Tool1.ValueChecker(6);
            }
        }
    }
}
