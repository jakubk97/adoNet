using System;
using System.Data;
using System.Data.SqlClient;

namespace muzyka
{
    class Program
    {
        static void Main(string[] args)
        {

            bool dalej = true;
            while (dalej)
            {
                Console.WriteLine("1 - Metoda połączeniowa z bazą");
                Console.WriteLine("2 - Metoda bezpołączeniowa z bazą");
                Console.WriteLine("Każde inne wejście - wyjście z aplikacji");

                string menu = Console.ReadLine();

                switch (menu)
                {
                    case "1":
                        Polaczeniowe();
                        break;

                    case "2":
                        BezPolaczeniowe();
                        break;

                    default:
                        dalej = false;
                        break;
                }
            }

        }

        public static void Polaczeniowe()
        {
            string connectionString = @"Data Source = KUBA-KOMPUTER\SQLEXPRESS; Initial Catalog = muzyka; Integrated Security = True; ";

            string queryString =
                @"SELECT track_id,artist_name,track_name,album_name from track
                INNER JOIN album ON track.album_id = album.album_id INNER JOIN artist ON track.artist_id = artist.artist_id;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    Console.WriteLine("Wynik metody:\n");
                    while (reader.Read())
                    {
                        Console.WriteLine("{0}" + " " + "{1}" + " " + "{2}" + " " + "{3}", reader[0], reader[1], reader[2], reader[3]);
                    }
                    reader.Close();
                    Console.WriteLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public static void BezPolaczeniowe()
        {
            try
            {
                SqlConnection sqlConnection = new SqlConnection(@"Data Source = KUBA-KOMPUTER\SQLEXPRESS; Initial Catalog = muzyka; Integrated Security = True; ");
                DataSet dataSet = new DataSet();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(@"SELECT track_id,artist_name,track_name,album_name from track
                    INNER JOIN album ON track.album_id = album.album_id INNER JOIN artist ON track.artist_id = artist.artist_id;", sqlConnection);
                dataAdapter.Fill(dataSet, "Tabela");
                Console.WriteLine("Wynik metody:\n");
                foreach (DataRow item in dataSet.Tables["Tabela"].Rows)
                {
                    Console.WriteLine(item[0] + " " + item[1] + " " + item[2] + " " + item[3]);
                }
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


    }
}
