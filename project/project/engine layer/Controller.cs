using project.presentation_layer;
using System;
using System.Collections.Generic;
using System.Text;

namespace project.engine_layer
{
    class Controller
    {
        public Controller()
        {
            IUserInterface show_view = new ConsoleUserInterface();
            string selection;
            do
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
                    PrintNote();
                    break;
                case "3":
                    UpdateNote();
                    break;
                case "4":
                    DeleteNote();
                    break;
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

        }
        private void PrintNote()
        {

        }
    }
}
