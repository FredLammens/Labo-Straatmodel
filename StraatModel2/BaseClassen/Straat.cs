using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
namespace Labo
{
    [Serializable]
    class Straat : ISerializable
    {
        #region properties
        public Graaf graaf { get; set; }
        public int straatId { get; set; }
        public string straatnaam { get; set; }
        #endregion
        #region methods
        public Straat(int straatID, string straatnaam, Graaf graaf) => (this.straatId, this.straatnaam, this.graaf) = (straatID, straatnaam, graaf);
        public void showStraat()
        {
            Console.WriteLine($"straat: {straatId} {straatnaam} heeft de graaf: \n");
            graaf.showGraaf();
        }
        public List<Knoop> getKnopen()
        {
            return graaf.getKnopen();
        }
        public double getLengte() 
        {
            return graaf.LengteGraaf();
        }
        #endregion
        #region overriden methods
        public override string ToString()
        {
            string toReturn = "-------------------------------------Straat----------------------------------------";
            toReturn = $"straat: {straatId} {straatnaam} heeft de graaf: \n";
            toReturn += graaf.ToString();
            toReturn += "-----------------------------------------------------------------------------";
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
            info.AddValue("straatId", straatId);
            info.AddValue("straatnaam", straatnaam);
            info.AddValue("graaf", graaf);
        }
        /// <summary>
        /// Deserializing constructor
        /// = remove object data from file 
        /// </summary>
        /// <param name="info">key value pair of stored data</param>
        /// <param name="context">meta-data</param>
        public Straat(SerializationInfo info, StreamingContext context)
        {
            //get values from info and assign them to properties
            straatId = (int)info.GetValue("straatId", typeof(int));
            straatnaam = (string)info.GetValue("straatnaam", typeof(string));
            graaf = (Graaf)info.GetValue("graaf", typeof(Graaf));
        }
        #endregion
    }
}
