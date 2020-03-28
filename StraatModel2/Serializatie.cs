using Labo;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
namespace StraatModel2
{
    class Serializatie
    {
        static public void Serialize() 
        {
        Provincie test = new Provincie(2, "Oost-Vlaanderen", new List<Gemeente> {new Gemeente(1,"testGemeente",new List<Straat> {new Straat(2,"Hippoliet",new Graaf(2)) } )});
        Stream stream = File.Open(@"C:\Users\Biebem\Downloads\provincie.dat", FileMode.Create);
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(stream, test);
            stream.Close();
        }
        static public void DeSerialize() 
        {
            Stream stream = File.Open(@"C:\Users\Biebem\Downloads\provincie.dat", FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();
            Provincie test = (Provincie)bf.Deserialize(stream);
            System.Console.WriteLine(test);
            stream.Close();
        }
    }
}
