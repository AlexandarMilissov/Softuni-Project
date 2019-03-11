using System;
using System.Collections.Generic;
using System.Text;

namespace project.presentation_layer
{
    interface IUserInterface
    {
        string SelectFunction();
        void ErrorMessage(string message);
    }
}
