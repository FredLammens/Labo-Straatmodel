using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Labo
{
    [Serializable]
    class Graaf : ISerializable
    {
        #region properties
        public int graafID { get; private set; }
        public Dictionary<Knoop, List<Segment>> map { get; private set; } = new Dictionary<Knoop, List<Segment>>();
        #endregion
        #region constructor
        public Graaf(int graafID)
        {
            this.graafID = graafID;
        }
        #endregion
        #region methoden
        public static Graaf buildGraaf(int graafID, List<Segment> ingelezenSegmenten)
        {
            Graaf buildedGraaf;
            buildedGraaf = new Graaf(graafID);
            //1st map checken of er niets in zit 
            foreach (Segment segment in ingelezenSegmenten)
            {
                if (buildedGraaf.map.Count == 0) //als map leeg is 
                    buildedGraaf.map.Add(segment.beginKnoop, new List<Segment>() { segment });//lijst segmeneten initialiseren en dan segment insteken
                else
                {
                    if (buildedGraaf.map.ContainsKey(segment.beginKnoop)) //als straat opslitst.
                    {
                        buildedGraaf.map[segment.beginKnoop].Add(segment); //bij die key voeg nieuw segment aan toe
                    }
                    else //als hij het niet vindt. 
                    {
                        buildedGraaf.map.Add(segment.beginKnoop, new List<Segment>() { segment });
                    }
                }
            }
            return buildedGraaf;
        }
        public List<Knoop> getKnopen()
        {
            List<Knoop> knopen = new List<Knoop>();
            foreach (KeyValuePair<Knoop, List<Segment>> mapitem in map)
            {
                knopen.Add(mapitem.Key); //beginknoop
                foreach (var segment in mapitem.Value)
                {
                    knopen.Add(segment.eindKnoop);
                }
            }
            return knopen;
        }
        public void showGraaf()
        {
            System.Console.WriteLine($"graafID : {graafID}");
            foreach (var pair in map)
            {
                Console.WriteLine($"segmenten van beginknoop : {pair.Key}");
                foreach (var segment in pair.Value)
                {
                    Console.WriteLine(segment);
                }
            }
        }
        public double LengteGraaf()
        {
            double lengte = 0;
            foreach (KeyValuePair<Knoop, List<Segment>> mapitem in map)
            {
                foreach (Segment segment in mapitem.Value)
                {
                    for (int i = 0; i < segment.vertices.Count - 1; i++)
                    {
                        Punt punt1 = segment.vertices[i];
                        Punt punt2 = segment.vertices[i + 1];
                        lengte += Math.Sqrt(Math.Pow((punt2.x - punt1.x),2) + Math.Pow((punt2.y - punt1.y),2));
                    }
                }
            }
            return lengte;
        }
        public override string ToString()
        {
            string toReturn = "-------------------------------------Graaf------------------------------------------ \n";
            toReturn += $"GraafID : {graafID} \n";
            foreach (var pair in map)
            {
                toReturn += $"Segmenten van beginknoop : {pair.Key}\n";
                foreach (var segment in pair.Value)
                {
                    toReturn += segment.ToString();
                }
            }
            toReturn += "-------------------------------------------------------------------------------\n";
            return toReturn;
        }

        #endregion
        #region Serialize
        /// <summary>
        /// Serializing function that stores object data in file.
        /// </summary>
        /// <param name="info">key value pair of stored data</param>
        /// <param name="context">meta-data</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            //assign key to data
            info.AddValue("graafID", graafID);
            info.AddValue("map", map);
        }
        /// <summary>
        /// Deserializing constructor
        /// = remove object data from file 
        /// </summary>
        /// <param name="info">key value pair of stored data</param>
        /// <param name="context">meta-data</param>
        public Graaf(SerializationInfo info, StreamingContext context)
        {
            //get values from info and assign them to properties
            graafID = (int)info.GetValue("graafID", typeof(int));
            map = (Dictionary<Knoop, List<Segment>>)info.GetValue("map", typeof(Dictionary<Knoop, List<Segment>>));
        }
        #endregion
    }
}
