using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using project.Models;

namespace project.database_layer
{
    /// <summary>
    /// The class NoteData.
    /// </summary>
    public class NoteData
    {
        /// <summary>
        /// This method used to display every existing note in the Database from the SQL Server.
        /// </summary>
        /// <returns>List of all existing notes.</returns>
        public List<Note> ShowAll()
        {
            var NoteList = new List<Note>();
            using (var connection = Connection.GetConnection())
            {
                var command = new SqlCommand("SELECT * FROM Notes", connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var product = new Note(
                            reader.GetInt32(0),
                            reader.GetString(1),
                            reader.GetString(2),
                            reader.GetInt32(3)
                        );

                        NoteList.Add(product);
                    }

                }
                connection.Close();

                return NoteList;
            }
        }
        /// <summary>
        /// This method is used for making new note.
        /// </summary>
        /// <param name="note">Makes a note that gets parameters from a user.</param>
        public void MakeNewNote(Note note)
        {
            using (var connection = Connection.GetConnection())
            {
                var command = new SqlCommand("INSERT INTO Notes(Title, Description,UserID)" +
                "VALUES(@title, @description,@userid);", connection);

                command.Parameters.AddWithValue("title", note.Title);
                command.Parameters.AddWithValue("description", note.Description);
                command.Parameters.AddWithValue("userid", note.UserID);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        /// <summary>
        /// This method is used for showing a specific note.
        /// </summary>
        /// <param name="id"> This parameter is searched in the Notes table.</param>
        /// <returns></returns>
        public Note ShowSpecificNote(int id)
        {
            Note note = null;
            using (var connection = Connection.GetConnection())
            {
                var command = new SqlCommand("SELECT * FROM Notes WHERE NoteId = @id", connection);
                command.Parameters.AddWithValue("id", id);

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        note = new Note(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetInt32(3)
                        );
                    }
                }
                connection.Close();
            }

            return note;
        }
        /// <summary>
        /// This method is used for deleting note with a specific id.
        /// </summary>
        /// <param name="id">This parameter is searched in the Notes table.</param>
        public void DeleteNote(int id)
        {
            using (var connection = Connection.GetConnection())
            {
                var command = new SqlCommand("DELETE FROM Notes where NoteId=@id;", connection);
                command.Parameters.AddWithValue("id", id);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        /// <summary>
        /// This method is used for updating note.
        /// </summary>
        /// <param name="note">Gets a note with parameters given by the user.</param>
        public void UpdateNote(Note note)
        {
            using (var connection = Connection.GetConnection())
            {
                var command = new SqlCommand("UPDATE Notes SET Title=@title, Description=@description WHERE NoteId=@id", connection);
                command.Parameters.AddWithValue("id", note.NoteId);
                command.Parameters.AddWithValue("title", note.Title);
                command.Parameters.AddWithValue("description", note.Description);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
