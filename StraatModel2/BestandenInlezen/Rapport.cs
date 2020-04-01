using Labo;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StraatModel2
{
    class Rapport
    {
        public static string MaakRapport(List<Provincie> provincies)
        {
            string rapportAantalStraten = "";
            string rapportStraatInfo = "";
            //info bijhouden
            int totaalAantalStraten = 0;
            provincies.ForEach(p => p.gemeentes.ForEach(g => totaalAantalStraten += g.straten.Count));
            rapportAantalStraten += $"<{totaalAantalStraten}> \n \n";
            rapportAantalStraten += "Aantal straten per provincie : \n \n";
            foreach (Provincie provincie in provincies)
            {
                int aantalStratenPerProvincie = 0;
                string straatInfoGemeente = "";
                foreach (Gemeente gemeente in provincie.gemeentes)
                {
                    aantalStratenPerProvincie += gemeente.straten.Count();
                    int aantalStratenInGemeente = gemeente.straten.Count();
                    int totaleLengteStraten = 0;
                    Straat kortste = new Straat(404, "bestaat niet", new Graaf(404));
                    Straat langste = new Straat(404, "bestaat niet", new Graaf(404));
                    int grootste = int.MinValue;
                    int kleinste = int.MaxValue;
                    foreach (Straat straat in gemeente.straten)
                    {
                        totaleLengteStraten += straat.getKnopen().Count;
                        int straatLengte = straat.getKnopen().Count;
                        if (straatLengte > grootste)
                        {
                            langste = straat;
                        }
                        if (straatLengte < kleinste)
                            kortste = straat;
                    }
                    straatInfoGemeente += $"    o   <{gemeente.gemeenteNaam}>: <{aantalStratenInGemeente}>,<{totaleLengteStraten}>";
                    straatInfoGemeente += $"           -  Kortste straat: ID: {kortste.straatId} Straatnaam: {kortste.straatnaam}\n";
                    straatInfoGemeente += $"           -  Langste straat: ID: {langste.straatId} Straatnaam: {langste.straatnaam}\n";
                }
                rapportAantalStraten += $"   o   <{provincie.provincieNaam}>: <{aantalStratenPerProvincie}>\n";
                rapportStraatInfo += $"StraatInfo <{provincie.provincieNaam}>:\n";
            }
            //rapport teruggeven
            return rapportAantalStraten + rapportStraatInfo;
        }
        public static void MaakRapportFile(string rapport) { }
        public static void GeefRapport(List<Provincie> provincies)
        {
            string rapport = MaakRapport(provincies);
            Console.WriteLine("-----------Rapport-----------");
            Console.WriteLine(rapport);
            Console.WriteLine("-----------------------------");
        }
    }
}
