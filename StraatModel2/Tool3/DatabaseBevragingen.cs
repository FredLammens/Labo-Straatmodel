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
        public List<int> straatIDs(string gemeentenaam)
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
    }
}
