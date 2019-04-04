using System;
using System.Collections.Generic;
using System.Text;

namespace project.Models
{
    /// <summary>
    /// The class User
    /// </summary>
    public class User
    {
        /// <summary>
        /// This represents the id of a user.
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// This represents the username of a user.
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// This represents the password of a user.
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// This represents the configuration id of a user. 
        /// </summary>
        public int ConfigurationID { get; set; }
        /// <summary>
        /// The class constructor.
        /// </summary>
        public User()
        {

        }
        /// <summary>
        /// The class constructor that receives parameters from the user.
        /// </summary>
        /// <param name="userid"> Sets the id of a user.</param>
        /// <param name="username"> Sets the username of a user.</param>
        /// <param name="password">Sets the password of a user.</param>
        /// <param name="configurationid">Sets the configuration id of a user.</param>
        public User(int userid, string username, string password, int configurationid)
        {
            this.Username = username;
            this.Password = password;
            this.UserId = userid;
            this.ConfigurationID = configurationid;
        }
        /// <summary>
        /// The class constructor that receives parameters from the user.
        /// </summary>
        /// <param name="userid"> Sets the id of a user.</param>
        /// <param name="username"> Sets the username of a user.</param>
        /// <param name="password"> Sets the password of a user.</param>
        public User(int userid, string username, string password)
        {
            this.Username = username;
            this.Password = password;
            this.UserId = userid;
        }
        /// <summary>
        /// The class constructor that receives parameters from the user.
        /// </summary>
        /// <param name="username"> Sets the username of a user.</param>
        /// <param name="password"> Sets the password of a user.</param>
        public User(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }
    }
}
