using System;
using System.Collections.Generic;

namespace Labo
{
    class Knoop
    {
        #region properties
        public int knoopId { get; private set; }
        public Punt punt { get; private set; }
        #endregion
        #region constructor
        public Knoop(int knoopID, Punt punt)
        {
            this.knoopId = knoopId;
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
    }
}
