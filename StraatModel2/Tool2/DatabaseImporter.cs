using Labo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace StraatModel2.Tool2
{
    class DatabaseImporter
    {
        private string connectionString;
        private SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }
        public DatabaseImporter(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public void InsertAll(List<Provincie> provincies)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                using (SqlBulkCopy sqlBulk = new SqlBulkCopy(connection))
                {
                    foreach (var datatable in GetDataTables(provincies))
                    {
                        sqlBulk.BulkCopyTimeout = 0;
                        sqlBulk.DestinationTableName = datatable.Key;
                        sqlBulk.WriteToServer(datatable.Value);
                        Console.WriteLine(datatable.Key + "is in database toegevoegd");
                    }
                }
            }
        }
        private Dictionary<string, DataTable> GetDataTables(List<Provincie> provincies) //moet nog gecontroleerd worden voor duplicates
        {
            Dictionary<string, DataTable> dataTables = new Dictionary<string, DataTable>();
            //provincie
            DataTable provinciesDT = new DataTable();
            provinciesDT.Columns.Add("id", typeof(int));
            provinciesDT.Columns.Add("naam", typeof(string));
            //Gemeente
            DataTable gemeenteDT = new DataTable();
            gemeenteDT.Columns.Add("id", typeof(int));
            gemeenteDT.Columns.Add("naam", typeof(string));
            gemeenteDT.Columns.Add("provincie", typeof(int));
            //Straat
            DataTable straatDT = new DataTable();
            straatDT.Columns.Add("id", typeof(int));
            straatDT.Columns.Add("naam", typeof(string));
            straatDT.Columns.Add("graaf", typeof(int));
            straatDT.Columns.Add("gemeente", typeof(int));
            //graaf
            DataTable graafDT = new DataTable();
            graafDT.Columns.Add("id", typeof(int));
            //map
            DataTable mapDT = new DataTable();
            mapDT.Columns.Add("segment", typeof(int));
            mapDT.Columns.Add("graaf", typeof(int));
            //Vertices
            DataTable verticesDT = new DataTable();
            verticesDT.Columns.Add("verticeX", typeof(double));
            verticesDT.Columns.Add("verticeY", typeof(double));
            verticesDT.Columns.Add("segment", typeof(int));
            //Segment
            DataTable segmentDT = new DataTable();
            segmentDT.Columns.Add("id", typeof(int));
            segmentDT.Columns.Add("beginKnoop", typeof(int));
            segmentDT.Columns.Add("eindKnoop", typeof(int));
            //Knoop
            DataTable knoopDT = new DataTable();
            knoopDT.Columns.Add("id", typeof(int));
            knoopDT.Columns.Add("knoopX", typeof(double));
            knoopDT.Columns.Add("knoopY", typeof(double));
            //punt
            DataTable puntDT = new DataTable();
            puntDT.Columns.Add("x", typeof(double));
            puntDT.Columns.Add("y", typeof(double));
            //
            foreach (Provincie provincie in provincies)
            {
                if (!provinciesDT.Rows.Contains(provincie.provincieID))
                {
                    provinciesDT.Rows.Add(provincie.provincieID, provincie.provincieNaam);
                }
                foreach (Gemeente gemeente in provincie.gemeentes)
                {
                    if (!gemeenteDT.Rows.Contains(gemeente.gemeenteID))
                    {
                        gemeenteDT.Rows.Add(gemeente.gemeenteID, gemeente.gemeenteNaam, provincie.provincieID);
                    }
                    foreach (Straat straat in gemeente.straten)
                    {
                        if (!straatDT.Rows.Contains(straat.straatId))
                        {
                            straatDT.Rows.Add(straat.straatId, straat.straatnaam, straat.graaf.graafID, gemeente.gemeenteID);
                        }
                        if (!graafDT.Rows.Contains(straat.graaf.graafID))
                        {
                            graafDT.Rows.Add(straat.graaf.graafID);
                        }
                        foreach (var mapItem in straat.graaf.map)
                        {
                            if (!knoopDT.Rows.Contains(mapItem.Key.knoopId))
                            {
                                knoopDT.Rows.Add(mapItem.Key.knoopId, mapItem.Key.punt.x, mapItem.Key.punt.y);//beginknoop uit map rest knopen uit segmenten
                            }
                            foreach (Segment segment in mapItem.Value)
                            {
                                mapDT.Rows.Add(segment.segmentID, straat.graaf.graafID);
                                foreach (var punt in segment.vertices)
                                {
                                    verticesDT.Rows.Add(punt.x, punt.y, segment.segmentID);
                                    if (!puntDT.Rows.Contains(punt.x) && !puntDT.Rows.Contains(punt.y))
                                    {
                                        puntDT.Rows.Add(punt.x, punt.y);
                                    }
                                }
                                if (!segmentDT.Rows.Contains(segment.segmentID))
                                {
                                    segmentDT.Rows.Add(segment.segmentID, segment.beginKnoop.knoopId, segment.eindKnoop.knoopId);
                                }
                                if (!knoopDT.Rows.Contains(segment.eindKnoop.knoopId))
                                {
                                    knoopDT.Rows.Add(segment.eindKnoop.knoopId, segment.eindKnoop.punt.x, segment.eindKnoop.punt.y);
                                }
                            }
                        }
                    }
                }
            }
            dataTables.Add("Provincie", provinciesDT);
            dataTables.Add("Gemeente", gemeenteDT);
            dataTables.Add("Straat", straatDT);
            dataTables.Add("Graaf", graafDT);
            dataTables.Add("Punt", puntDT);
            dataTables.Add("Knoop", knoopDT);
            dataTables.Add("Segment", segmentDT);
            dataTables.Add("Vertices", verticesDT);
            dataTables.Add("Map", mapDT);
            return dataTables;
        }
    }
}
