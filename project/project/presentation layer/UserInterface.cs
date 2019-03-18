using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace project.presentation_layer
{
    interface IUserInterface
    {
        string SelectFunction();
        void ErrorMessage(string message);
        void ViewNote(string noteName, string note);
        string ViewNotesNames(List<string> noteNames);
        List<string> CreateNote();
    }
}
