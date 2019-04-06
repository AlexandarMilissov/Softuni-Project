using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using project.Models;

namespace project.database_layer
{
    /// <summary>
    /// The class ConfigurationData.
    /// </summary>
    public class ConfigurationData
    {
        /// <summary>
        /// This method is used for making a new configuration.
        /// </summary>
        /// <param name="configuration"> This is used to be filled with parameters by the user.</param>
        public void MakeNewConfiguration(Configuration configuration)
        {
            using (var connection = Connection.GetConnection())
            {
                var command = new SqlCommand("INSERT INTO Configurations(TextColor,BackgroundColor)" +
                "VALUES(@textcolor,@bgcolor);", connection);
                command.Parameters.AddWithValue("textcolor", configuration.TextColour);
                command.Parameters.AddWithValue("bgcolor", configuration.BackgroundColour);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        /// <summary>
        /// This metod is used for changing a configuration.
        /// </summary>
        /// <param name="config">This changes the parameters of an already existing configuration that is found by Id.</param>
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
        /// <summary>
        /// This method used to display every existing configuration in the Database from the SQL Server.
        /// </summary>
        /// <returns>List of all existing configurations.</returns>
        public List<Configuration> ShowAll()
        {
            var ConfigList = new List<Configuration>();
            using (var connection = Connection.GetConnection())
            {
                var command = new SqlCommand("SELECT * FROM Configurations", connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var product = new Configuration(
                            reader.GetInt32(0),
                            (ConsoleColor)reader.GetInt32(1),
                            (ConsoleColor)reader.GetInt32(2)
                        );

                        ConfigList.Add(product);
                    }
                }
                connection.Close();

                return ConfigList;
            }
        }
    }
}
