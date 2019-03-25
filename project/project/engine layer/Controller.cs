using System;
using System.Collections.Generic;
using System.Text;
using project.presentation_layer;
using project.Models;

namespace project.engine_layer
{
    class Controller
    {
        public Controller()
        {
            IUserInterface show_view = new ConsoleUserInterface();
            string selection = show_view.SelectFunction();
            while(selection!="5")
            {
                if (int.Parse(selection) < 1 || int.Parse(selection) > 5) show_view.ErrorMessage("Invalid number entered. Please enter a number between 1 and 5 included.");
                switch (selection)
                {
                    case "1":
                        CreateNote();
                        break;
                    case "2":
                        //PrintNote();
                        break;
                    case "3":
                        UpdateNote();
                        break;
                    case "4":
                        DeleteNote();
                        break;
                }
                selection = show_view.SelectFunction();
            }
        }
        private void DeleteNote()
        {

        }
        private void UpdateNote()
        {

        }
        private void CreateNote()
        {
            IUserInterface show_view = new ConsoleUserInterface();
            Note newNote = show_view.CreateNote();
            PrintNote(newNote);
        }
        private void PrintNote(Note note)
        {
            IUserInterface show_view = new ConsoleUserInterface();
            show_view.ViewNote(note);
        }
    }
}
