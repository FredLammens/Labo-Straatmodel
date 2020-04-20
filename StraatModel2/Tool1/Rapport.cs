using Labo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace StraatModel2
{
    class Rapport
    {
        private static string MaakRapport(List<Provincie> provincies)
        {
            StringBuilder rapportAantalStraten = new StringBuilder();
            StringBuilder rapportStraatInfo = new StringBuilder();
            //info bijhouden
            int totaalAantalStraten = 0;
            provincies.ForEach(p => p.gemeentes.ForEach(g => totaalAantalStraten += g.straten.Count));
            rapportAantalStraten.Append($"<{totaalAantalStraten}> \n \n");
            rapportAantalStraten.Append("Aantal straten per provincie : \n \n");
            foreach (Provincie provincie in provincies)
            {
                int aantalStratenPerProvincie = 0;
                string straatInfoGemeente = "";
                foreach (Gemeente gemeente in provincie.gemeentes)
                {
                    aantalStratenPerProvincie += gemeente.straten.Count();
                    int aantalStratenInGemeente = gemeente.straten.Count();
                    double totaleLengteStraten = 0;
                    Straat kortste = new Straat(404, "bestaat niet", new Graaf(404));
                    Straat langste = new Straat(404, "bestaat niet", new Graaf(404));
                    double grootste = double.MinValue;
                    double kleinste = double.MaxValue;
                    foreach (Straat straat in gemeente.straten)
                    {
                        if (!double.IsNaN(straat.getLengte()))
                        {
                            totaleLengteStraten += straat.getLengte();
                        }
                        double straatLengte = straat.getLengte();
                        if (straatLengte > grootste)
                        {
                            grootste = straatLengte;
                            langste = straat;
                        }
                        if (straatLengte < kleinste)
                        {
                            kleinste = straatLengte;
                            kortste = straat;
                        }
                    }
                    straatInfoGemeente += $"    o   <{gemeente.gemeenteNaam}>: <{aantalStratenInGemeente}>,<{Math.Round(totaleLengteStraten,2)}>";
                    straatInfoGemeente += $"\n         -  Kortste straat: ID: {kortste.straatId} Straatnaam: {kortste.straatnaam} Lengte: {Math.Round(kleinste,2)}\n";
                    straatInfoGemeente += $"         -  Langste straat: ID: {langste.straatId} Straatnaam: {langste.straatnaam} Lengte: {Math.Round(grootste,2)}\n";
                }
                rapportAantalStraten.Append($"   o   <{provincie.provincieNaam}>: <{aantalStratenPerProvincie}>\n");
                rapportStraatInfo.Append($"\nStraatInfo <{provincie.provincieNaam}>:\n");
                rapportStraatInfo.Append(straatInfoGemeente);
            }
            //rapport teruggeven
            return rapportAantalStraten.ToString() + rapportStraatInfo.ToString();
        }
        public static void MaakRapportFile(List<Provincie> provincies , string path) 
        {
            string rapport = MaakRapport(provincies);
            using (StreamWriter file = new StreamWriter(path + @"\ProvinciesRapport.txt")) 
            {
                file.Write(rapport);
            }
        }
        public static void GeefRapport(List<Provincie> provincies)
        {
            string rapport = MaakRapport(provincies);
            Console.WriteLine("--------------------------------Rapport-----------------------------------------");
            Console.WriteLine(rapport);
            Console.WriteLine("--------------------------------------------------------------------------------");
        }
    }
}
