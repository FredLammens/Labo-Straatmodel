using System.Collections.Generic;

namespace Labo
{
    class Gemeente
    {
        #region properties
        public int gemeenteID { get; private set; }
        public string naam { get; private set; }
        public List<Straat> straten { get; private set; }
        #endregion
        public Gemeente(int gemeenteID, string naam, List<Straat> straten) => (this.gemeenteID, this.naam, this.straten) = (gemeenteID, naam, straten);
    }
}