using System.Collections.Generic;

namespace Labo
{
    class Straat
    {
        #region properties
        public Graaf graaf { get; private set; }
        public int straatId { get; private set; }
        public string straatnaam { get; private set; }
        #endregion
        #region methods
        public Straat(int straatID, string straatnaam, Graaf graaf) => (this.straatId, this.straatnaam, this.graaf) = (straatID, straatnaam, graaf);
        public void showStraat()
        {
            System.Console.WriteLine($"straat : {straatId} {straatnaam} heeft de graaf :");
            graaf.showGraaf();
        }
        public List<Knoop> getKnopen()
        {
            return graaf.getKnopen();
        }
        #endregion
    }
}
