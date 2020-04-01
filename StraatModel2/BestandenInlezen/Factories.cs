using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

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
            //try
            //{
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
                    Straat temp = new Straat(straatnaam.Key, straatnaam.Value, Graaf.buildGraaf(straatnaam.Key, WRData[straatnaam.Key]));
                    straten.Add(temp);
                    Console.WriteLine($"straat {straatnaam.Value} aangemaakt. \n");
                }
            }
            Console.WriteLine("klaar met straten maken.");
            //}
            //catch (IDException) { }
            //catch (StraatnaamIdException) { }
            //catch (WRIdException) { }
            //catch (Exception) { Console.WriteLine("Er is iets onverwacht foutgelopen."); }
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
            //try
            //{
            Dictionary<int, List<int>> gemeenteIDs = Inlezer.WRgemeenteIDParser();
            Dictionary<int, string> gemeentenamenPerId = Inlezer.WRgemeentenaamParser();
            List<Straat> alleStraten = StraatFactory();
            #endregion    //gemeenteID //straatnaamIDs
            foreach (KeyValuePair<int, List<int>> gemeenteId in gemeenteIDs)
            {
                if (gemeentenamenPerId.ContainsKey(gemeenteId.Key)) // of gemeentenaam bestaat
                {
                    #region alle straten uit gemeente
                    List<Straat> straten = new List<Straat>();
                    Parallel.ForEach(gemeenteId.Value, (straatnaamID) => //eerst door alle stratenIDs in de gemeente => kleinste foreach zoveel mogelijk boven : 3^5 < 5^3 foreach (int straatnaamID in gemeenteId.Value)
                    {
                        foreach (Straat straat in alleStraten) //door alle straten
                        {
                            if (straatnaamID == straat.straatId)
                            {
                                lock (straten)
                                {
                                    straten.Add(straat); //als de straat in de gemeentevoorkomt toevoegen aan lijst van straten
                                }
                            }
                        }
                    });
                    #endregion
                    if (straten.Count != 0)
                    {//straten mag niet leeg zijn                     
                        gemeentes.Add(new Gemeente(gemeenteId.Key, gemeentenamenPerId[gemeenteId.Key], straten)); // straten toevoegen aan gemeente en deze aan de lijst van alle gemeentes
                        Console.WriteLine($"gemeente : {gemeenteId.Key} added");
                    }
                }
            }
            //}
            //catch (StraatNaamIdGemeenteException) { }
            //catch (GemeenteIdGemeenteException) { }
            //catch (GemeenteIdException) { }
            //catch (Exception) { Console.WriteLine("Er is iets onverwacht foutgelopen."); }
            return gemeentes;
        }
        #endregion
        #region ProvincieFactory
        public static List<Provincie> ProvincieFactory()
        {
            List<Provincie> provincies = new List<Provincie>();
            //try
            //{
            #region inlezen
            Dictionary<int, List<int>> gemeenteIDPerProvincie = Inlezer.ProvincieInfoParserGemeenteIDPerProvincie(Inlezer.FileReader("ProvincieInfo.csv"));//ProvincieID - gemeenteIDs
            Dictionary<int, string> provincieIDProvincienaam = Inlezer.ProvincieInfoParserProvincienamen(Inlezer.FileReader("ProvincieInfo.csv"));//provincieID-naam
            List<Gemeente> gemeentes = GemeenteFactory();
            #endregion
            foreach (var provincieID in gemeenteIDPerProvincie)
            {
                if (provincieIDProvincienaam.ContainsKey(provincieID.Key))
                {
                    List<Gemeente> gemeentesInProvincie = new List<Gemeente>();
                    foreach (var gemeenteID in provincieID.Value) //geef voor elk provincieID de lijst van gemeenteIDs
                    {
                        foreach (Gemeente gemeente in gemeentes) // ga door alle gemeentes 
                        {
                            if (gemeente.gemeenteID == gemeenteID) //&& !gemeentesInProvincie.Contains(gemeente)
                            {
                                gemeentesInProvincie.Add(gemeente);
                            }
                        }
                    }
                    provincies.Add(new Provincie(provincieID.Key, provincieIDProvincienaam[provincieID.Key], gemeentesInProvincie));
                    Console.WriteLine($"provincie : {provincieID.Key} toegevoegd");
                }
            }
            //}
            //catch (gemeenteIdProvincieException) { }
            //catch (ProvincieIDException) { }
            //catch (Exception) { Console.WriteLine("Er is iets onverwacht foutgelopen."); }
            return provincies;
        }
        #endregion
    }
}
