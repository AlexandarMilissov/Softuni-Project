using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using project.Models;


namespace project.database_layer
{
    class UserData
    {
        public void RegisterUser(User user)
        {
            using (var connection = Connection.GetConnection())
            {
                var command = new SqlCommand("INSERT INTO Users(Username, Password)" +
                "VALUES(@username, @password);", connection);
                command.Parameters.AddWithValue("username", user.Username);
                command.Parameters.AddWithValue("password", user.Password);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
