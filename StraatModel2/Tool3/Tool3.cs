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
                Console.WriteLine("| 7) Exit                                                                      |");
                Console.WriteLine("--------------------------------------------------------------------------------");
                int entered = Tool1.Tool1.ValueChecker(7);
                DatabaseBevragingen dbv = new DatabaseBevragingen(@"Data Source=DESKTOP-OF28PIK\SQLEXPRESS;Initial Catalog=provincies; Integrated Security=True");
                if (entered == 1)
                {
                    Console.WriteLine("--------------------------------Het wegennetwerk Tool 3-------------------------");
                    Console.WriteLine("| Wat is de gemeentenaam?                                                      |");
                    Console.WriteLine("--------------------------------------------------------------------------------");
                    string gemeentenaam = Console.ReadLine().Trim();
                    foreach (int straatID in dbv.GeefStraatIds(gemeentenaam))
                    {
                        Console.WriteLine("straatId is  :" + straatID);
                    }
                }
                else if (entered == 2)
                {
                    Console.WriteLine("--------------------------------Het wegennetwerk Tool 3-------------------------");
                    Console.WriteLine("| Wat is de straatID?                                                          |");
                    Console.WriteLine("--------------------------------------------------------------------------------");
                    //string straatID = Console.ReadLine().Trim();
                    int straatID = 0;
                    while (!int.TryParse(Console.ReadLine(), out straatID)) 
                    {
                        Console.WriteLine("Geen geldig getal, gelieve opnieuw te proberen: ");
                    }
                    Console.WriteLine(dbv.GeefStraat(straatID)); 
                }
                else if (entered == 3)
                {
                    Console.WriteLine("--------------------------------Het wegennetwerk Tool 3-------------------------");
                    Console.WriteLine("| Wat is de straatnaam?                                                        |");
                    Console.WriteLine("--------------------------------------------------------------------------------");
                    string straatnaam = Console.ReadLine();
                    Console.WriteLine("--------------------------------Het wegennetwerk Tool 3-------------------------");
                    Console.WriteLine("| Wat is de gemeentenaam?                                                      |");
                    Console.WriteLine("--------------------------------------------------------------------------------");
                    string gemeentenaam = Console.ReadLine();
                    Console.WriteLine(dbv.GeefStraat(straatnaam, gemeentenaam));
                }
                else if (entered == 4)
                {
                    Console.WriteLine("--------------------------------Het wegennetwerk Tool 3-------------------------");
                    Console.WriteLine("| Wat is de gemeentenaam?                                                      |");
                    Console.WriteLine("--------------------------------------------------------------------------------");
                    string gemeentenaam = Console.ReadLine();
                    Console.WriteLine("\nAlle straten van de gemeente: "+ gemeentenaam);
                    foreach (string straatnaam in dbv.GeefStraatnamenGemeente(gemeentenaam))
                    {
                        Console.WriteLine(straatnaam);
                    }
                }
                else if (entered == 5)
                    Console.WriteLine("Comming soon");
                else if (entered == 6)
                    Console.WriteLine("Comming soon");
                else if (entered == 7)
                    return;
            }
        }
    }
}
