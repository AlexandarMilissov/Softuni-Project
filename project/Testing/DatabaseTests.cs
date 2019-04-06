using NUnit.Framework;
using project.Models;
using project.database_layer;
using System.Collections.Generic;

namespace Tests
{
    public class DatabaseTests
    {
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
        //TODO...
        public void DoesItGetTheCorrectIdOfANote()
        {

        }
        [Test]
        //TODO...
        public void DoesItDeleteNoteById()
        {

        }
        [Test]
        //TODO...
        public void DoesItUpdateNote()
        {

        }
        [Test]
        //TODO...
        public void DoesItUpdateConfiguration()
        {

        }
    }
}