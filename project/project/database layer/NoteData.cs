﻿using System;
using System.Collections.Generic;
using System.Text;
using project.Models;
using System.Data.SqlClient;

namespace project.database_layer
{
    class NoteData
    {
        //Method used for Displaying all notes in the DB
        public List<Note> ShowAll()
        {
            var NoteList = new List<Note>();
            using (var connection = ConnectionDB.GetConnection())
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
                            reader.GetString(2)
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
            using (var connection = ConnectionDB.GetConnection())
            {
                var command = new SqlCommand("SET IDENTITY_INSERT Note ON " +
                "INSERT INTO Note(Id, Name, Description)" +
                "VALUES(@id, @name, @description);", connection);

                command.Parameters.AddWithValue("id", note.Id);
                command.Parameters.AddWithValue("name", note.Name);
                command.Parameters.AddWithValue("description", note.Description);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }

        }

        public Note ShowSpecificNote(int id)
        {
            Note note = null;
            using (var connection = ConnectionDB.GetConnection())
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
                        reader.GetString(2)
                        );
                    }
                }
                connection.Close();
            }

            return note;
        }

        public void DeleteNote(int id)
        {
            using (var connection = ConnectionDB.GetConnection())
            {
                var command = new SqlCommand("DELETE note FROM Note where Id=@id;", connection);
                command.Parameters.AddWithValue("id", id);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}


