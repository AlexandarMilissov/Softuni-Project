using System;
using System.Collections.Generic;
using System.Text;
using project.Models;

namespace project.presentation_layer
{
    public class ConsoleUserInterface : IUserInterface
    {
        private string CreateNewNoteName()
        {
            Console.Clear();

            Console.Write("Note name: \n");
            string result = Console.ReadLine();
            return result;
        }
        private string CreateNewNoteText()
        {
            Console.Clear();

            List<string> result = new List<string>();
            string exitKey = ":wq";

            Console.WriteLine($"Note Description \n" +
                              $"(Write '{exitKey}' on the next row to end the note):\n");

            while(true)
            {
                result.Add(Console.ReadLine());
                if (result[result.Count-1] == exitKey)
                {
                    break;
                }
            }
            result.RemoveAt(result.Count-1);

            string text = "";
            foreach (var s in result)
            {
                text += s;
                text += "\n";
            }
            
            return text;
        }

        public Note CreateNote()
        {
            Console.Clear();

            string noteName = CreateNewNoteName();
            string text = CreateNewNoteText();

            Note note = new Note(noteName, text);

            return note;
        }

        public void ViewNote(Note note)
        {
            Console.Clear();
            Console.Write(note.Title + "\n\n" +
                          note.Description + "\n\n" +
                          "Press any key to continue");
            Console.Read();
        }

        public string ViewNotesNames(List<string> noteNames)
        {
            int count = 1;
            foreach(string name in noteNames)
            {
                Console.WriteLine(count + ". " + name);
                count++;
            }
            Console.Write("Please write the number of the funtion you want to use and press 'Enter'\n");
            string answer = Console.ReadLine();
            return answer;
        }

        public void ErrorMessage(string message)
        {
            Console.Clear();
            Console.Write(message + "\n" +
                          "Press any key to continue");
            Console.ReadLine();
        }

        public string SelectFunction()
        {
            Console.Clear();
            Console.Write("What do you want to do?:\n" +
                          "1.Create note\n" +
                          "2.Read note\n" +
                          "3.Update note\n" +
                          "4.Delete note\n" +
                          "5.Exit the program\n" +
                          "Please write the number of the funtion you want to use and press 'Enter'\n");
            string answer = Console.ReadLine();
            return answer;
        }
    }
}
