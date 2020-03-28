using System;

namespace Labo
{
    class IDException : Exception
    {
        public IDException()
        {
            Console.WriteLine("ID is verkeerd geparsed of is invalid");
        }
    }
    class WRDataException : Exception
    {
        public WRDataException()
        {
            Console.WriteLine("Er is iets foutgelopen bij het parsen van het wegsegment");
        }
    }
    class WRIdException : WRDataException
    {
        public WRIdException()
        {
            Console.Write("Er is iets foutgelopen bij het parsen van het ID");
        }
    }
    class DoubleException : WRDataException
    {
        public DoubleException()
        {
            Console.Write("kon knoopwaarde niet omzetten");
        }
    }
    class StraatnaamIdException : WRDataException
    {
        public StraatnaamIdException()
        {
            Console.WriteLine("probleem bij het omzetten van de straatnaamID");
        }
    }
    class GemeenteNaamFileException : Exception
    {
        public GemeenteNaamFileException()
        {
            Console.WriteLine("er is een probleem bij het inlezen van gemeentenaam.csv");
        }
    }
    class GemeenteIdException : GemeenteNaamFileException
    {
        public GemeenteIdException()
        {
            Console.WriteLine("Probleem bij het parsen van de gemeenteID");
        }
    }
    class GemeenteIdFileException : Exception
    {
        public GemeenteIdFileException()
        {
            Console.WriteLine("Er is een probleem bij het inlezen van GemeenteID.csv");
        }
    }
    class StraatNaamIdGemeenteException : GemeenteIdFileException
    {
        public StraatNaamIdGemeenteException()
        {
            Console.WriteLine("Er is een probleem bij het inlezen van straatnaamID");
        }
    }
    class GemeenteIdGemeenteException : GemeenteIdFileException
    {
        public GemeenteIdGemeenteException()
        {
            Console.WriteLine("Er is een probleem bij het inlezen van gemeenteID");
        }
    }
    class ProvincieInfoFileException : Exception
    {
        public ProvincieInfoFileException()
        {
            Console.WriteLine("Er is een probleem bij het inlezen van ProvincieInfo.csv");
        }
    }
    class gemeenteIdProvincieException : ProvincieInfoFileException
    {
        public gemeenteIdProvincieException()
        {
            Console.WriteLine("Er is een probleem bij het inlezen van gemeenteId");
        }
    }
    class ProvincieIdException : ProvincieInfoFileException
    {
        public ProvincieIdException()
        {
            Console.WriteLine("Er is een probleem bij het inlezen van provincieId");
        }
    }
    class ProvincieIDFileException : Exception
    {
        public ProvincieIDFileException()
        {
            Console.WriteLine("probleem bij het inlezen van ProvincieIDsVlaanderen.csv");
        }
    }
    class ProvincieIDException : ProvincieIDFileException
    {
        public ProvincieIDException()
        {
            Console.WriteLine("fout bij het parsen van provincieID");
        }
    }
    class StraatFactoryException : Exception
    {
        public StraatFactoryException()
        {
            Console.WriteLine("Er is iets fout gebeurd bij de StraatFactory");
        }
    }
    class STraatNietInDataException : StraatFactoryException
    {
        public STraatNietInDataException()
        {
            Console.WriteLine("straatnaam zit niet in WRdata \n");
        }
    }

}
