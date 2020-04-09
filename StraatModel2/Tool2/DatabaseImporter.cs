using Labo;
using System;
using System.Collections.Generic;
using System.Text;

namespace StraatModel2.Tool2
{
    class DatabaseImporter
    {
        private readonly string connectionString;
        public DatabaseImporter(string connectionString) => (this.connectionString) = (connectionString);
        public void Insert(List<Provincie> provincies) 
        {
            //waarden die in databanken moeten
            List<Punt> punten = new List<Punt>();
            List<Knoop> knopen = new List<Knoop>();
            List<Segment> segmenten = new List<Segment>();
            List<Graaf> graven = new List<Graaf>();
            List<Straat> straten = new List<Straat>();
            List<Gemeente> gemeentes = new List<Gemeente>();




        }
    }
}
