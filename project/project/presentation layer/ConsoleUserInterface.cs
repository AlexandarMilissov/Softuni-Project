using System;
using System.Collections.Generic;
using System.Text;
using project.Models;
using System.Linq;

namespace project.presentation_layer
{
    public class ConsoleUserInterface : IUserInterface
    {
        /// <summary>
        /// Asks the user to write text.
        /// </summary>
        /// <param name="old"> Something the user can rewrite.</param>
        /// <param name="meta"> Text that should be written to the user. They can't edit it. </param>
        /// <param name="canEnter">If the user is allowed to add new rolls.</param>
        /// <returns></returns>
        private string CreateNoteInfo(string old,string meta,bool canEnter)
        {
            List<string> newName = StringToList(old);
            string name = TextEditor(newName, canEnter, StringToList(meta));
            return name;
        }
        /// <summary>
        /// Gives the ability the user to edit text.
        /// </summary>
        /// <param name="posX"></param>
        /// <param name="posY"></param>
        /// <param name="text"></param>
        /// <returns>The text in string format.</returns>
        private string TextEditor(List<string> text,bool canEnter,List<string> input)
        {
            int posY = text.Count - 1;
            int posX = text[posY].Length;
            PrintText(text, input);
            Console.SetCursorPosition(posX, posY + input.Count);
            ConsoleKeyInfo keyPressed;
            while (true)
            {
                keyPressed = Console.ReadKey();

                switch (keyPressed.Key)
                {
                    case ConsoleKey.Backspace:
                        {
                            if (posX == 0 && posY == 0)
                            {
                                break;
                            }
                            if (posX == 0)
                            {
                                if (text[posY].Length != 0)
                                {
                                    text[posY - 1] += text[posY];
                                }
                                text.RemoveAt(posY);
                                posY--;
                                posX = text[posY].Length;
                            }
                            else
                            {
                                text[posY] = text[posY].Remove(posX - 1, 1);
                                posX--;
                            }
                            PrintText(text, input);
                            Console.SetCursorPosition(posX, posY + input.Count);
                            continue;
                        }
                    case ConsoleKey.Escape:
                        {
                            string result = PrintText(text, input);
                            result = result.Remove(result.Length - 1,1);
                            return result;
                        }
                    case ConsoleKey.Enter:
                        {
                            if(!canEnter)
                            {
                                PrintText(text, input);
                                Console.SetCursorPosition(posX, posY + input.Count);
                                break;
                            }
                            string newRow = "";
                            if (posX != text[posY].Length)
                            {
                                newRow = text[posY].Substring(posX);
                                text[posY] = text[posY].Remove(posX);
                            }

                            posY++;
                            posX = 0;
                            text.Insert(posY, newRow);

                            PrintText(text, input);
                            Console.SetCursorPosition(posX, posY + input.Count);
                            break;
                        }
                    case ConsoleKey.Delete:
                        {
                            if (posY == text.Count - 1 && posX == text[text.Count - 1].Length)
                            {
                                PrintText(text, input);
                                Console.SetCursorPosition(posX, posY + input.Count);
                                break;
                            }
                            if (posX == text[posY].Length)
                            {
                                if (text[posY].Length != 0)
                                {
                                    text[posY] += text[posY + 1];
                                }
                                text.RemoveAt(posY + 1);
                            }
                            else
                            {
                                text[posY] = text[posY].Remove(posX, 1);
                            }
                            PrintText(text, input);
                            Console.SetCursorPosition(posX, posY + input.Count);
                            break;
                        }
                    case ConsoleKey.LeftArrow:
                        {
                            posX--;
                            if (posX < 0)
                            {
                                if (posY == 0)
                                {
                                    posX = 0;
                                }
                                else
                                {
                                    posY--;
                                    posX = text[posY].Length;
                                }
                            }
                            PrintText(text, input);
                            Console.SetCursorPosition(posX, posY + input.Count);
                            break;
                        }
                    case ConsoleKey.RightArrow:
                        {
                            posX++;
                            if (posX > text[posY].Length)
                            {
                                posY++;
                                if (posY > text.Count - 1)
                                {
                                    posY--;
                                    posX--;
                                }
                                else
                                {
                                    posX = 0;
                                }
                            }
                            PrintText(text, input);
                            Console.SetCursorPosition(posX, posY + input.Count);
                            break;
                        }
                    case ConsoleKey.UpArrow:
                        {
                            posY--;
                            if (posY < 0)
                            {
                                posY = 0;
                            }
                            else if (posX > text[posY].Length)
                            {
                                posX = text[posY].Length;
                            }
                            PrintText(text, input);
                            Console.SetCursorPosition(posX, posY + input.Count);
                            break;
                        }
                    case ConsoleKey.DownArrow:
                        {
                            posY++;
                            if (posY > text.Count - 1)
                            {
                                posY = text.Count - 1;
                            }
                            else if (posX > text[posY].Length)
                            {
                                posX = text[posY].Length;
                            }
                            PrintText(text, input);
                            Console.SetCursorPosition(posX, posY + input.Count);
                            break;
                        }
                    case ConsoleKey.Home:
                        {
                            posX = 0;
                            PrintText(text, input);
                            Console.SetCursorPosition(posX, posY + input.Count);
                            break;
                        }
                    case ConsoleKey.End:
                        {
                            posX = text[posY].Length;
                            PrintText(text, input);
                            Console.SetCursorPosition(posX, posY + input.Count);
                            break;
                        }
                    case ConsoleKey.PageDown:
                        {
                            posY = text.Count - 1;
                            if(posX > text[posY].Length)
                            {
                                posX = text[posY].Length;
                            }
                            PrintText(text, input);
                            Console.SetCursorPosition(posX, posY + input.Count);
                            break;
                        }
                    case ConsoleKey.PageUp:
                        {
                            posY = 0;
                            if (posX > text[posY].Length)
                            {
                                posX = text[posY].Length;
                            }
                            PrintText(text, input);
                            Console.SetCursorPosition(posX, posY + input.Count);
                            break;
                        }
                    default:
                        {
                            text[posY] = text[posY].Insert(posX, keyPressed.KeyChar.ToString());
                            posX++;
                            if(posX >= Console.BufferWidth - 1)
                            {
                                string newRow = "";

                                posY++;
                                posX = 0;
                                text.Insert(posY, newRow);

                                PrintText(text, input);
                                Console.SetCursorPosition(posX, posY);
                            }
                            PrintText(text, input);
                            Console.SetCursorPosition(posX, posY + input.Count);
                            break;
                        }
                }
            }
        }
        /// <summary>
        /// Prints a text on the screen.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private string PrintText(List<string> text, List<string> input)
        {
            Console.Clear();
            string output = ListToString(input);
            Console.Write(output);

            string result = ListToString(text);
            Console.Write(result);
            return result;
        }
        /// <summary>
        /// Prints list on all colours on the screen
        /// </summary>
        /// <param name="message"></param>
        /// <returns>What the user has written. It's supposed to be the number of the colour in the given list. You can cast it to 'ConsoleColor'</returns>
        private string SelectColour(string message)
        {
            Console.Clear();
            Console.WriteLine("Please write the number of the colour you want to use and press 'Enter'");
            Console.WriteLine(message);
            ConsoleColor BackgroundColor = Console.BackgroundColor;
            ConsoleColor TextColor = Console.ForegroundColor;

            for (int i = 0; i < Enum.GetNames(typeof(ConsoleColor)).Length; i++)
            {
                try
                {
                    ConsoleColor c = (ConsoleColor)i;
                    Console.ForegroundColor = c;
                    Console.WriteLine((i+1) + "." + c.ToString());
                }
                catch(System.ArgumentException)
                {
                    i++;
                }
            }

            Console.BackgroundColor = BackgroundColor;
            Console.ForegroundColor = TextColor;
            string result = Console.ReadLine();
            return result;
        }
        /// <summary>
        /// Creates a single string from list of strings, separates the with new row.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private string ListToString(List<string> text)
        {
            string result = "";
            foreach (string s in text)
            {
                result += s + "\n";
            }
            return result;
        }
        /// <summary>
        /// Creates a list of string from a single string, separates by with new row.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private List<string> StringToList(string text)
        {
            List<string> result = text.Split("\n").ToList();
            return result;
        }

        public Note CreateNote()
        {
            Console.Clear();

            string metaTitle = "Note title:\n" +
                              "(Press escape to stop writing)\n"
                              + "";
            string noteName = CreateNoteInfo("",metaTitle,false);



            string metaDesc = "Note Description\n" +
                              "(Press escape to stop writing)\n";
            string text = CreateNoteInfo("",metaDesc,true);

            Note note = new Note(noteName, text);

            return note;
        }
        public Note CreateNote(Note note)
        {
            Console.Clear();

            string metaTitle = "Edit note title:" +
                              "(Press escape to stop writing)\n"
                              + "";
            string noteName = CreateNoteInfo(note.Title,metaTitle,false);


            string metaDesc = "Note Description\n" +
                              "(Press escape to stop writing)\n"
                              + "";
            string text = CreateNoteInfo(note.Description,metaDesc,true);

            Note updatedNote = new Note(noteName, text);

            return updatedNote;
        }
        public void ViewNote(Note note)
        {
            Console.Clear();
            Console.Write("Title:\n"+
                          note.Title + "\n\n" +
                          "Description:\n"+
                          note.Description + "\n\n" +
                          "Press any key to continue");
            Console.ReadLine();
        }
        public string ViewNotesNames(List<string> noteNames)
        {
            Console.Clear();
            int count = 1;
            foreach(string name in noteNames)
            {
                Console.WriteLine(count + ". " + name);
                count++;
            }
            Console.WriteLine(count + "." +
                "(Return to the main menu)");
            Console.Write("Please write the number of the note you want to use and press 'Enter'\n");
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
                          "5.Configure window\n" +
                          "6.Logout\n" +
                          "7.Exit the program\n" +
                          "Please write the number of the function you want to use and press 'Enter'\n");
            string answer = Console.ReadLine();
            return answer;
        }
        public User RegisterUser()
        {
            Console.Clear();
            Console.WriteLine("Username:");
            string username = Console.ReadLine();

            Console.Write("Password:\n");
            string password = null;
            while (true)
            {
                var key = System.Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                    break;
                else if(key.Key == ConsoleKey.Backspace)
                {
                    if(password.Length == 0)
                    {
                        continue;
                    }
                    password = password.Substring(0,password.Length - 1);
                }
                else
                {
                    password += key.KeyChar;
                }
            }

            User user = new User(username,password);
            return user;
        }
        public string StartUpMenu()
        {
            Console.Clear();
            Console.Write("What do you want to do?:\n" +
                          "1.Login\n" +
                          "2.Register new user\n" +
                          "3.Exit\n" +
                          "Please write the number of the function you want to use and press 'Enter'\n");
            string answer = Console.ReadLine();
            return answer;
        }
        public string SelectBackgroundColour()
        {
            return SelectColour("Select your background colour");
        }
        public string SelectTextColour()
        {
            return SelectColour("Select your text colour");
        }
    }
}
