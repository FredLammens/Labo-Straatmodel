using Labo;
using System;
using System.Collections.Generic;
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
        private static string StartUnzipper()
        {
            Console.Clear();
            Console.WriteLine("--------------------------------Unzipper----------------------------------------");
            Console.WriteLine("| Please give the path to file to unzip :                                      |");
            Console.WriteLine("| to go back type: exit                                                        |");
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
                unzipPath = UnzipChecker(unzipPath, "Rapport-----------------------------------------");
            List<Provincie> provincies = Factories.ProvincieFactory(unzipPath);
            Console.Clear();
            Console.WriteLine("--------------------------------Rapport-----------------------------------------");
            Console.WriteLine("| 1) Make rapport file                                                         |");
            Console.WriteLine("| 2) Give rapport                                                              |");
            Console.WriteLine("| 3) Both                                                                      |");
            Console.WriteLine("| 4) Exit                                                                      |");
            Console.WriteLine("--------------------------------------------------------------------------------");
            int entered2 = ValueChecker(4);
            if (entered2 == 1)
            {
                Console.WriteLine("Give path for output file: ");
                Rapport.MaakRapportFile(provincies, @"" + Console.ReadLine());
            }
            else if (entered2 == 2)
                Rapport.GeefRapport(provincies);
            else if (entered2 == 3)
            {
                Console.WriteLine("Give path for output file: ");
                Rapport.MaakRapportFile(provincies, @"" + Console.ReadLine());
                Console.WriteLine("Rapport bestand gemaakt");
                Console.Clear();
                Rapport.GeefRapport(provincies);
                Console.WriteLine("Exit type: exit");
                if (Console.ReadLine().ToLower().Trim() == "exit")
                    return;
            }
            else if (entered2 == 4)
            {
                return;
            }
        }
        private static void StartDatabestand(string unzipPath) 
        {
            Console.Clear();
            if(unzipPath == "")
                unzipPath = UnzipChecker(unzipPath, "Databestand-------------------------------------");
                Console.WriteLine("--------------------------------Databestand-------------------------------------");
                Console.WriteLine("| Type :                                                                       |");
                Console.WriteLine("| 1) Binary                                                                    |");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("| 2) XML (comming soon)                                                        |");
                Console.WriteLine("| 3) JSON (comming soon)                                                       |");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("| 4) Exit                                                                      |");
                Console.WriteLine("--------------------------------------------------------------------------------");
            int entered = ValueChecker(4);
            Console.WriteLine("Give path for output file: ");
            string path = @"" + Console.ReadLine();
            if (entered == 1)
                Serializatie.SerializeProvinciesBinary(unzipPath,path);
            else if (entered == 2)
            {
                Console.WriteLine("Comming soon");
                return;
            }
            else if (entered == 3)
            {
                Console.WriteLine("Comming soon");
                return;
            }
            else if (entered == 4) 
            {
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
                    Console.WriteLine("Value is not valid , Please try again: ");
                }
                if (!(entered <= amountofValues && entered != 0))
                {
                    Console.WriteLine("Value is not in menu range, Please try again: ");
                }
                else
                {
                    inCorrectAnswer = false;
                }
            }
            return entered;
        }
        private static string UnzipChecker(string unzipPath , string menuItem) 
        {
                Console.WriteLine("--------------------------------"+menuItem);
                Console.WriteLine("| Please enter path of unzipt folder (name must be WRdata-master) :            |");
                Console.WriteLine("--------------------------------------------------------------------------------");
                string entered = Console.ReadLine();
                if (entered.ToLower().Trim() == "exit")
                {
                    return "";
                }
                else
                {
                    return @"" + entered;
                }
        }
    }
}
