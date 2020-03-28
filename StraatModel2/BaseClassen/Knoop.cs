using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Labo
{
    [Serializable]
    class Knoop : ISerializable
    {
        #region properties
        public int knoopId { get; private set; }
        public Punt punt { get; private set; }
        #endregion
        #region constructor
        public Knoop(int knoopID, Punt punt)
        {
            this.knoopId = knoopID;
            this.punt = punt;
        }

        #endregion
        #region overridden methods
        public override bool Equals(object obj)
        {
            return obj is Knoop knoop &&
                   knoopId == knoop.knoopId &&
                   EqualityComparer<Punt>.Default.Equals(punt, knoop.punt);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(knoopId, punt);
        }

        public override string ToString()
        {
            return $"knoopID : {knoopId} met punt : {punt}";
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
            info.AddValue("knoopId", knoopId);
            info.AddValue("punt", punt);
        }
        /// <summary>
        /// Deserializing constructor
        /// = remove object data from file 
        /// </summary>
        /// <param name="info">key value pair of stored data</param>
        /// <param name="context">meta-data</param>
        public Knoop(SerializationInfo info, StreamingContext context)
        {
            //get values from info and assign them to properties
            knoopId = (int)info.GetValue("knoopId", typeof(int));
            punt = (Punt)info.GetValue("punt", typeof(Punt));
        }
        #endregion
    }
}
