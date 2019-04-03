using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using project.Models;

namespace project.database_layer
{
    class NoteData
    {
        public List<Note> ShowAll()
        {
            var NoteList = new List<Note>();
            using (var connection = Connection.GetConnection())
            {
                var command = new SqlCommand("SELECT * FROM NOTE", connection);
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
        //Method used for creating new note.
        public void MakeNewNote(Note note)
        {
            using (var connection = Connection.GetConnection())
            {
                var command = new SqlCommand("INSERT INTO Note(Title, Description)" +
                "VALUES(@title, @description);", connection);

                command.Parameters.AddWithValue("title", note.Title);
                command.Parameters.AddWithValue("description", note.Description);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public Note ShowSpecificNote(int id)
        {
            Note note = null;
            using (var connection = Connection.GetConnection())
            {
                var command = new SqlCommand("SELECT * FROM Note WHERE Id = @id", connection);
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

        public void DeleteNote(int id)
        {
            using (var connection = Connection.GetConnection())
            {
                var command = new SqlCommand("DELETE note FROM Note where Id=@id;", connection);
                command.Parameters.AddWithValue("id", id);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void UpdateNote(Note note)
        {
            using (var connection = Connection.GetConnection())
            {
                var command = new SqlCommand("UPDATE Note SET Title=@title, Description=@description WHERE Id=@id", connection);
                command.Parameters.AddWithValue("id", note.Id);
                command.Parameters.AddWithValue("title", note.Title);
                command.Parameters.AddWithValue("description", note.Description);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
