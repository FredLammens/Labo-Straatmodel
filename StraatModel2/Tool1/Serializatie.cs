using Jil;
using Labo;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace StraatModel2
{
    class Serializatie
    {
        static public void SerializeProvinciesBinary(string unziptPath , string serializePath)
        {

            List<Provincie> provincies = Factories.ProvincieFactory(unziptPath);
            using (Stream stream = File.Open(@"" + serializePath + @"\provincies.dat", FileMode.Create))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(stream, provincies);
            }
            System.Console.WriteLine("File serialized in "+ serializePath+"\n");
        }
        static public List<Provincie> DeSerializeProvinciesBinary(string serializePath)
        {
            List<Provincie> provincies;
            using (Stream stream = File.Open(@""+ serializePath, FileMode.Open))
            {
                BinaryFormatter bf = new BinaryFormatter();
                provincies = (List<Provincie>)bf.Deserialize(stream);
            }
            return provincies;
        }
        #region test
        //static public void SerializeProvincieInXML()
        //{
        //    Provincie test = new Provincie(2, "Oost-Vlaanderen", new List<Gemeente> { new Gemeente(1, "testGemeente", new List<Straat> { new Straat(2, "Hippoliet", new Graaf(2)) }) });
        //    XmlSerializer serializer = new XmlSerializer(typeof(Provincie));
        //    using (TextWriter tw = new StreamWriter(@"C:\Users\Biebem\Downloads\provincie.xml"))
        //    {
        //        serializer.Serialize(tw, test);
        //    }
        //}
        //static public Provincie DeSerializeProvincieInXML() 
        //{
        //    Provincie test;
        //    XmlSerializer deserialiezr = new XmlSerializer(typeof(Provincie));
        //    using (TextReader tr = new StreamReader(@"C:\Users\Biebem\Downloads\provincie.xml")) 
        //    {
        //        test = (Provincie)deserialiezr.Deserialize(tr);
        //    }
        //    return test;
        //}
        //public static void SerializeProvinciesJSON(string unziptPath)
        //{
        //    List<Provincie> provincies = Factories.ProvincieFactory(unziptPath);
        //    using (StreamWriter sw = new StreamWriter(@"C:\Users\Biebem\Downloads\provincies.JSON"))
        //    {
        //        JSON.Serialize(provincies, sw);
        //    }
        //}
        //static public void SerializeStratenJSON(string unziptPath)
        //{
        //    List<Straat> straten = Factories.StraatFactory(unziptPath);
        //    using (StreamWriter sw = new StreamWriter(@"C:\Users\Biebem\Downloads\straten.JSON"))
        //    {
        //        JSON.Serialize(straten, sw);
        //    }
        //}
        //static public void SerializeGemeentesJSON(string unziptPath)
        //{
        //    List<Gemeente> gemeentes = Factories.GemeenteFactory(unziptPath);
        //    using (StreamWriter sw = new StreamWriter(@"C:\Users\Biebem\Downloads\gemeentes.JSON"))
        //    {
        //        JSON.Serialize(gemeentes, sw);
        //    }
        //}
        #endregion

    }
}
