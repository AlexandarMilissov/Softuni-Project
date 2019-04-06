using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using project.presentation_layer;
using project.Models;
using project.database_layer;

namespace project.engine_layer
{
    public class Controller
    {
        /// <summary>
        /// A private variable for user interface class initialization.
        /// </summary>
        private IUserInterface userInterface = new ConsoleUserInterface();
        /// <summary>
        /// A private variable for class 'Note' database functions initialization.
        /// </summary>
        private NoteData noteDatabaseFunctions = new NoteData();
        /// <summary>
        /// A private variable for class 'User' database functions initialization.
        /// </summary>
        private UserData userDatabaseFunctions = new UserData();
        /// <summary>
        /// A private variable for class 'Configuration' database functions initialization.
        /// </summary>
        private ConfigurationData configurationDatabaseFunctions = new ConfigurationData();
        /// <summary>
        /// A private variable that keeps track of the currently logged in user.
        /// </summary>
        private User currentUser = null;
        /// <summary>
        /// A public contstructor for global program startup.
        /// </summary>
        public Controller()
        {
            StartMenu();
        }
        /// <summary>
        /// A private method for note deletion. Connects the database and presentation layers.
        /// </summary>
        private void DeleteNote()
        {
            int note_num = ListNotes();
            if(note_num==-1)
            {
                return;
            }
            noteDatabaseFunctions.DeleteNote(note_num);
        }
        /// <summary>
        /// A private method for note editing. Connects the database and presentation layers.
        /// </summary>
        private void UpdateNote()
        {
            int note_id = ListNotes();
            if(note_id == -1)
            {
                return;
            }
            Note oldNote = noteDatabaseFunctions.ShowAll().Where(x => x.NoteId == note_id).First();
            Note newNote = userInterface.CreateNote(oldNote);
            newNote = NoteFieldsValidation(newNote);
            newNote.NoteId = note_id;
            noteDatabaseFunctions.UpdateNote(newNote);
        }
        /// <summary>
        /// A private method for note creation. Connects the database and presentation layers.
        /// </summary>
        private void CreateNote()
        {
            Note newNote = userInterface.CreateNote();
            newNote = NoteFieldsValidation(newNote);
            newNote.UserID = currentUser.UserId;
            noteDatabaseFunctions.MakeNewNote(newNote);
        }
        /// <summary>
        /// A private method for note parameters validation. Checks if the note title is empty or longer than 20 symbols.
        /// </summary>
        /// <param name="newNote">The note to be checked.</param>
        /// <returns>Returns an instance of the class 'Note' that complies with the standards.</returns>
        private Note NoteFieldsValidation(Note newNote)
        {
            while (string.IsNullOrEmpty(newNote.Title))
            {
                userInterface.ErrorMessage("Note title cannot be empty.");
                newNote = userInterface.CreateNote(newNote);
            }
            while (newNote.Title.Length > 20)
            {
                userInterface.ErrorMessage("Note title cannot be longer than 20 characters.");
                newNote = userInterface.CreateNote(newNote);
            }
            return newNote;
        }
        /// <summary>
        /// A private method for note printing. Connects the database and presentation layers.
        /// </summary>
        private void PrintNote()
        {
            int note_num = ListNotes();
            if(note_num==-1)
            {
                return;
            }
            userInterface.ViewNote(noteDatabaseFunctions.ShowSpecificNote(note_num));
        }
        /// <summary>
        /// A private method that is used in multiple other private methods. Created for convenience. Lists all the available notes in the database and returns the number of the selected by the user note.
        /// </summary>
        /// <returns>The index of the selected by the user note.</returns>
        private int ListNotes()
        {
            var available_notes = noteDatabaseFunctions.ShowAll().Where(x => x.UserID == currentUser.UserId).ToList();
            if(available_notes.Count==0)
            {
                userInterface.ErrorMessage("Cannot find any previously saved notes.");
                return -1;
            }
            List<string> titles = new List<string>();
            foreach (var item in available_notes)
            {
                titles.Add(item.Title);
            }
            int note_num = UserNoteChoiceValidation(titles);
            if(note_num==-1)
            {
                return -1;
            }
            string note_name = titles[note_num-1];
            foreach(var item in available_notes)
            {
                if(item.Title==note_name)
                {
                    note_num = item.NoteId;
                }
            }
            return note_num;
        }
        /// <summary>
        /// A created for convenience private method. Checks whether the entered from the user number is valid and asks for a new input every time it is not.
        /// </summary>
        /// <param name="input">The list with the availabe notes titles.</param>
        /// <returns>A valid index from the list.</returns>
        private int UserNoteChoiceValidation(List<string> input)
        {
            int note_num = 0;
            Int32.TryParse(userInterface.ViewNotesNames(input), out note_num);
            if (note_num < 1 || note_num > input.Count)
            {
                if (note_num == input.Count + 1)
                {
                    return -1;
                }
                userInterface.ErrorMessage("No note with the entered number exists in the database.");
                note_num = UserNoteChoiceValidation(input);
            }
            return note_num;
        }
        /// <summary>
        /// The second screen that user sees after they register/login. Acts as a menu controller.
        /// </summary>
        private void OperationsMenu()
        {
            int selection = 0;
            Int32.TryParse(userInterface.SelectFunction(), out selection);
            while(selection < 1 || selection > 7)
            {
                userInterface.ErrorMessage("Invalid input detected. Please enter a NUMBER between 1 and 7 included.");
                Int32.TryParse(userInterface.SelectFunction(), out selection);
            }
            while (true)
            {
                switch (selection)
                {
                    case 1:
                        CreateNote();
                        break;
                    case 2:
                        PrintNote();
                        break;
                    case 3:
                        UpdateNote();
                        break;
                    case 4:
                        DeleteNote();
                        break;
                    case 5:
                        ConfigureColors();
                        break;
                    case 6:
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
                        StartMenu();
                        break;
                    case 7:
                        Environment.Exit(0);
                        break;
                }
                Int32.TryParse(userInterface.SelectFunction(), out selection);
                while (selection == 0 || (selection < 1 || selection > 7))
                {
                    userInterface.ErrorMessage("Invalid input detected. Please enter a NUMBER between 1 and 7 included.");
                    Int32.TryParse(userInterface.SelectFunction(), out selection);
                }
            }
        }
        /// <summary>
        /// The first screen that a user sees after they execute the program. Acts as a main menu controller.
        /// </summary>
        private void StartMenu()
        {
            int selection = 0;
            Int32.TryParse(userInterface.StartUpMenu(), out selection);
            while (selection == 0 || (selection < 1 || selection > 3))
            {
                userInterface.ErrorMessage("Please enter a NUMBER between 1 and 3 included.");
                Int32.TryParse(userInterface.StartUpMenu(), out selection);
            }
            if(selection==1)
            {
                bool user_exists = false;
                User logging_user = userInterface.RegisterUser();
                foreach(var item in userDatabaseFunctions.ShowAll())
                {
                    if(item.Username==logging_user.Username && item.Password==logging_user.Password)
                    {
                        currentUser = item;
                        Configuration config = configurationDatabaseFunctions.ShowAll().Where(x => x.Id == item.ConfigurationID).First();
                        Console.BackgroundColor = config.BackgroundColour;
                        Console.ForegroundColor = config.TextColour;
                        OperationsMenu();
                        user_exists = true;
                        break;
                    }
                }
                if(!user_exists)
                {
                    userInterface.ErrorMessage("Incorrect username or password.");
                    StartMenu();
                }
            }
            else if(selection==2)
            {
                User registering_user = userInterface.RegisterUser();
                registering_user.ConfigurationID = ConfigurationMenu();
                userDatabaseFunctions.RegisterUser(registering_user);
                currentUser = userDatabaseFunctions.ShowAll().Last();
                OperationsMenu();
            }
            else
            {
                Environment.Exit(0);
            }
        }
        /// <summary>
        /// A private method that exists for the sake of convenience. Adds a new user configuration in the database and returns it's id.
        /// </summary>
        /// <returns>The id of the latest newly added configuration.</returns>
        private int ConfigurationMenu()
        {
            Configuration config = Configure();
            configurationDatabaseFunctions.MakeNewConfiguration(config);
            return configurationDatabaseFunctions.ShowAll().Last().Id;
        }
        /// <summary>
        /// A private method that exists for the sake of convenience. Changes a user configuration in the database.
        /// </summary>
        private void ConfigureColors()
        {
            Configuration config = Configure();
            config.Id = currentUser.ConfigurationID;
            configurationDatabaseFunctions.ChangeConfiguration(config);
        }
        /// <summary>
        /// A private method for configuration selection. Acts as a configuration menu setup controller.
        /// </summary>
        /// <returns>An instance of the configuration that the user has created.</returns>
        private Configuration Configure()
        {
            Configuration config = new Configuration();
            int colour_num = 0;
            Int32.TryParse(userInterface.SelectBackgroundColour(), out colour_num);
            while (colour_num < 1 || colour_num > 17)
            {
                userInterface.ErrorMessage("Please enter a NUMBER between 1 and 17 included.");
                Int32.TryParse(userInterface.SelectBackgroundColour(), out colour_num);
            }
            Console.BackgroundColor = (ConsoleColor)colour_num - 1;
            config.BackgroundColour = (ConsoleColor)colour_num - 1;
            Int32.TryParse(userInterface.SelectBackgroundColour(), out colour_num);
            while (colour_num < 1 || colour_num > 17)
            {
                userInterface.ErrorMessage("Please enter a NUMBER between 1 and 17 included.");
                Int32.TryParse(userInterface.SelectTextColour(), out colour_num);
            }
            Console.ForegroundColor = (ConsoleColor)colour_num - 1;
            config.TextColour = (ConsoleColor)colour_num - 1;

            return config;
        }
    }
}
