using Labo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace StraatModel2.Tool1
{
    public class Tool1
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
                menuValue = ValueChecker(4);
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
            }
        }
        private static string StartUnzipper()
        {
            Console.Clear();
            Console.WriteLine("--------------------------------Unzipper----------------------------------------");
            Console.WriteLine("| Gelieve het pad van de gezipte file in te geven: (WRdata-master.zip)         |");
            Console.WriteLine("| Terugkeren typ: exit                                                         |");
            Console.WriteLine("--------------------------------------------------------------------------------");
            string entered = Console.ReadLine();
            if (entered.ToLower().Trim() == "exit")
            {
                return "";
            }
            else
            {
                string path = @"" + entered;
                return Inlezer.Unzipper(path);
            }
        }
        private static void StartRapport(string unzipPath)
        {
            Console.Clear();
            if (unzipPath == "")
            {
                bool ispath = false;
                while (!ispath)
                {
                    unzipPath = UnzipChecker(unzipPath, "Rapport-----------------------------------------");
                    if (Directory.Exists(unzipPath) && (unzipPath.EndsWith("WRdata-master") || unzipPath.EndsWith(@"WRdata-master\")))
                    {
                        ispath = true;
                    }
                }
            }
            List<Provincie> provincies = Factories.ProvincieFactory(unzipPath);
            Console.Clear();
            Console.WriteLine("--------------------------------Rapport-----------------------------------------");
            Console.WriteLine("| 1) Maak rapport bestand                                                      |");
            Console.WriteLine("| 2) Geef rapport                                                              |");
            Console.WriteLine("| 3) Beide                                                                     |");
            Console.WriteLine("| 4) Exit                                                                      |");
            Console.WriteLine("--------------------------------------------------------------------------------");
            int entered2 = ValueChecker(4);
            if (entered2 == 1)
            {
                Console.WriteLine("Geef pad van output in: ");
                Rapport.MaakRapportFile(provincies, @"" + Console.ReadLine());
            }
            else if (entered2 == 2)
                Rapport.GeefRapport(provincies);
            else if (entered2 == 3)
            {
                Console.WriteLine("Geef pad van output in: ");
                Rapport.MaakRapportFile(provincies, @"" + Console.ReadLine());
                Console.WriteLine("Rapport bestand gemaakt");
                Console.Clear();
                Rapport.GeefRapport(provincies);
                Console.WriteLine("type: exit");
                if (Console.ReadLine().ToLower().Trim() == "exit")
                    return;
            }
            else if (entered2 == 4)
            {
                Console.Clear();
                return;
            }
        }
        private static void StartDatabestand(string unzipPath)
        {
            Console.Clear();
            if (unzipPath == "")
                unzipPath = UnzipChecker(unzipPath, "Databestand-------------------------------------");
            Console.WriteLine("--------------------------------Databestand-------------------------------------");
            Console.WriteLine("| Type :                                                                       |");
            Console.WriteLine("| 1) Binary                                                                    |");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("| 2) XML (komt binnekort)                                                      |");
            Console.WriteLine("| 3) JSON (komt binnekort)                                                     |");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("| 4) Exit                                                                      |");
            Console.WriteLine("--------------------------------------------------------------------------------");
            int entered = ValueChecker(4);
            if (entered == 1)
            {
                Console.WriteLine("Geef pad van output in: ");
                string path = @"" + Console.ReadLine();
                Serializatie.SerializeProvinciesBinary(unzipPath, path);
            }
            else if (entered == 2)
            {
                Console.Clear();
                Console.WriteLine("Komt binnekort");
                return;
            }
            else if (entered == 3)
            {
                Console.Clear();
                Console.WriteLine("Komt binnekort");
                return;
            }
            else if (entered == 4)
            {
                Console.Clear();
                return;
            }
        }
        /// <summary>
        /// asks user for value and response appropriately.
        /// Gives back  the value user pressed
        /// </summary>
        /// <param name="amountofValues"></param>
        /// <returns></returns>
        public static int ValueChecker(int amountofValues)
        {
            int entered = -1;
            bool inCorrectAnswer = true;
            while (inCorrectAnswer)
            {
                while (!int.TryParse(Console.ReadLine(), out entered))
                {
                    Console.WriteLine("ongeldige waarde, probeer opnieuw a.u.b");
                }
                if (!(entered <= amountofValues && entered != 0))
                {
                    Console.WriteLine("Waarde is niet in het menu bereik, probeer opnieuw a.u.b");
                }
                else
                {
                    inCorrectAnswer = false;
                }
            }
            return entered;
        }
        private static string UnzipChecker(string unzipPath, string menuItem)
        {
            Console.WriteLine("--------------------------------" + menuItem);
            Console.WriteLine("| Geef pad van unzipt folder in a.u.b (beginnend met WRdata-master)            |");
            Console.WriteLine("--------------------------------------------------------------------------------");
            string entered = Console.ReadLine();
            if (entered.ToLower().Trim() == "exit")
            {
                Console.Clear();
                return "";
            }
            else
            {
                return @"" + entered;
            }
        }
    }
}
