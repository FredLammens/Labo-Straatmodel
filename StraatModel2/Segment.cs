using System;
using System.Collections.Generic;

namespace Labo
{
    class Segment
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
            return $"segmen : {segmentID} heeft beginknoop : {beginKnoop} en eindknoop : {eindKnoop} met eindknopen in vertices";
        }
        #endregion
    }
}
