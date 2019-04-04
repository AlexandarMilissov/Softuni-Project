using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using project.Models;

namespace project.presentation_layer
{
    interface IUserInterface
    {
        string StartUpMenu();
        string SelectFunction();
        void ErrorMessage(string message);
        void ViewNote(Note note);
        string ViewNotesNames(List<string> noteNames);
        Note CreateNote();
        User RegisterUser();
        string SelectBackgroundColour();
        string SelectTextColour();
    }
}
