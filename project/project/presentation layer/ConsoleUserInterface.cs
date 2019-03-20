using System;
using System.Collections.Generic;
using System.Text;

namespace project.presentation_layer
{
    public class ConsoleUserInterface : IUserInterface
    {
        private string CreateNewNoteName()
        {
            Console.Clear();

            Console.Write("Note name: ");
            //Console.Read();
            string result = Console.ReadLine();
            return result;
        }
       private List<string> CreateNewNoteText()
        {
            Console.Clear();

            List<string> result = new List<string>();

            Console.WriteLine("Note Text, write ^%$*% to end:\n");

            while(true)
            {
                result.Add(Console.ReadLine());
                if (result[result.Count-1].ToLower() == "^%$*%")
                {
                    break;
                }
            }
            result.RemoveAt(result.Count-1);

            return result;
        }

        public List<string> CreateNote()
        {
            Console.Clear();

            string noteName = CreateNewNoteName();
            List<string> result = CreateNewNoteText();

            result.Reverse();
            result.Add(noteName);
            result.Reverse();

            return result;
        }

        public void ViewNote(string noteName, string note)
        {
            Console.Clear();
            Console.Write(noteName + "\n\n" +
                          note + "\n\n" +
                          "Press any key to continue");

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
