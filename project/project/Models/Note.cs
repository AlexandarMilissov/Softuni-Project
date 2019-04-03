using System;
using System.Collections.Generic;
using System.Text;

namespace project.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Note()
        {

        }

        public Note(int id,string title, string description, DateTime checkpoint)
        {
            this.Id = id;
            this.Title = title;
            this.Description = description;
            this.Checkpoint = checkpoint;
        }

        public Note(string title, string description)
        {
            this.Title = title;
            this.Description = description;
        }    
    }
}
