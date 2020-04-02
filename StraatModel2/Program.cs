using StraatModel2;
using System.Collections.Generic;

namespace Labo
{
    class Program
    {
        static void Main(string[] args)
        {
            //-------------------------------testparsers------------------------------------------------------------
            //Dictionary<int, string> Straatnamen = Inlezer.WRstraatNamenParser();
            //Dictionary<int, List<int>> gemeenteIDs = Inlezer.WRgemeenteIDParser();
            //Dictionary<int, List<int>> gemeentePerProvincie = Inlezer.ProvincieInfoParserGemeenteIDPerProvincie(Inlezer.FileReader());
            //Dictionary<int, string> provincies = Inlezer.ProvincieInfoParserProvincienamen();
            //List<int> provincieIDS = Inlezer.ProvincieIDsVlaanderenParser();
            //uitprinten
            //foreach (var gemeenteID in gemeenteIDs)
            //{
            //    System.Console.WriteLine("gemeenteID " + gemeenteID.Key);
            //    foreach (var straatID in gemeenteID.Value)
            //    {
            //        System.Console.WriteLine("straatID " + straatID);
            //    }
            //}
            //foreach (var provincie in gemeentePerProvincie)
            //{
            //    System.Console.WriteLine("provincieID :" + provincie.Key);
            //    foreach (var gemeente in provincie.Value)
            //    {
            //        System.Console.WriteLine("gemeenteID :" + gemeente);
            //    }
            //}
            //foreach (var provincieId in provincies)
            //{
            //    System.Console.WriteLine("ProvincieID :" + provincieId.Key);
            //    System.Console.WriteLine("provincienaam :" + provincieId.Value);
            //}
            //foreach (var provincieID in provincieIDS)
            //{
            //    System.Console.WriteLine(provincieID);
            //}
            //-------------------------------testFactories------------------------------------------------------------
            //--------------------stopwatch-------------------------------------------------
            //Stopwatch stopWatch = new Stopwatch();
            //stopWatch.Start();
            ////List<Straat> straten = Factories.StraatFactory();
            ////List<Gemeente> gemeentes = Factories.GemeenteFactory();
            //List<Provincie> provincies = Factories.ProvincieFactory();
            //stopWatch.Stop();
            ////Get the elapsed time as a TimeSpan value.
            //TimeSpan ts = stopWatch.Elapsed;
            //string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            //ts.Hours, ts.Minutes, ts.Seconds,
            //ts.Milliseconds / 10);
            //Console.WriteLine("RunTime " + elapsedTime);
            //Console.ReadLine();
            //-----------stopwatch--------------------------------------------------------
            //foreach (Gemeente gemeente in gemeentes)
            //{
            //    System.Console.WriteLine(gemeente);
            //}
            //Serializatie.SerializeProvinciesInBinary();
            //foreach (var item in Serializatie.DeSerializeProvinciesBinary())
            //{
            //    System.Console.WriteLine(item.provincieNaam);
            //}
            //try
            //{
            //    foreach (Provincie provincie in provincies)
            //    {
            //        foreach (Gemeente gemeente in provincie.gemeentes)
            //        {
            //            foreach (Straat straat in gemeente.straten)
            //            {
            //                if (straat == null)
            //                    throw new Exception("wat is happening ");
            //            }
            //        }
            //    }
            //}
            //catch (Exception e) 
            //{
            //    Console.Clear();
            //    Console.WriteLine(e);
            //    Console.WriteLine(e); Console.WriteLine(e); Console.WriteLine(e); Console.WriteLine(e); Console.WriteLine(e); Console.WriteLine(e); Console.WriteLine(e); Console.WriteLine(e); Console.WriteLine(e);
            //    Console.WriteLine(e); Console.WriteLine(e); Console.WriteLine(e); Console.WriteLine(e); Console.WriteLine(e); Console.WriteLine(e); Console.WriteLine(e); Console.WriteLine(e); Console.WriteLine(e);
            //}
            //Serializatie.SerializeProvinciesJSON();
            //Serializatie.SerializeGemeentesJSON();
            //bool nuller = straten.Contains(null);
            //Console.WriteLine(nuller);
            //List<Gemeente> gemeentes = Factories.GemeenteFactory();
            //foreach (Gemeente gemeente in gemeentes)
            //{
            //    if (gemeente.gemeenteNaam == null)
            //    {
            //        throw new System.Exception("gemeentenaam fout");
            //    }
            //    foreach (Straat straat in gemeente.straten)
            //    {
            //        //straat checken
            //        if (straat == null)
            //        {
            //            throw new System.Exception("straat fout");
            //        }
            //        if (straat.straatnaam == null)
            //        {
            //            throw new System.Exception("straatnam fout");
            //        }
            //        //map van graaf checken
            //        if (straat.graaf.map == null)
            //        {
            //            throw new System.Exception("map fout");
            //        }
            //        foreach (var pair in straat.graaf.map)
            //        {
            //            if (pair.Key == null)
            //            {
            //                throw new System.Exception("key(knoop) in map fout");
            //            }
            //            if (pair.Value == null)
            //            {
            //                throw new System.Exception("value (list segmenten) in map fout");
            //            }
            //            foreach (var segment in pair.Value)
            //            {
            //                if (segment.beginKnoop == null)
            //                {
            //                    throw new System.Exception("beginknoop in segment in map fout");
            //                }
            //                if (segment.beginKnoop.punt == null)
            //                {
            //                    throw new System.Exception("beginKnoop punt in segment in map fout");
            //                }
            //                if (segment.eindKnoop == null)
            //                {
            //                    throw new System.Exception("eindknoop in segment in map fout");
            //                }
            //                if (segment.eindKnoop.punt == null)
            //                {
            //                    throw new System.Exception("eindknoop punt in segment in map fout");
            //                }//punten checken
            //                foreach (var punt in segment.vertices)
            //                {
            //                    if (punt == null)
            //                    {
            //                        throw new System.Exception("punt in vertices in segment in map fout");
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
            //System.Console.WriteLine(Rapport.MaakRapport(Factories.ProvincieFactory()));
            //Serializatie.SerializeProvinciesJSON();
            Factories.StraatFactory();
        }
    }
}
