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
        public string naam { get; private set; }
        public List<Straat> straten { get; private set; }
        #endregion
        public Gemeente(int gemeenteID, string naam, List<Straat> straten) => (this.gemeenteID, this.naam, this.straten) = (gemeenteID, naam, straten);
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
            info.AddValue("naam", naam);
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
            gemeenteID  = (int)info.GetValue("gemeenteID", typeof(int));
            naam = (string)info.GetValue("naam", typeof(string));
            straten = (List<Straat>)info.GetValue("straten", typeof(List<Straat>));
        }
        #endregion
    }
}