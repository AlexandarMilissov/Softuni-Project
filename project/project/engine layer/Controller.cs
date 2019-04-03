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
        private User currentUser = null;
        public Controller()
        {
            FirstMenu();
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
            newNote.Id = note_id;
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
                    note_num = item.Id;
                }
            }
            return note_num;
        }
        private void SecondMenu()
        {
            string selection = userInterface.SelectFunction();
            while (selection != "6")
            {
                if (int.Parse(selection) < 1 || int.Parse(selection) > 6)
                {
                    userInterface.ErrorMessage("Invalid number entered. Please enter a number between 1 and 6 included.");
                }
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
                    case "5":
                        FirstMenu();
                        break;
                }
                selection = userInterface.SelectFunction();
            }
        }
        private void FirstMenu()
        {
            int selection = int.Parse(userInterface.StartUpMenu());
            while (selection < 1 || selection > 2)
            {
                selection = int.Parse(userInterface.StartUpMenu());
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
                        SecondMenu();
                        user_exists = true;
                        break;
                    }
                }
                if(!user_exists)
                {
                    userInterface.ErrorMessage("Incorrect username or password.");
                    FirstMenu();
                }
            }
            else
            {
                User registering_user = userInterface.RegisterUser();
                userDatabaseFunctions.RegisterUser(registering_user);
                SecondMenu();
            }
        }
    }
}
