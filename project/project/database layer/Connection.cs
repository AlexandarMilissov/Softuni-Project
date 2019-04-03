using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace project.database_layer
{
    public static class Connection
    {
        //String used to establish connection with SQL_server
        private static string connectionString = "Server=.\\SQLEXPRESS;Database=Project;Integrated Security=true";
        //Method used for getting the connectiongString from above.
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
