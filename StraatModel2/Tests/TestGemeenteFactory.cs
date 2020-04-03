using Labo;
using System;
using System.Collections.Generic;
using System.Text;

namespace StraatModel2.Tests
{
    class TestGemeenteFactory
    {
        public TestGemeenteFactory()
        {
            List<Gemeente> gemeentes = Factories.GemeenteFactory();
            foreach (Gemeente gemeente in gemeentes)
            {
                if (gemeente.gemeenteNaam == null)
                {
                    throw new System.Exception("gemeentenaam fout");
                }
                foreach (Straat straat in gemeente.straten)
                {
                    //straat checken
                    if (straat == null)
                    {
                        throw new System.Exception("straat fout");
                    }
                    if (straat.straatnaam == null)
                    {
                        throw new System.Exception("straatnam fout");
                    }
                    //map van graaf checken
                    if (straat.graaf.map == null)
                    {
                        throw new System.Exception("map fout");
                    }
                    foreach (var pair in straat.graaf.map)
                    {
                        if (pair.Key == null)
                        {
                            throw new System.Exception("key(knoop) in map fout");
                        }
                        if (pair.Value == null)
                        {
                            throw new System.Exception("value (list segmenten) in map fout");
                        }
                        foreach (var segment in pair.Value)
                        {
                            if (segment.beginKnoop == null)
                            {
                                throw new System.Exception("beginknoop in segment in map fout");
                            }
                            if (segment.beginKnoop.punt == null)
                            {
                                throw new System.Exception("beginKnoop punt in segment in map fout");
                            }
                            if (segment.eindKnoop == null)
                            {
                                throw new System.Exception("eindknoop in segment in map fout");
                            }
                            if (segment.eindKnoop.punt == null)
                            {
                                throw new System.Exception("eindknoop punt in segment in map fout");
                            }//punten checken
                            foreach (var punt in segment.vertices)
                            {
                                if (punt == null)
                                {
                                    throw new System.Exception("punt in vertices in segment in map fout");
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
