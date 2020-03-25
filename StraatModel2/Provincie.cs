using System.Collections.Generic;

namespace Labo
{
    class Provincie
    {
        #region properties
        public int provincieID { get; private set; }
        public string provincieNaam { get; private set; }
        public List<Gemeente> gemeentes { get; private set; }
        #endregion
        public Provincie(int provincieID, string provincieNaam, List<Gemeente> gemeentes) => (this.provincieID, this.provincieNaam, this.gemeentes) = (provincieID, provincieNaam, gemeentes);
    }
}