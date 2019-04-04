using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using project.presentation_layer;
using project.Models;
using project.database_layer;

namespace project.engine_layer
{
    class Controller
    {
        private IUserInterface userInterface = new ConsoleUserInterface();
        private NoteData noteDatabaseFunctions = new NoteData();
        private UserData userDatabaseFunctions = new UserData();
        private ConfigurationData configurationDatabaseFunctions = new ConfigurationData();
        private User currentUser = null;
        public Controller()
        {
            StartMenu();
        }
        private void DeleteNote()
        {
            int note_num = ListNotes();
            if(note_num==-1)
            {
                return;
            }
            noteDatabaseFunctions.DeleteNote(note_num);
        }
        private void UpdateNote()
        {
            int note_id = ListNotes();
            if(note_id == -1)
            {
                return;
            }
            Note newNote = userInterface.CreateNote();
            newNote.NoteId = note_id;
            noteDatabaseFunctions.UpdateNote(newNote);
        }
        private void CreateNote()
        {
            Note newNote = userInterface.CreateNote();
            newNote.UserID = currentUser.UserId;
            noteDatabaseFunctions.MakeNewNote(newNote);
        }
        private void PrintNote()
        {
            int note_num = ListNotes();
            if(note_num==-1)
            {
                return;
            }
            userInterface.ViewNote(noteDatabaseFunctions.ShowSpecificNote(note_num));
        }
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
            int note_num = 0;
            Int32.TryParse(userInterface.ViewNotesNames(titles), out note_num);
            while (note_num < 1 || note_num > available_notes.Count)
            {
                userInterface.ErrorMessage("No note with the entered number exists in the database.");
                note_num = ListNotes();
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
        private void OperationsMenu()
        {
            int selection = 0;
            Int32.TryParse(userInterface.SelectFunction(), out selection);
            while(selection == 0 || (selection < 1 || selection > 7))
            {
                userInterface.ErrorMessage("Invalid input detected. Please enter a NUMBER between 1 and 7 included.");
                Int32.TryParse(userInterface.SelectFunction(), out selection);
            }
            while (selection != 7)
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
                }
                Int32.TryParse(userInterface.SelectFunction(), out selection);
                while (selection == 0 || (selection < 1 || selection > 7))
                {
                    userInterface.ErrorMessage("Invalid input detected. Please enter a NUMBER between 1 and 7 included.");
                    Int32.TryParse(userInterface.SelectFunction(), out selection);
                }
            }
        }
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
                registering_user.ConfigurationID = 1;
                registering_user.ConfigurationID = ConfigurationMenu();
                userDatabaseFunctions.RegisterUser(registering_user);
                OperationsMenu();
            }
            else
            {
                Environment.Exit(0);
            }
        }
        private int ConfigurationMenu()
        {
            Configuration config = new Configuration();
            int colour_num = 0;
            Int32.TryParse(userInterface.SelectBackgroundColour(), out colour_num);
            while (colour_num == 0 || (colour_num <1 || colour_num>17))
            {
                userInterface.ErrorMessage("Please enter a NUMBER between 1 and 17 included.");
                Int32.TryParse(userInterface.SelectBackgroundColour(), out colour_num);
            }
            Console.BackgroundColor = (ConsoleColor)colour_num-1;
            config.BackgroundColour = (ConsoleColor)colour_num-1;
            Int32.TryParse(userInterface.SelectBackgroundColour(), out colour_num);
            while (colour_num == 0 || (colour_num < 1 || colour_num > 17))
            {
                userInterface.ErrorMessage("Please enter a NUMBER between 1 and 17 included.");
                Int32.TryParse(userInterface.SelectTextColour(), out colour_num);
            }
            Console.ForegroundColor = (ConsoleColor)colour_num-1;
            config.TextColour = (ConsoleColor)colour_num-1;
            configurationDatabaseFunctions.MakeNewConfiguration(config);
            return configurationDatabaseFunctions.ShowAll().Last().Id;
        }
        private void ConfigureColors()
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
            config.Id = currentUser.ConfigurationID;
            configurationDatabaseFunctions.ChangeConfiguration(config);
        }
    }
}
