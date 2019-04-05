using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using project.Models;

namespace project.presentation_layer
{
    interface IUserInterface
    {
        /// <summary>
        /// Menu where the user can choose between logging in or register an account.
        /// </summary>
        /// <returns></returns>
        string StartUpMenu();
        /// <summary>
        /// Menu where the user can select the fucntion he wants to do.
        /// </summary>
        /// <returns></returns>
        string SelectFunction();
        /// <summary>
        /// Writes message to the user.
        /// </summary>
        /// <param name="message"></param>
        void ErrorMessage(string message);
        /// <summary>
        /// Displays a note.
        /// </summary>
        /// <param name="note"></param>
        void ViewNote(Note note);
        /// <summary>
        /// Displays list of note names.
        /// </summary>
        /// <param name="noteNames"></param>
        /// <returns>What the user has written. It's supposed to be the number of the note in the given list.</returns>
        string ViewNotesNames(List<string> noteNames);
        /// <summary>
        /// The user can create a new note
        /// </summary>
        /// <returns></returns>
        Note CreateNote();
        /// <summary>
        /// The user can edit a new note.
        /// </summary>
        /// <param name="note"></param>
        /// <returns></returns>
        Note CreateNote(Note note);
        /// <summary>
        /// The user can enter data for an account - username and password.
        /// </summary>
        /// <returns></returns>
        User RegisterUser();
        /// <summary>
        /// Displays a list of all colours available
        /// </summary>
        /// <returns>What the user has written. It's supposed to be the number of the colour in the given list. You can cast it to 'ConsoleColor'</returns>
        string SelectBackgroundColour();
        /// <summary>
        /// Displays a list of all colours available
        /// </summary>
        /// <returns>What the user has written. It's supposed to be the number of the colour in the given list. You can cast it to 'ConsoleColor'</returns>
        string SelectTextColour();
    }
}
