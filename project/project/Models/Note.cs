using System;
using System.Collections.Generic;
using System.Text;

namespace project.Models
{
    public class Note
    {
        public int NoteId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int UserID { get; set; }

        public Note()
        {

        }

        public Note(int id,string title, string description,int userid)
        {
            this.NoteId = id;
            this.Title = title;
            this.Description = description;
            this.UserID = userid;
        }

        public Note(string title, string description)
        {
            this.Title = title;
            this.Description = description;
        }    
    }
}
