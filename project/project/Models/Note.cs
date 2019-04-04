using System;
using System.Collections.Generic;
using System.Text;

namespace project.Models
{
    /// <summary>
    /// The class Note
    /// </summary>
    public class Note
    {
        /// <summary>
        /// This represents the id of a note.
        /// </summary>
        public int NoteId { get; set; }
        /// <summary>
        ///  This represents the title of a note.
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// This represents the description of a note.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// This represents the owner of a note.
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// The class constructor.
        /// </summary>
        public Note()
        {

        }
        /// <summary>
        ///  The class constructor that receives parameters from the user.
        /// </summary>
        /// <param name="id"> Sets the id of a note.</param>
        /// <param name="title"> Sets the title of a note.</param>
        /// <param name="description"> Sets the description of a note.</param>
        /// <param name="userid">Sets the owner of a note.</param>
        public Note(int id,string title, string description,int userid)
        {
            this.NoteId = id;
            this.Title = title;
            this.Description = description;
            this.UserID = userid;
        }
        /// <summary>
        /// The class constructor that receives parameters from the user.
        /// </summary>
        /// <param name="title"> Sets the title of a note.</param>
        /// <param name="description"> Sets the description of a note.</param>
        public Note(string title, string description)
        {
            this.Title = title;
            this.Description = description;
        }    
    }
}
