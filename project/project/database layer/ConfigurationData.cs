using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using project.Models;

namespace project.database_layer
{
    class ConfigurationData
    {
        public void MakeNewConfiguration(Configuration configuration)
        {
            using (var connection = Connection.GetConnection())
            {
                var command = new SqlCommand("INSERT INTO Configurations(ConfigId, TextColor,BackgroundColor)" +
                "VALUES(@configid, @textcolor,@bgcolor);", connection);

                command.Parameters.AddWithValue("configid", configuration.Id);
                command.Parameters.AddWithValue("textcolor", configuration.TextColour);
                command.Parameters.AddWithValue("bgcolor", configuration.BackgroundColour);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public List<Configuration> ShowAll()
        {
            var NoteList = new List<Configuration>();
            using (var connection = Connection.GetConnection())
            {
                var command = new SqlCommand("SELECT * FROM Notes", connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var product = new Configuration(
                            reader.GetInt32(0)
                        );

                        NoteList.Add(product);
                    }
                }
                connection.Close();

                return NoteList;
            }
        }
    }
}
