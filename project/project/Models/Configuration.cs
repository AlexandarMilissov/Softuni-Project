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
        /// <summary>
        /// The class constructor.
        /// </summary>
        public Configuration()
        {

        }
        /// <summary>
        /// The class constructor that receives parameters from the user.
        /// </summary>
        /// <param name="id">Sets the id of a configuration.</param>
        /// <param name="textcolor">Sets the text color of a configuration.</param>
        /// <param name="backgroundcolor">Sets the background color of a configuration.</param>
        public Configuration(int id, ConsoleColor textcolor,ConsoleColor backgroundcolor)
        {
            this.Id = id;
            this.TextColour = textcolor;
            this.BackgroundColour = backgroundcolor;
        }
        /// <summary>
        /// The class constructor that receives id from the user.
        /// </summary>
        /// <param name="id">Sets the id of a configuration.</param>
        public Configuration(int id)
        {
            this.Id = id;
        }
    }


}
