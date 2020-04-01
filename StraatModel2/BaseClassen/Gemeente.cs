using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Labo
{
    [Serializable]
    class Gemeente : ISerializable
    {
        #region properties
        public int gemeenteID { get; private set; }
        public string gemeenteNaam { get; private set; }
        public List<Straat> straten { get; private set; }
        #endregion
        public Gemeente(int gemeenteID, string naam, List<Straat> straten) => (this.gemeenteID, this.gemeenteNaam, this.straten) = (gemeenteID, naam, straten);
        public override string ToString()
        {
            string toReturn = "-------------------------------------Gemeente----------------------------------------";
            toReturn = $"Gemeente: {gemeenteID} {gemeenteNaam} heeft de straat: \n";
            foreach (Straat straat in straten)
            {
                toReturn += straat.ToString();
            }
            toReturn += "-----------------------------------------------------------------------------";
            return toReturn;
        }
        #region Serialize
        /// <summary>
        /// Serializing function that stores object data in file.
        /// </summary>
        /// <param name="info">key value pair of stored data</param>
        /// <param name="context">meta-data</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            //assign key to data
            info.AddValue("gemeenteID", gemeenteID);
            info.AddValue("gemeenteNaam", gemeenteNaam);
            info.AddValue("straten", straten);
        }
        /// <summary>
        /// Deserializing constructor
        /// = remove object data from file 
        /// </summary>
        /// <param name="info">key value pair of stored data</param>
        /// <param name="context">meta-data</param>
        public Gemeente(SerializationInfo info, StreamingContext context)
        {
            //get values from info and assign them to properties
            gemeenteID = (int)info.GetValue("gemeenteID", typeof(int));
            gemeenteNaam = (string)info.GetValue("gemeenteNaam", typeof(string));
            straten = (List<Straat>)info.GetValue("straten", typeof(List<Straat>));
        }
        #endregion
    }
}