﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using project.Models;


namespace project.database_layer
{
    /// <summary>
    /// The class UserData.
    /// </summary>
    public class UserData
    {
        /// <summary>
        /// This method registers new user in the Database.
        /// </summary>
        /// <param name="user">Makes a new user that gets parameters from a user.</param>
        public void RegisterUser(User user)
        {
            using (var connection = Connection.GetConnection())
            {
                var command = new SqlCommand("INSERT INTO Users(Username, Password, ConfigId)" +
                "VALUES(@username, @password, @configid);", connection);
                command.Parameters.AddWithValue("username", user.Username);
                command.Parameters.AddWithValue("password", user.Password);
                command.Parameters.AddWithValue("configid", user.ConfigurationID);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        /// <summary>
        /// This method used to display every existing user in the Database from the SQL Server.
        /// </summary>
        /// <returns>List of all existing users.</returns>
        public List<User> ShowAll()
        {
            var UserList = new List<User>();
            using (var connection = Connection.GetConnection())
            {
                var command = new SqlCommand("SELECT * FROM Users", connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var product = new User(
                            reader.GetInt32(0),
                            reader.GetString(1),
                            reader.GetString(2),
                            reader.GetInt32(3)
                        );

                        UserList.Add(product);
                    }
                }
                connection.Close();
            }
            return UserList;
        }
    }
}
