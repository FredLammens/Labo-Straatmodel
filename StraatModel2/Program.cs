using System.Collections.Generic;

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
            //List<Straat> straten = Factories.StraatFactory();
            //foreach (Straat straat in straten)
            //{
            //    straat.showStraat();
            //}
            //List<Gemeente> gemeentes = Factories.GemeenteFactory();
            //foreach (Gemeente gemeente in gemeentes)
            //{
            //    System.Console.WriteLine(gemeente);
            //}
            //List<Provincie> provincies = Factories.ProvincieFactory();
            string testjeu = "1;LINESTRING (217368.75 181577.0159999989, 217400.1099999994 181499.5159999989);114;4;126722;41353;-9;-9";
            string[] testSTr = testjeu.Split(';'); ;
            Segment test = Inlezer.SegmentMaker(testSTr);
            System.Console.WriteLine(test);//t
        }
    }
}
