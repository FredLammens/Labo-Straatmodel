using Labo;
using System;
using System.Collections.Generic;
using System.Text;

namespace StraatModel2.Tests
{
    class TestParsers
    {
        public TestParsers(string parsernaam)
        {
            switch (parsernaam.ToLower())
            {
                case "wrgemeenteid":
                    WRGemeenteIDTester();
                    break;
                case "provincieinfo":
                    ProvincieInfoTester();
                    break;
                case "provincieid":
                    ProvincieIDsTester();
                    break;
                default:
                    break;
            }
        }
        public void WRGemeenteIDTester() 
        {
            Console.WriteLine("Testing WRGemeenteID.csv");
            Dictionary<int, List<int>> gemeenteIDs = Inlezer.WRgemeenteIDParser();
            //uitprinten
            foreach (var gemeenteID in gemeenteIDs)
            {
                System.Console.WriteLine("gemeenteID " + gemeenteID.Key);
                foreach (var straatID in gemeenteID.Value)
                {
                    System.Console.WriteLine("straatID " + straatID);
                }
            }
        }
        public void ProvincieInfoTester() 
        {
            Console.WriteLine("Testing provincieInfo.csv");
            Dictionary<int, List<int>> gemeentePerProvincie = Inlezer.ProvincieInfoParserGemeenteIDPerProvincie();//Inlezer.ProvincieInfoParserGemeenteIDPerProvincie();
            foreach (var provincie in gemeentePerProvincie)
            {
                System.Console.WriteLine("provincieID :" + provincie.Key);
                foreach (var gemeente in provincie.Value)
                {
                    System.Console.WriteLine("gemeenteID :" + gemeente);
                }
            }
        }
        public void ProvincieIDsTester() 
        {
            Console.WriteLine("Testing ProvincieID.csv");
            Dictionary<int, string> provincies = Inlezer.ProvincieInfoParserProvincienamen();
            List<int> provincieIDS = Inlezer.ProvincieIDsVlaanderenParser();
            foreach (var provincieId in provincies)
            {
                System.Console.WriteLine("ProvincieID :" + provincieId.Key);
                System.Console.WriteLine("provincienaam :" + provincieId.Value);
            }
            foreach (var provincieID in provincieIDS)
            {
                System.Console.WriteLine(provincieID);
            }
        }
    }
}
