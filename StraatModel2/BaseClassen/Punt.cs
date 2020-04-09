using System;
using System.Runtime.Serialization;

namespace Labo
{
    [Serializable]
    class Punt : ISerializable
    {
        #region properties
        public double x { get; set; }
        public double y { get; set; }
        #endregion
        #region constructor
        public Punt(double x, double y) => (this.x, this.y) = (x, y);
        #endregion
        #region overridden methods
        public override string ToString()
        {
            return $"X :{x}, Y : {y}\n";
        }

        public override bool Equals(object obj)
        {
            return obj is Punt punt &&
                   x == punt.x &&
                   y == punt.y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(x, y);
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
            info.AddValue("x", x);
            info.AddValue("y", y);
        }
        /// <summary>
        /// Deserializing constructor
        /// = remove object data from file 
        /// </summary>
        /// <param name="info">key value pair of stored data</param>
        /// <param name="context">meta-data</param>
        public Punt(SerializationInfo info, StreamingContext context)
        {
            //get values from info and assign them to properties
            x = (double)info.GetValue("x", typeof(double));
            y = (double)info.GetValue("y", typeof(double));
        }
        #endregion
    }
}
