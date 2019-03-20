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
            /*do
            {
                selection = show_view.SelectFunction();
                if(int.Parse(selection) < 1 || int.Parse(selection) > 4)show_view.ErrorMessage("Invalid number entered. Please enter a number between 1 and 4 included.");
            }
            while (int.Parse(selection) < 1 || int.Parse(selection) > 4);
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
            }*/
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
            List<string> NoteContents = show_view.CreateNote();
            Note newNote = new Note(NoteContents[0], NoteContents[1]);
        }
        private void PrintNote(Note note)
        {
            IUserInterface show_view = new ConsoleUserInterface();
            show_view.ViewNote(note.Title, note.Description);
        }
    }
}
