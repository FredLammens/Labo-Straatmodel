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
        public static List<Straat> StraatFactory(string unziptPath)
        {
            List<Straat> straten = new List<Straat>();
            #region ingelezen
            Dictionary<int, string> WRstraatnamen = Inlezer.WRstraatNamenParser(unziptPath);
            Dictionary<int, List<Segment>> WRData = Inlezer.WRdataParser(unziptPath);
            Console.WriteLine("\nLoading straten maken: ");
            int teller = 0;
            #endregion
            foreach (KeyValuePair<int, string> straatnaam in WRstraatnamen)
            {
                //controleren of straatnamen en wrData overeenkomen
                if (WRstraatnamen.ContainsKey(straatnaam.Key) && WRData.ContainsKey(straatnaam.Key))
                {
                    Straat temp = new Straat(straatnaam.Key, straatnaam.Value, Graaf.buildGraaf(straatnaam.Key, WRData[straatnaam.Key]));
                    straten.Add(temp);
                    teller++;
                    if (teller > 50000)
                    {
                        teller = 0;
                        Console.Write("*");
                    }
                }
            }
            return straten;
        }
        #endregion
        #region GemeenteFactory
        /// <summary>
        /// gaat door gemeenteID.csv en WRgemeentenaam.csv en door de files van Straatfactory
        /// om een lijst van gemeentes terug te geven
        /// </summary>
        public static List<Gemeente> GemeenteFactory(string unziptPath)
        {
            List<Gemeente> gemeentes = new List<Gemeente>();
            #region ingelezen
            Dictionary<int, List<int>> gemeenteIDs = Inlezer.WRgemeenteIDParser(unziptPath);
            Dictionary<int, string> gemeentenamenPerId = Inlezer.WRgemeentenaamParser(unziptPath);
            List<Straat> alleStraten = StraatFactory(unziptPath);
            Console.WriteLine("\nLoading gemeentes: ");
            int teller = 0;
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
                        teller++;
                        if (teller > 6)
                        {
                            teller = 0;
                            Console.Write("*");

                        }
                    }
                }
            }
            return gemeentes;
        }
        #endregion
        #region ProvincieFactory
        public static List<Provincie> ProvincieFactory(string unziptPath)
        {
            List<Provincie> provincies = new List<Provincie>();
            try
            {
                #region inlezen
                Dictionary<int, List<int>> gemeenteIDPerProvincie = Inlezer.ProvincieInfoParserGemeenteIDPerProvincie(unziptPath);//ProvincieID - gemeenteIDs
                Dictionary<int, string> provincieIDProvincienaam = Inlezer.ProvincieInfoParserProvincienamen(unziptPath);//provincieID-naam
                List<Gemeente> gemeentes = GemeenteFactory(unziptPath);
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
            }
            ////straat
            catch (StraatnaamIdException) { }
            catch (WRIdException) { }
            ////gemeente
            catch (StraatNaamIdGemeenteException) { }
            catch (GemeenteIdGemeenteException) { }
            catch (GemeenteIdException) { }
            ////provincies
            catch (GemeenteIdProvincieException) { }
            catch (ProvincieIDException) { }
            catch (IOException io) { Console.WriteLine("Kan het bestand niet vinden: " + io.Message); }
            catch (Exception ex) { Console.WriteLine("Er is iets onverwacht foutgelopen: " + ex.Message); }
            return provincies;
        }
        #endregion
    }
}
