using System;
using System.Collections.Generic;
using System.Text;

namespace project.Models
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int UserId { get; set; }     

        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
