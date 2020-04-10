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
        private readonly string connectionString;
        public DatabaseImporter(string connectionString) => (this.connectionString) = (connectionString);
        public void InsertAll(List<Provincie> provincies)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
            }
        }
        private void GetDataTables(List<Provincie> provincies)
        {
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
            verticesDT.Columns.Add("verticeX",typeof(double));
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
            foreach (Provincie provincie in provincies)
            {
                provinciesDT.Rows.Add(provincie.provincieID, provincie.provincieNaam);
            }
        }
    }
}
