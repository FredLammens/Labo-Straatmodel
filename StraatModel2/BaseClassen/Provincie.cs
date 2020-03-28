using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
namespace Labo
{
    [Serializable]
    class Provincie : ISerializable
    {
        #region properties
        public int provincieID { get; private set; }
        public string provincieNaam { get; private set; }
        public List<Gemeente> gemeentes { get; private set; }
        #endregion
        public Provincie(int provincieID, string provincieNaam, List<Gemeente> gemeentes) => (this.provincieID, this.provincieNaam, this.gemeentes) = (provincieID, provincieNaam, gemeentes);
        #region Serialize
        /// <summary>
        /// Serializing function that stores object data in file.
        /// </summary>
        /// <param name="info">key value pair of stored data</param>
        /// <param name="context">meta-data</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            //assign key to data
            info.AddValue("provincieNaam", provincieNaam);
            info.AddValue("provincieID", provincieID);
            info.AddValue("gemeentes", gemeentes);
        }
        /// <summary>
        /// Deserializing constructor
        /// = remove object data from file 
        /// </summary>
        /// <param name="info">key value pair of stored data</param>
        /// <param name="context">meta-data</param>
        public Provincie(SerializationInfo info, StreamingContext context)
        {
            //get values from info and assign them to properties
            provincieID = (int)info.GetValue("provincieID", typeof(int));
            provincieNaam = (string)info.GetValue("provincieID", typeof(string));
            gemeentes = (List<Gemeente>)info.GetValue("gemeentes", typeof(List<Gemeente>));
        }
        #endregion
    }
}