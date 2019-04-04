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
        //private Configuration DefaultConfig = new Configuration();
        public Controller()
        {
            //configurationDatabaseFunctions.MakeNewConfiguration(DefaultConfig);
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
            int note_num = int.Parse(userInterface.ViewNotesNames(titles));
            while (note_num < 1 || note_num > noteDatabaseFunctions.ShowAll().Count)
            {
                userInterface.ErrorMessage("No note with such ID exists in the database.");
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
            while(selection == 0 || (selection < 1 || selection > 6))
            {
                userInterface.ErrorMessage("Invalid input detected. Please enter a NUMBER between 1 and 7 included.");
                Int32.TryParse(userInterface.SelectFunction(), out selection);
            }
            while (selection != 6)
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
                        StartMenu();
                        break;
                }
                Int32.TryParse(userInterface.SelectFunction(), out selection);
                while (selection == 0 || (selection < 1 || selection > 6))
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
            while (selection == 0 || (selection < 1 || selection > 2))
            {
                userInterface.ErrorMessage("Please enter a NUMBER between 1 and 2 included.");
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
            else
            {
                User registering_user = userInterface.RegisterUser();
                //registering_user.ConfigurationID = DefaultConfig.Id;
                userDatabaseFunctions.RegisterUser(registering_user);
                ConfigurationMenu();
                OperationsMenu();
            }
        }
        private void ConfigurationMenu()
        {
            Configuration config = new Configuration();
            int colour_num = 0;
            Int32.TryParse(userInterface.SelectBackgroundColour(), out colour_num);
            while (colour_num == 0 || (colour_num <1 || colour_num>17))
            {
                userInterface.ErrorMessage("Please enter a NUMBER between 1 and 17 included.");
                Int32.TryParse(userInterface.SelectBackgroundColour(), out colour_num);
            }
            Console.BackgroundColor = (ConsoleColor)colour_num;
            config.BackgroundColour = (ConsoleColor)colour_num;
            Int32.TryParse(userInterface.SelectBackgroundColour(), out colour_num);
            while (colour_num == 0 || (colour_num < 1 || colour_num > 17))
            {
                userInterface.ErrorMessage("Please enter a NUMBER between 1 and 17 included.");
                Int32.TryParse(userInterface.SelectTextColour(), out colour_num);
            }
            Console.ForegroundColor = (ConsoleColor)colour_num;
            config.TextColour = (ConsoleColor)colour_num;
            configurationDatabaseFunctions.MakeNewConfiguration(config);
        }
    }
}
