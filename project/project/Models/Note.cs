using System;
using System.Collections.Generic;
using System.Text;

namespace project.Models
{
    public class Note
    {
        private string title;
        private string description;
        private DateTime checkpoint;

        public string Title { get => title; }
        public string Description { get => description; }
        public DateTime Checkpoint { get => checkpoint; }

        public Note(string title, string description, DateTime checkpoint)
        {
            this.title = title;
            this.description = description;
            this.checkpoint = checkpoint;
        }
        public Note(string title, string description)
        {
            this.title = title;
            this.description = description;
        }
        
    }
}
