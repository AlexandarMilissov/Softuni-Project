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
        public void ChangeConfiguration(Configuration config)
        {
            using (var connection = Connection.GetConnection())
            {
                var command = new SqlCommand("UPDATE Configurations SET TextColor=@textcolor,BackgroundColor=@bgcolor WHERE ConfigId=@configid", connection);
                command.Parameters.AddWithValue("configid", config.Id);
                command.Parameters.AddWithValue("textcolor", config.TextColour);
                command.Parameters.AddWithValue("bgcolor", config.BackgroundColour);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
