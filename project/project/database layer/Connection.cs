using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace project.database_layer
{
    public static class Connection
    {
        /// <summary>
        /// This class is used for establishing connection with the SQL server
        /// </summary>
        private static string connectionString = "Server=.\\SQLEXPRESS;Database=Project;Integrated Security=true";
        /// <summary>
        /// Method used for getting the connection string from above. Looks more appropriate.
        /// </summary>
        /// <returns>Returns the connection string from above.</returns>
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
