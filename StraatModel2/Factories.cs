using System.Collections.Generic;

namespace Labo
{
    class Factories
    {
        #region StraatFactory
        /// <summary>
        ///  gaat door WRstraatnamen en WRdata files
        ///  returned lijst van alle straten.
        /// </summary>
        /// <returns></returns>
        public static List<Straat> StraatFactory()
        {
            List<Straat> straten = new List<Straat>();
            #region ingelezen
            Dictionary<int, string> WRstraatnamen = Inlezer.WRstraatNamenParser();
            Dictionary<int, List<Segment>> WRData = Inlezer.WRdataParser();
            #endregion
            foreach (KeyValuePair<int, string> straatnaam in WRstraatnamen)
            {
                //controleren of straatnamen en wrData overeenkomen
                if (WRstraatnamen.ContainsKey(straatnaam.Key) && WRData.ContainsKey(straatnaam.Key))
                {
                    straten.Add(new Straat(straatnaam.Key, straatnaam.Value, Graaf.buildGraaf(straatnaam.Key, WRData[straatnaam.Key])));
                    System.Console.WriteLine($"straat {straatnaam.Value} aangemaakt. \n");
                }
                else
                    System.Console.WriteLine("straatnaam zit niet in WRdata \n");
            }
            System.Console.WriteLine("klaar met straten maken.");
            return straten;
        }
        #endregion
        #region GemeenteFactory
        /// <summary>
        /// gaat door gemeenteID.csv en WRgemeentenaam.csv en door de files van Straatfactory
        /// om een lijst van gemeentes terug te geven
        /// </summary>
        public static List<Gemeente> GemeenteFactory()
        {
            List<Gemeente> gemeentes = new List<Gemeente>();
            #region ingelezen
            List<Straat> alleStraten = StraatFactory();
            Dictionary<int, List<int>> gemeenteIDs = Inlezer.WRgemeenteIDParser();
            Dictionary<int, string> gemeentenamenPerId = Inlezer.WRgemeentenaamParser();
            #endregion
            foreach (KeyValuePair<int, List<int>> gemeenteId in gemeenteIDs)
            {
                #region alle straten uit gemeente
                List<Straat> straten = new List<Straat>();
                foreach (int straatnaamIDs in gemeenteId.Value)//eerst door alle stratenIDs in de gemeente => kleinste foreach zoveel mogelijk boven : 3^5 < 5^3
                {
                    foreach (Straat straat in alleStraten) //door alle straten
                    {
                        if (straatnaamIDs == straat.straatId)
                            straten.Add(straat); //als de straat in de gemeentevoorkomt toevoegen aan lijst van straten
                    }
                }
                #endregion
                gemeentes.Add(new Gemeente(gemeenteId.Key, gemeentenamenPerId[gemeenteId.Key], straten)); // straten toevoegen aan gemeente en deze aan de lijst van alle gemeentes
                //kan er een gemeente voorkomen die er al in zit
            }
            return gemeentes;
        }
        #endregion
        #region ProvincieFactory
        public static List<Provincie> ProvincieFactory()
        {
            List<Provincie> provincies = new List<Provincie>();
            Dictionary<int, List<int>> gemeenteIDPerProvincie = Inlezer.ProvincieInfoParserGemeenteIDPerProvincie(Inlezer.FileReader("ProvincieInfo.csv"));//ProvincieID - gemeenteIDs
            Dictionary<int, string> provincieIDProvincienaam = Inlezer.ProvincieInfoParserProvincienamen(Inlezer.FileReader("ProvincieInfo.csv"));//provincieID-naam
            List<Gemeente> gemeentes = GemeenteFactory();
            foreach (var provincieID in gemeenteIDPerProvincie)
            {
            List<Gemeente> gemeentesInProvincie = new List<Gemeente>();    
                foreach (var gemeenteID in provincieID.Value) //geef voor elk provincieID de lijst van gemeenteIDs
                {
                    foreach (Gemeente gemeente in gemeentes) // ga door alle gemeentes 
                    {
                        if (gemeente.gemeenteID == gemeenteID) //&& !gemeentesInProvincie.Contains(gemeente)
                            gemeentesInProvincie.Add(gemeente);
                    }
                }
                provincies.Add(new Provincie(provincieID.Key, provincieIDProvincienaam[provincieID.Key], gemeentesInProvincie));
            }
            return provincies;
        }
        #endregion
    }
}
