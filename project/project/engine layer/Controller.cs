using System;
using System.Collections.Generic;
using System.Text;
using project.presentation_layer;
using project.Models;
using project.database_layer;

namespace project.engine_layer
{
    class Controller
    {
        private IUserInterface userInterface = new ConsoleUserInterface();
        private NoteData databaseFunctions = new NoteData();
        public Controller()
        {
            string selection = userInterface.SelectFunction();
            while (selection!="5")
            {
                if (int.Parse(selection) < 1 || int.Parse(selection) > 5)
                {
                    userInterface.ErrorMessage("Invalid number entered. Please enter a number between 1 and 5 included.");
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
                }
                selection = userInterface.SelectFunction();
            }
        }
        private void DeleteNote()
        {
            int note_num = ListNotes();
            databaseFunctions.DeleteNote(note_num);
        }
        private void UpdateNote()
        {

        }
        private void CreateNote()
        {
            Note newNote = userInterface.CreateNote();
            databaseFunctions.MakeNewNote(newNote);
        }
        private void PrintNote()
        {
            int note_num = ListNotes();
            userInterface.ViewNote(databaseFunctions.ShowSpecificNote(note_num));
        }
        private int ListNotes()
        {
            var available_notes = databaseFunctions.ShowAll();
            List<string> titles = new List<string>();
            foreach (var item in available_notes)
            {
                titles.Add(item.Title);
            }
            int note_num = int.Parse(userInterface.ViewNotesNames(titles));
            while (note_num < 1 || note_num > databaseFunctions.ShowAll().Count)
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
    }
}
