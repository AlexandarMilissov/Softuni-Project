using System;
using System.Collections.Generic;
using System.Text;

namespace project.Models
{
    /// <summary>
    /// The class Configuration
    /// </summary>
    public class Configuration
    {
        public int Id;
        public ConsoleColor TextColour = ConsoleColor.White;
        public ConsoleColor BackgroundColour = ConsoleColor.Black;

        public Configuration()
        {

        }

        public Configuration(int id, ConsoleColor textcolor,ConsoleColor backgroundcolor)
        {
            this.Id = id;
            this.TextColour = textcolor;
            this.BackgroundColour = backgroundcolor;
        }

        public Configuration(int id)
        {
            this.Id = id;
        }
    }


}
