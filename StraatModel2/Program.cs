using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Labo
{
    class Program
    {
        static void Main(string[] args)
        {
            //-------------------------------testparsers------------------------------------------------------------
            //Dictionary<int, List<int>> gemeenteIDs = Inlezer.WRgemeenteIDParser();
            //Dictionary<int, List<int>> gemeentePerProvincie = Inlezer.ProvincieInfoParserGemeenteIDPerProvincie();
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
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            //List<Straat> straten = Factories.StraatFactory();
            List<Gemeente> gemeentes = Factories.GemeenteFactory();
            stopWatch.Stop();
            //Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);
            Console.ReadLine();
            //foreach (Gemeente gemeente in gemeentes)
            //{
            //    System.Console.WriteLine(gemeente);
            //}
            //List<Provincie> provincies = Factories.ProvincieFactory();
        }
    }
}
