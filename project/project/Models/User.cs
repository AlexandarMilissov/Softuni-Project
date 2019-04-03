using System;
using System.Collections.Generic;
using System.Text;

namespace project.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public User()
        {

        }

        public User(int userid, string username, string password)
        {
            this.Username = username;
            this.Password = password;
            this.UserId = userid;
        }

        public User(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }
    }
}
