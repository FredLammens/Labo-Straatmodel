using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Labo
{
    [Serializable]
    public struct Segment : ISerializable
    {
        #region properties
        public Knoop beginKnoop { get; private set; }
        public Knoop eindKnoop { get; private set; }
        public int segmentID { get; private set; }
        public List<Punt> vertices { get; private set; }
        #endregion
        #region constructor
        public Segment(int segmentID, Knoop beginKnoop, Knoop eindKnoop, List<Punt> vertices)
        {
            this.segmentID = segmentID;
            this.beginKnoop = beginKnoop;
            this.eindKnoop = eindKnoop;
            this.vertices = vertices;
        }
        #endregion
        #region overridden methodes
        public override bool Equals(object obj)
        {
            return obj is Segment segment &&
                   EqualityComparer<Knoop>.Default.Equals(beginKnoop, segment.beginKnoop) &&
                   EqualityComparer<Knoop>.Default.Equals(eindKnoop, segment.eindKnoop) &&
                   segmentID == segment.segmentID &&
                   EqualityComparer<List<Punt>>.Default.Equals(vertices, segment.vertices);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(beginKnoop, eindKnoop, segmentID, vertices);
        }
        public override string ToString()
        {
            string toReturn = $"segment: {segmentID} heeft beginknoop: {beginKnoop} en eindknoop: {eindKnoop}, met vertices: \n ";
            foreach (Punt punt in vertices)
            {
                toReturn += punt.ToString();
            }
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
            info.AddValue("beginKnoop", beginKnoop);
            info.AddValue("eindKnoop", eindKnoop);
            info.AddValue("segmentID", segmentID);
            info.AddValue("vertices", vertices);
        }
        /// <summary>
        /// Deserializing constructor
        /// = remove object data from file 
        /// </summary>
        /// <param name="info">key value pair of stored data</param>
        /// <param name="context">meta-data</param>
        public Segment(SerializationInfo info, StreamingContext context)
        {
            //get values from info and assign them to properties
            beginKnoop = (Knoop)info.GetValue("beginKnoop", typeof(Knoop));
            eindKnoop = (Knoop)info.GetValue("eindKnoop", typeof(Knoop));
            segmentID = (int)info.GetValue("segmentID", typeof(int));
            vertices = (List<Punt>)info.GetValue("vertices", typeof(List<Punt>));
        }
        #endregion
    }
}
