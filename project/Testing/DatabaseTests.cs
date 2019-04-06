using NUnit.Framework;
using project.Models;
using project.database_layer;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Tests
{
    public class DatabaseTests
    {
        private NoteData noteDatabaseFunctions = new NoteData();
        private ConfigurationData configurationDatabaseFunctions = new ConfigurationData();
        [Test]
        public void DoesItCreateNoteCorrectly()
        {
            Note note = new Note();
            note.NoteId = 1;
            note.Title = "FirstNote";
            note.Description = "FirstNotesDescription";
            note.UserID = 1;

            Assert.AreEqual(1,
                note.NoteId,
                "NoteId must be int and is incorrectly set.");

            Assert.AreEqual("FirstNote",
                note.Title,
                "Title must be string and is incorrectly set.");

            Assert.AreEqual("FirstNotesDescription",
                note.Description,
                "Description must be string and is incorrectly set.");

            Assert.AreEqual(1,
                note.UserID,
                "UserID must be int and is incorrectly set.");
        }
        [Test]
        public void DoesItCreateUserCorrectly()
        {
            User user = new User();
            user.UserId = 1;
            user.Username = "Gosho";
            user.Password = "123";
            user.ConfigurationID = 1;

            Assert.AreEqual(1
                , user.UserId,
                "UserId must be int and is incorrectly set.");

            Assert.AreEqual("Gosho",
                user.Username,
                "Username must be string and is incorrectly set.");

            Assert.AreEqual("123",
                user.Password,
                "Password must be string and is incorrectly set.");

            Assert.AreEqual(1,
                user.ConfigurationID,
                "ConfigurationID must be int and is incorrectly set.");
        }
        [Test]
        public void DoesItCreateConfigurationCorrectly()
        {
            Configuration configuration = new Configuration();

            configuration.Id = 1;
            configuration.TextColour = System.ConsoleColor.Red;
            configuration.BackgroundColour = System.ConsoleColor.Black;

            Assert.AreEqual(1, configuration.Id);
            Assert.AreEqual(System.ConsoleColor.Red,
                configuration.TextColour,
                "Colors are not identical.");

            Assert.AreEqual(System.ConsoleColor.Black,
                configuration.BackgroundColour,
                "Colors are not identical.");
        }
        [Test]
        public void DoesItShowAllNotesCorrectly()
        {
            Note note1 = new Note();
            note1.NoteId = 1;
            note1.Title = "FirstNote";
            note1.Description = "FirstNotesDescription";
            note1.UserID = 1;

            Note note2 = new Note();
            note2.NoteId = 2;
            note2.Title = "SecondNote";
            note2.Description = "SeondNotesDescription";
            note2.UserID = 2;

            var NoteList = new List<Note>();
            NoteList.Add(note1);
            NoteList.Add(note2);

            Assert.AreEqual(2, NoteList.Count, "The amount is incorrect.");
        }
        [Test]
        public void DoesItShowAllUsersCorrectly()
        {
            User user1 = new User();
            user1.UserId = 1;
            user1.Username = "Gosho";
            user1.Password = "123";
            user1.ConfigurationID = 1;

            User user2 = new User();
            user2.UserId = 2;
            user2.Username = "Pesho";
            user2.Password = "321";
            user2.ConfigurationID = 2;

            var UsersList = new List<User>();
            UsersList.Add(user1);
            UsersList.Add(user2);

            Assert.AreEqual(2, UsersList.Count, "The amount is incorrect.");
        }
        [Test]
        public void DoesItShowAllConfigurationsCorrectly()
        {
            Configuration configuration1 = new Configuration();
            configuration1.Id = 1;
            configuration1.TextColour = System.ConsoleColor.Red;
            configuration1.BackgroundColour = System.ConsoleColor.Black;

            Configuration configuration2 = new Configuration();
            configuration2.Id = 1;
            configuration2.TextColour = System.ConsoleColor.Red;
            configuration2.BackgroundColour = System.ConsoleColor.Black;

            var ConfigurationsList = new List<Configuration>();
            ConfigurationsList.Add(configuration1);
            ConfigurationsList.Add(configuration2);

            Assert.AreEqual(2, ConfigurationsList.Count, "The amount is incorrect.");
        }
        [Test]
        public void DoesItGetTheCorrectIdOfANote()
        {
            Note note = new Note("TestTitle", "SampleDescription");
            note.UserID = 1;
            noteDatabaseFunctions.MakeNewNote(note);
            note.NoteId = noteDatabaseFunctions.ShowAll().Last().NoteId;
            Assert.AreEqual(note.NoteId, noteDatabaseFunctions.ShowSpecificNote(noteDatabaseFunctions.ShowAll().Last().NoteId).NoteId);
            noteDatabaseFunctions.DeleteNote(note.NoteId);
        }
        [Test]
        public void DoesItDeleteNoteById()
        {
            Note note = new Note("SampleTitle", "SampleDescription");
            note.UserID = 1;
            Note note2 = new Note("Title", "Description");
            note2.UserID = 1;
            noteDatabaseFunctions.MakeNewNote(note);
            note.NoteId = noteDatabaseFunctions.ShowAll().Last().NoteId;
            noteDatabaseFunctions.MakeNewNote(note2);
            note2.NoteId = noteDatabaseFunctions.ShowAll().Last().NoteId;
            noteDatabaseFunctions.DeleteNote(noteDatabaseFunctions.ShowAll().Last().NoteId);
            Assert.AreNotEqual(note2.NoteId, noteDatabaseFunctions.ShowAll().Last().NoteId);
            noteDatabaseFunctions.DeleteNote(note.NoteId);
        }
        [Test]
        public void DoesItUpdateNote()
        {
            Note original_note = new Note("SampleTitle", "SampleDescription");
            original_note.UserID = 1;
            Note updated_note = new Note("UpdatedTitle", "UpdatedDescription");
            original_note.UserID = 1;
            noteDatabaseFunctions.MakeNewNote(original_note);
            updated_note.NoteId = noteDatabaseFunctions.ShowAll().Last().NoteId;
            noteDatabaseFunctions.UpdateNote(updated_note);
            Assert.AreEqual(updated_note.NoteId, noteDatabaseFunctions.ShowAll().Last().NoteId);
            noteDatabaseFunctions.DeleteNote(updated_note.NoteId);
        }
        [Test]
        public void DoesItUpdateConfiguration()
        {
            Configuration original_config = new Configuration();
            original_config.BackgroundColour = ConsoleColor.Black;
            original_config.TextColour = ConsoleColor.White;
            configurationDatabaseFunctions.MakeNewConfiguration(original_config);
            Configuration new_config = new Configuration();
            new_config.BackgroundColour = ConsoleColor.Yellow;
            new_config.TextColour = ConsoleColor.Black;
            new_config.Id = configurationDatabaseFunctions.ShowAll().Last().Id;
            configurationDatabaseFunctions.ChangeConfiguration(new_config);
            Assert.AreEqual(new_config.Id, configurationDatabaseFunctions.ShowAll().Last().Id);
        }
    }
}