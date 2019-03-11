using System;
using System.Collections.Generic;
using System.Text;

namespace project.presentation_layer
{
    class ConsoleUserInterface : IUserInterface
    {
        void IUserInterface.ErrorMessage(string message)
        {
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
