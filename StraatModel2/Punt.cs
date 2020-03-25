using System;

namespace Labo
{
    class Punt
    {
        #region properties
        public double x { get; private set; }
        public double y { get; private set; }
        #endregion
        #region constructor
        public Punt(double x, double y) => (this.x, this.y) = (x, y);
        #endregion
        #region overridden methods
        public override string ToString()
        {
            return $"X :{x}, Y : {y}";
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
    }
}
