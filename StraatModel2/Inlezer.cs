using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace Labo
{
    class Inlezer
    {
        #region Startmethoden
        /// <summary>
        /// Unzipt WRdata-master 
        /// voorlopig gelocaliseerd in users\Biebem\downloads
        /// moet je nog kunnen kiezen en moet path returnen .
        /// </summary>
        private static void Unzipper()
        {
            ZipFile.ExtractToDirectory(@"C:\Users\Biebem\Downloads" + @"\WRdata-master.zip", @"C:\Users\Biebem\Downloads");
            ZipFile.ExtractToDirectory(@"C:\Users\Biebem\Downloads" + @"\WRdata-master\WRdata.zip", @"C:\Users\Biebem\Downloads\WRdata-master");
            ZipFile.ExtractToDirectory(@"C:\Users\Biebem\Downloads" + @"\WRdata-master\WRstraatnamen.zip", @"C:\Users\Biebem\Downloads\WRdata-master");
            //unzipte bestanden verwijderen worden.
            File.Delete(@"C:\Users\Biebem\Downloads" + @"\WRdata-master\WRdata.zip");
            File.Delete(@"C:\Users\Biebem\Downloads" + @"\WRdata-master\WRstraatnamen.zip");
        }
        /// <summary>
        /// leest lijn per lijn en geeft lijst van lijnen terug opgesplitst door delimeter.
        /// indien niets ingevult is de standaard delimeter = ;
        /// </summary>
        /// <param name="fileName">bestandsnaam</param>
        /// <param name="delimeter">character voor opslitsing</param>
        /// <returns></returns>
        public static List<string[]> FileReader(string fileName, char delimeter = ';')
        {
            //string path = Unzipper();
            string path = @"C:\Users\Biebem\Downloads\WRdata-master\" + fileName;
            List<string[]> lines = new List<string[]>();
            using (FileStream fs = File.Open(path + @"", FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (BufferedStream bs = new BufferedStream(fs))
            using (StreamReader sr = new StreamReader(bs))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    string[] splitted = s.Split(delimeter);
                    lines.Add(splitted);
                }
            }
            Console.WriteLine("File : {0} read", fileName);
            return lines;
        }
        #endregion
        #region WRstraatnamen
        /// <summary>
        /// Method voor parsen van WRstraatnamen.csv
        /// returned straatID met bijhorende straatnaam
        /// </summary>
        /// <returns></returns>
        public static Dictionary<int, string> WRstraatNamenParser()
        {
            List<string[]> FileSplitted = FileReader("WRstraatnamen.csv");
            Dictionary<int, string> splittedLines = new Dictionary<int, string>();
            foreach (string[] line in FileSplitted.Skip(2))
            {
                splittedLines.Add(IDchecker(line[0]), line[1]);
            }
            Console.WriteLine("straatnamen in dictionary geparsed.");
            return splittedLines;
        }

        /// <summary>
        /// parst id en checkt of het niet gelijk is aan -9 of geeft bijhorende exception terug
        /// </summary>
        /// <param name="id">id die geparst moet worden</param>
        /// <returns></returns>
        private static int IDchecker(string id)
        {
            if (int.TryParse(id, out int intID) & intID != -9) // niet && want anders short-circuit
            {
                return intID;
            }
            else
            {
                throw new IDException();
            }
            throw new Exception();
        }
        #endregion
        #region WRData
        /// <summary>
        /// Method voor parsen van WRdata.csv
        /// returned straatnaamIDs (zowel links als rechts) met elk een Lijst van segmenten die deze straat bevat
        /// </summary>
        /// <returns>Dictionary met key : straatnaamID en value : Lijst van segmenten</returns>
        public static Dictionary<int, List<Segment>> WRdataParser()
        {
            List<string[]> WRDataSplitted = FileReader("WRdata.csv");//idem uit straatmaker
            Console.WriteLine("Start parsen van WRdata.csv"); //tonen op console
            Dictionary<int, List<Segment>> segmentenDic = new Dictionary<int, List<Segment>>();
            foreach (string[] line in WRDataSplitted.Skip(1))//eerste lijn wegsmijten
            {
                if (!int.TryParse(line[6], out int linksStraatnaamID))
                    throw new StraatnaamIdException();
                if (!int.TryParse(line[7], out int rechtsStraatnaamID))
                    throw new StraatnaamIdException();
                if (linksStraatnaamID == rechtsStraatnaamID)
                {
                    if (linksStraatnaamID != -9)
                    {
                        //controleren of al in dictionary zit zoja voeg toe
                        if (segmentenDic.ContainsKey(linksStraatnaamID))
                        {
                            segmentenDic[linksStraatnaamID].Add(SegmentMaker(line));
                        }
                        else //zonee maak nieuwe item aan.
                            segmentenDic.Add(linksStraatnaamID, new List<Segment>() { SegmentMaker(line) });
                    }
                }
                else
                {
                    if (linksStraatnaamID == -9 || rechtsStraatnaamID == -9) // is 1 van de 2 -9
                    {
                        if (linksStraatnaamID == -9)
                        {
                            //controleren of al in dictionary zit zoja voeg toe
                            if (segmentenDic.ContainsKey(rechtsStraatnaamID))
                            {
                                segmentenDic[rechtsStraatnaamID].Add(SegmentMaker(line));
                            }
                            else //zonee maak nieuwe item aan.
                                segmentenDic.Add(rechtsStraatnaamID, new List<Segment>() { SegmentMaker(line) });
                        }
                        else if (rechtsStraatnaamID == -9)
                        {
                            //controleren of al in dictionary zit zoja voeg toe
                            if (segmentenDic.ContainsKey(linksStraatnaamID))
                            {
                                segmentenDic[linksStraatnaamID].Add(SegmentMaker(line));
                            }
                            else //zonee maak nieuwe item aan.
                                segmentenDic.Add(linksStraatnaamID, new List<Segment>() { SegmentMaker(line) });
                        }
                    }
                    else
                    {
                        //controleren of al in dictionary zit zoja voeg toe
                        if (segmentenDic.ContainsKey(linksStraatnaamID))
                        {
                            segmentenDic[linksStraatnaamID].Add(SegmentMaker(line));
                        }
                        else //zonee maak nieuwe items aan.
                        {
                            segmentenDic.Add(linksStraatnaamID, new List<Segment>() { SegmentMaker(line) });
                        }
                        if (segmentenDic.ContainsKey(rechtsStraatnaamID))
                        {
                            segmentenDic[rechtsStraatnaamID].Add(SegmentMaker(line));
                        }
                        else //zonee maak nieuwe items aan.
                        {
                            segmentenDic.Add(rechtsStraatnaamID, new List<Segment>() { SegmentMaker(line) });
                        }
                    }
                }
            }
            return segmentenDic;
        }
        /// <summary>
        /// Hulpmethod voor WRdataParser
        /// analyseert lijnen in strings en geeft bijhorende segment terug
        /// </summary>
        /// <param name="line">Lines parsed from file</param>
        /// <returns></returns>
        public static Segment SegmentMaker(String[] line)
        {
            Console.WriteLine("Start met segment maken");//tonen op console
            List<Punt> vertices = new List<Punt>();
            Knoop beginknoop;
            Knoop eindknoop;
            //SegmentID
            if (!int.TryParse(line[0], out int wegsegmentID))
                throw new WRIdException();
            //vertices = allePunten(incl begin/eind)
            string tempVerwijderEerste = line[1].Remove(0, 12); // verwijdert LINESTRING (
            string tempVerwijderLaatst = tempVerwijderEerste.Remove(tempVerwijderEerste.Length - 1);
            string[] puntenTuples = tempVerwijderLaatst.Split(", ");
            List<string[]> punten = new List<string[]>();
            foreach (string punt in puntenTuples)
            {
                punten.Add(punt.Split(" "));
            }
            for (int i = 0; i < punten.Count; i++)
            {
                if (!double.TryParse(punten[i][0], out double x))
                    throw new DoubleException();
                if (!double.TryParse(punten[i][1], out double y))
                    throw new DoubleException();
                vertices.Add(new Punt(x, y));
            }
            //beginknoop
            if (!int.TryParse(line[4], out int beginknoopID))
                throw new WRIdException();
            beginknoop = new Knoop(beginknoopID, vertices.First());
            //eindknoop
            if (!int.TryParse(line[5], out int eindknoopID))
                throw new WRIdException();
            eindknoop = new Knoop(eindknoopID, vertices.Last());
            //
            Console.WriteLine("Segment gemaakt"); //tonen op console
            return new Segment(wegsegmentID, beginknoop, eindknoop, vertices);
        }
        #endregion
        #region WRgemeentenaam
        /// <summary>
        /// Method voor parsen van WRgemeentenaam.csv
        /// returned gemeenteID met bijhorende gemeenteNaam
        /// </summary>
        /// <returns>gemeenteID met bijhorende gemeenteNaam</returns>
        public static Dictionary<int, string> WRgemeentenaamParser()
        {
            List<string[]> FileSplitted = FileReader("WRgemeentenaam.csv");
            Dictionary<int, string> gemeentenamen = new Dictionary<int, string>();
            foreach (string[] line in FileSplitted)
            {
                if (line[2] == "nl") //op taalcode filteren
                {
                    if (!int.TryParse(line[1], out int gemeenteId))
                        throw new GemeenteIdException();
                    gemeentenamen.Add(gemeenteId, line[3]);
                }
            }
            return gemeentenamen;
        }
        #endregion
        #region WRgemeenteID
        /// <summary>
        /// Method voor gemeenteID.csv te parsen
        /// returned gemeenteId met bijhorende lijst van straatnaamIDs
        /// </summary>
        /// <returns></returns>
        public static Dictionary<int, List<int>> WRgemeenteIDParser()
        {
            List<string[]> FileSplitted = FileReader("WRgemeenteID.csv");
            Dictionary<int, List<int>> gemeenteIDs = new Dictionary<int, List<int>>();
            foreach (string[] line in FileSplitted.Skip(1))
            {
                if (!int.TryParse(line[0], out int straatNaamId))
                    throw new StraatNaamIdGemeenteException();
                if (!int.TryParse(line[1], out int gemeenteId))
                    throw new GemeenteIdGemeenteException();
                if (gemeenteIDs.ContainsKey(gemeenteId)) //als het al in dictionary zit toevoegen aan lijst
                    gemeenteIDs[gemeenteId].Add(straatNaamId);
                else
                    gemeenteIDs.Add(gemeenteId, new List<int>() { straatNaamId });
            }
            Console.WriteLine("WRgemeenteID is geparsed");
            return gemeenteIDs;
        }
        #endregion
        #region ProvincieInfo
        /// <summary>
        /// Method 1/2 voor ProvincieInfo.csv te parsen.
        /// returned provincieID met lijst van gemeenteIDs
        /// </summary>
        /// <param name="FileSplitted"> Lijnen van Provincieinfo.csv gesplit</param>
        /// <returns></returns>
        public static Dictionary<int, List<int>> ProvincieInfoParserGemeenteIDPerProvincie(List<string[]> FileSplitted)
        {
            //List<string[]> FileSplitted = FileReader("ProvincieInfo.csv"); // kan in factory gestoken worden omdat de 2 provincieinfoparsers deze gebruiken .
            Dictionary<int, List<int>> gemeenteIDperProvincie = new Dictionary<int, List<int>>();
            foreach (string[] line in FileSplitted)
            {
                if (line[2] == "nl")
                {
                    if (!int.TryParse(line[0], out int gemeenteId))
                        throw new gemeenteIdProvincieException();
                    if (!int.TryParse(line[1], out int provincieId))
                        throw new ProvincieIdException();
                    if (gemeenteIDperProvincie.ContainsKey(provincieId))//als het in dictionary zit toevoegen aan lijst
                        gemeenteIDperProvincie[provincieId].Add(gemeenteId);
                    else
                        gemeenteIDperProvincie.Add(provincieId, new List<int>() { gemeenteId });
                }
                //else
                //Console.WriteLine("provincie != nl");
            }
            Console.WriteLine("ProvincieID - gemeenteID geparsed");
            return gemeenteIDperProvincie;
        }
        /// <summary>
        /// Method 2/2 voor ProvincieInfo.csv te parsen
        /// returned provincieID met bijhorende provincienaam
        /// </summary>
        /// <param name="FileSplitted">Lijnen van Provincieinfo.csv gesplit</param>
        /// <returns></returns>
        public static Dictionary<int, string> ProvincieInfoParserProvincienamen(List<string[]> FileSplitted)
        {
            List<int> provincieIDsVlaanderen = ProvincieIDsVlaanderenParser(); //hier bijhouden voor enkel deze pronvincies op te slaan.
            //List<string[]> FileSplitted = FileReader("ProvincieInfo.csv");
            Dictionary<int, string> provincienamen = new Dictionary<int, string>();
            foreach (string[] line in FileSplitted)
            {
                if (line[2] == "nl")
                {
                    if (!int.TryParse(line[1], out int provincieId))
                        throw new ProvincieIdException();
                    foreach (int provincieID in provincieIDsVlaanderen)
                    {
                        if (provincieId == provincieID && !provincienamen.ContainsKey(provincieId))
                            provincienamen.Add(provincieId, line[3]);
                    }
                }
                //else
                //Console.WriteLine("provincie != nl");
            }
            Console.WriteLine("ProvincieID - provincienaam geparsed");
            return provincienamen;
        }
        #endregion
        #region provincieIDsVlaanderen
        /// <summary>
        /// Method voor get parsen van ProvincieIDsVlaanderen.csv
        /// geeft lijst van provincieIDs in vlaanderen terug.
        /// </summary>
        /// <returns>lijst van provincieIDs</returns>
        public static List<int> ProvincieIDsVlaanderenParser()
        {
            List<string[]> FileSplitted = FileReader("ProvincieIDsVlaanderen.csv", ',');
            List<int> provincieIds = new List<int>();
            foreach (string[] line in FileSplitted)
            {
                foreach (string provincieId in line)
                {
                    if (!int.TryParse(provincieId, out int provincieID))
                        throw new ProvincieIDException();
                    provincieIds.Add(provincieID);
                    Console.WriteLine("provincieID added");
                }
            }
            Console.WriteLine("Alle provincieIDs toegevoegd");
            return provincieIds;
        }
        #endregion
    }
}
