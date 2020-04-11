using Labo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace StraatModel2.Tool3
{
    class DatabaseBevragingen
    {
        private string connectionString;
        public DatabaseBevragingen(string connectionString) => (this.connectionString) = (connectionString);
        private SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }
        /// <summary>
        /// Geeft lijst van straatIDs voor opgegeven gemeentenaam
        /// </summary>
        /// <param name="gemeentenaam"></param>
        public List<int> GeefStraatIds(string gemeentenaam)
        {
            List<int> straten = new List<int>();
            //gemeenteid verkrijgen via gemeentenaam
            string gemeenteIdQuery = "SELECT id " +
                                     "FROM Gemeente " +
                                     "WHERE naam = @gemeentenaam";
            //via gemeenteid straatids krijgen
            string straatIdQuery = "SELECT id " +
                                   "FROM Straat " +
                                   "WHERE gemeente = @gemeenteId";

            using (SqlConnection connection = GetConnection())
            {
                try
                {
                    SqlCommand command1 = new SqlCommand(gemeenteIdQuery, connection);
                    command1.Parameters.AddWithValue("@gemeentenaam", gemeentenaam);
                    connection.Open();
                    int gemeenteId = (int)command1.ExecuteScalar();
                    SqlCommand command2 = new SqlCommand(straatIdQuery, connection);
                    command2.Parameters.AddWithValue("@gemeenteId", gemeenteId);
                    SqlDataReader reader = command2.ExecuteReader();
                    while (reader.Read())
                    {
                        straten.Add((int)reader["id"]);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                finally
                {
                    connection.Close();
                }
            }
            return straten;
        }
        /// <summary>
        /// Geeft straat terug op basis van straatID
        /// </summary>
        /// <param name="straatID"></param>
        public Straat GeefStraat(int straatID)
        {
            int graafID = 0;
            string straatnaam = "";
            //graafid verkrijgen via straatid Kan eventueel weggelaten worden : graafid == straatid
            string graafQuery = "SELECT graaf " +
                                "FROM Straat " +
                                "WHERE id = @straatId";
            //straatnaam verkrijgen via straatID
            string straatNaamQuery = "SELECT naam " +
                                   "FROM Straat " +
                                   "WHERE id = @straatId";

            using (SqlConnection connection = GetConnection())
            {
                try
                {
                    SqlCommand commandGraafID = new SqlCommand(graafQuery, connection);
                    commandGraafID.Parameters.AddWithValue("@straatId", straatID);
                    connection.Open();
                    graafID = (int)commandGraafID.ExecuteScalar();
                    SqlCommand commandStraatNaam = new SqlCommand(straatNaamQuery, connection);
                    commandStraatNaam.Parameters.AddWithValue("@straatId", straatID);
                    SqlDataReader reader = commandStraatNaam.ExecuteReader();
                    while (reader.Read())
                    {
                        straatnaam = (string)reader["naam"]; // tostring werkt ook ?
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                finally
                {
                    connection.Close();
                }
            }
            List<Segment> segmentenVoorGraafBuilder = GeefLijstVanSegmenten(graafID);
            return new Straat(straatID, straatnaam, Graaf.buildGraaf(graafID, segmentenVoorGraafBuilder));
        }
        /// <summary>
        /// Geeft straat terug op basis van straatnaam en gemeentenaam.
        /// </summary>
        /// <param name="straatnaam"></param>
        /// <param name="gemeentenaam"></param>
        /// <returns></returns>
        public Straat GeefStraat(string straatnaam, string gemeentenaam)
        {
            int straatId = 0;
            string queryGemeenteID = "SELECT id " +
                                     "FROM Gemeente " +
                                     "WHERE naam = @gemeentenaam";
            string queryStraatID = "SELECT id " +
                                   "FROM Straat " +
                                   "WHERE naam = @straatnaam AND gemeente = @gemeenteID";
            using (SqlConnection connection = GetConnection())
            {
                try
                {
                    SqlCommand gemeenteID = new SqlCommand(queryGemeenteID, connection);
                    gemeenteID.Parameters.AddWithValue("@gemeentenaam", gemeentenaam);
                    connection.Open();
                    int gemeenteId = (int)gemeenteID.ExecuteScalar();
                    SqlCommand straatID = new SqlCommand(queryStraatID, connection);
                    straatID.Parameters.AddWithValue("@straatnaam", straatnaam);
                    straatID.Parameters.AddWithValue("@gemeenteID", gemeenteId);
                    straatId = (int)straatID.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                finally
                {
                    connection.Close();
                }
            }
            return GeefStraat(straatId);
        }
        public List<String> GeefStraatnamenGemeente(string gemeente)
        {
            List<String> straatNamen = new List<string>();
            string queryGemeenteID = "SELECT id " +
                                     "FROM Gemeente " +
                                     "WHERE naam = @gemeentenaam";
            string queryStraatnamen = "SELECT naam " +
                                      "FROM Straat " +
                                      "WHERE gemeente = @gemeenteID " +
                                      "ORDER BY naam ASC";//hoeft eigenlijk niet want gesorteerd in databank gestoken.
            using (SqlConnection connection = GetConnection())
            {
                try
                {
                    SqlCommand gemeenteID = new SqlCommand(queryGemeenteID, connection);
                    gemeenteID.Parameters.AddWithValue("@gemeentenaam", gemeente);
                    connection.Open();
                    int gemeenteId = (int)gemeenteID.ExecuteScalar();
                    SqlCommand straatnamen = new SqlCommand(queryStraatnamen, connection);
                    straatnamen.Parameters.AddWithValue("@gemeenteID", gemeenteId);
                    SqlDataReader reader = straatnamen.ExecuteReader();
                    while (reader.Read())
                    {
                        straatNamen.Add((string)reader["naam"]);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                finally
                {
                    connection.Close();
                }
            }
            return straatNamen;
        }
        #region Hulpmethodes
        private List<Segment> GeefLijstVanSegmenten(int graafID)
        {
            List<Segment> segmenten = new List<Segment>();
            //via graafID alle segmentenIDS van graaf verkrijgen
            List<int> lijstSegmentenIDs = GeefLijstSegmentenIds(graafID);
            foreach (int segmentID in lijstSegmentenIDs)
            {
                //punten van segment verkrijgen  via vertices => punt
                List<Punt> puntenSegment = GeefPuntenSegment(segmentID);
                //knopen van segment verkrijgen (begin en eindknoop)
                List<Knoop> knopenSegment = GeefKnopenSegment(segmentID);
                //segmenten opmaken en toevoegen aan lijst
                for (int i = 0; i < knopenSegment.Count; i += 2)
                {
                    segmenten.Add(new Segment(segmentID, knopenSegment[i], knopenSegment[i + 1], puntenSegment));
                }
            }
            return segmenten;
        }
        /// <summary>
        /// geeft via graafID lijst van alle segmentenIDS voor die graaf terug
        /// </summary>
        /// <param name="graafID"></param>
        /// <returns></returns>
        private List<int> GeefLijstSegmentenIds(int graafID)
        {
            List<int> segmentenIDs = new List<int>();
            string query = "SELECT segment " +
                           "FROM Map " +
                           "WHERE graaf = @graafID";
            using (SqlConnection connection = GetConnection())
            {
                try
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@graafID", graafID);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        segmentenIDs.Add((int)reader["segment"]);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                finally
                {
                    connection.Close();
                }
            }
            return segmentenIDs;
        }
        /// <summary>
        /// geeft een lijst van alle punten die horen tot dit segment
        /// </summary>
        /// <param name="segmentID"></param>
        /// <returns></returns>
        private List<Punt> GeefPuntenSegment(int segmentID)
        {
            List<Punt> punten = new List<Punt>();
            string query = "SELECT verticeX,verticeY " +
                           "FROM Vertices " +
                           "WHERE segment = @segmentID";
            using (SqlConnection connection = GetConnection())
            {
                try
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@segmentID", segmentID);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        double x = (double)reader["verticeX"];
                        double y = (double)reader["verticeY"];
                        punten.Add(new Punt(x, y));
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                finally
                {
                    connection.Close();
                }
            }
            return punten;
        }
        /// <summary>
        /// geeft lijst van knopen met afwisselend begin en eindknoop
        /// </summary>
        /// <param name="segmentID"></param>
        private List<Knoop> GeefKnopenSegment(int segmentID)
        {
            List<Knoop> knopen = new List<Knoop>();
            int[] knopenIds = new int[2];
            string queryKnoopIds = "SELECT beginKnoop,eindKnoop " +
                                   "FROM Segment " +
                                   "WHERE id = @segmentID";

            string queryPunten = "SELECT knoopX,knoopY " +
                                 "FROM Knoop " +
                                 "WHERE id = @knoopID";

            using (SqlConnection connection = GetConnection())
            {
                try
                {
                    //knopenIds vinden
                    SqlCommand commandKnoopIds = new SqlCommand(queryKnoopIds, connection);
                    commandKnoopIds.Parameters.AddWithValue("@segmentID", segmentID);
                    connection.Open();
                    SqlDataReader reader = commandKnoopIds.ExecuteReader();
                    while (reader.Read())
                    {
                        knopenIds[0] = (int)reader["beginKnoop"];
                        knopenIds[1] = (int)reader["eindKnoop"];
                    }
                    reader.Close();
                    //punten voor knopenids vinden en toevoegen aan dictionary
                    SqlCommand commandPuntenBeginKnoop = new SqlCommand(queryPunten, connection);
                    SqlCommand commandPuntenEindKnoop = new SqlCommand(queryPunten, connection);
                    commandPuntenBeginKnoop.Parameters.AddWithValue("@knoopID", knopenIds[0]);
                    commandPuntenEindKnoop.Parameters.AddWithValue("@knoopID", knopenIds[1]);
                    //beginknoop
                    SqlDataReader readerBeginKnoop = commandPuntenBeginKnoop.ExecuteReader();
                    while (readerBeginKnoop.Read())
                    {
                        double x = (double)readerBeginKnoop["knoopX"];
                        double y = (double)readerBeginKnoop["knoopY"];
                        knopen.Add(new Knoop(knopenIds[0], new Punt(x, y)));
                    }
                    readerBeginKnoop.Close();
                    //eindknoop
                    SqlDataReader readerEindKnoop = commandPuntenEindKnoop.ExecuteReader();
                    while (readerEindKnoop.Read())
                    {
                        double x = (double)readerEindKnoop["knoopX"];
                        double y = (double)readerEindKnoop["knoopY"];
                        knopen.Add(new Knoop(knopenIds[1], new Punt(x, y)));
                    }
                    readerBeginKnoop.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                finally
                {
                    connection.Close();
                }
            }
            return knopen;
        }
        #endregion
    }
}
