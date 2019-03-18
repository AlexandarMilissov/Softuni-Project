using System;
using System.Collections.Generic;
using System.Text;

namespace project.presentation_layer
{
    class ConsoleUserInterface : IUserInterface
    {
        string CreateNewNoteName()
        {
            Console.Clear();

            string result = "";



            return result;
        }

        public string[] CreateNote()
        {
            Console.Clear();

            string[] result = new string[2];



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

        void IUserInterface.ErrorMessage(string message)
        {
            Console.Clear();
            Console.Write(message + "\n" +
                          "Press any key to continue");
            Console.Read();
        }

        string IUserInterface.SelectFunction()
        {
            Console.Clear();
            Console.Write("What do you want to do?:\n" +
                          "1.Create note\n" +
                          "2.Read note\n" +
                          "3.Update note\n" +
                          "4.Delete note\n" +
                          "Please write the number of the funtion you want to use and press 'Enter'\n");
            string answer = Console.ReadLine();
            return answer;
        }
    }
}
