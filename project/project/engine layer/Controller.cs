using System;
using System.Collections.Generic;
using System.Text;
using project.presentation_layer;
using project.Models;

namespace project.engine_layer
{
    class Controller
    {
        private IUserInterface userInterface = new ConsoleUserInterface();
        public Controller()
        {
            string selection = userInterface.SelectFunction();
            while(selection!="5")
            {
                if (int.Parse(selection) < 1 || int.Parse(selection) > 5) userInterface.ErrorMessage("Invalid number entered. Please enter a number between 1 and 5 included.");
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
                selection = userInterface.SelectFunction();
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
            Note newNote = userInterface.CreateNote();
            PrintNote(newNote);
        }
        private void PrintNote(Note note)
        {
            userInterface.ViewNote(note);
        }
    }
}
