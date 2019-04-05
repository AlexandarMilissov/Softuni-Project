using System;
using System.Collections.Generic;
using System.Text;
using project.Models;
using System.Linq;

namespace project.presentation_layer
{
    public class ConsoleUserInterface : IUserInterface
    {
        private string CreateNewNoteName()
        {
            string name = "";

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Note title:\n");
                Console.Write(name);
                Console.CursorLeft = name.Length;
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.Enter)
                    break;
                else if (key.Key == ConsoleKey.Backspace)
                {
                    if (name.Length == 0)
                    {
                        continue;
                    }
                    name = name.Substring(0, name.Length - 1);
                }
                else
                {
                    name += key.KeyChar;
                }
            }
            return name;
        }
        private string CreateNewNoteName(string oldName)
        {
            string newName = oldName;
            do
            {
                Console.Clear();
                Console.WriteLine("Edit note title");
                Console.Write(newName);
                Console.CursorLeft = newName.Length;

                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.Enter)
                {
                    break;
                }
                else if (key.Key == ConsoleKey.Backspace)
                {
                    if (newName.Length == 0)
                    {
                        continue;
                    }
                    newName = newName.Substring(0, newName.Length - 1);
                }
                else
                {
                    newName += key.KeyChar;
                }
                Console.Clear();
                Console.WriteLine("Edit note title");
                Console.Write(newName);
                Console.CursorLeft = newName.Length;
            } while (true);
            return newName;
        }
        private string TextEditor(int posX,int posY, List<string> text)
        {
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
                            Console.Write(PrintText(text));
                            Console.SetCursorPosition(posX, posY);
                            continue;
                        }
                    case ConsoleKey.Escape:
                        {
                            return PrintText(text);
                        }
                    case ConsoleKey.Enter:
                        {
                            string newRow = "";
                            if (posX != text[posY].Length)
                            {
                                newRow = text[posY].Substring(posX);
                                text[posY] = text[posY].Remove(posX);
                            }

                            posY++;
                            posX = 0;
                            text.Insert(posY, newRow);

                            Console.Write(PrintText(text));
                            Console.SetCursorPosition(posX, posY);
                            break;
                        }
                    case ConsoleKey.Delete:
                        {
                            if (posY == text.Count - 1 && posX == text[text.Count - 1].Length)
                            {
                                Console.Write(PrintText(text));
                                Console.SetCursorPosition(posX, posY);
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
                            Console.Write(PrintText(text));
                            Console.SetCursorPosition(posX, posY);
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
                            Console.Write(PrintText(text));
                            Console.SetCursorPosition(posX, posY);
                            break;
                        }
                    case ConsoleKey.RightArrow:
                        {
                            posX++;
                            if (posX > text[posY].Length)
                            {
                                if (posY == text.Count)
                                {
                                    posY--;
                                }
                                else
                                {
                                    posX = 0;
                                    posY++;
                                }
                            }
                            Console.Write(PrintText(text));
                            Console.SetCursorPosition(posX, posY);
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
                            Console.Write(PrintText(text));
                            Console.SetCursorPosition(posX, posY);
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
                            Console.Write(PrintText(text));
                            Console.SetCursorPosition(posX, posY);
                            break;
                        }
                    case ConsoleKey.Home:
                        {
                            posX = 0;
                            Console.Write(PrintText(text));
                            Console.SetCursorPosition(posX, posY);
                            break;
                        }
                    case ConsoleKey.End:
                        {
                            posX = text[posY].Length;
                            Console.Write(PrintText(text));
                            Console.SetCursorPosition(posX, posY);
                            break;
                        }
                    case ConsoleKey.PageDown:
                        {
                            posY = text.Count - 1;
                            if(posX > text[posY].Length)
                            {
                                posX = text[posY].Length;
                            }
                            Console.Write(PrintText(text));
                            Console.SetCursorPosition(posX, posY);
                            break;
                        }
                    case ConsoleKey.PageUp:
                        {
                            posY = 0;
                            if (posX > text[posY].Length)
                            {
                                posX = text[posY].Length;
                            }
                            Console.Write(PrintText(text));
                            Console.SetCursorPosition(posX, posY);
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

                                Console.Write(PrintText(text));
                                Console.SetCursorPosition(posX, posY);
                            }
                            Console.Write(PrintText(text));
                            Console.SetCursorPosition(posX, posY);
                            break;
                        }
                }
            }
        }
        private string CreateNewNoteText()
        {
            Console.Clear();
            Console.WriteLine("Note Description\n" +
                              "(Press escape to stop writing)\n"+
                              "(Press any key to continue)");
            Console.ReadKey();
            Console.Clear();


            List<string> text = new List<string>();
            text.Add("");
            int posX = 0;
            int posY = 0;

            string description = TextEditor(posX, posY, text);
            return description;
        }
        private string CreateNewNoteText(string oldDescription)
        {
            Console.Clear();
            Console.WriteLine("Update your note description\n" +
                              "(Press escape to stop writing)\n" +
                              "(Press any key to continue)");
            Console.ReadKey();
            Console.Clear();


            List<string> text = oldDescription.Split("\n").ToList();
            int posY = text.Count - 1;
            int posX = text[posY].Length;
            
            string description = TextEditor(posX, posY, text);
            return description;
            
        }
        private string PrintText(List<string> text)
        {
            Console.Clear();
            string result = "";
            foreach (string s in text)
            {
                result += s + "\n";
            }
            return result;
        }
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

        public Note CreateNote()
        {
            Console.Clear();

            string noteName = CreateNewNoteName();
            string text = CreateNewNoteText();

            Note note = new Note(noteName, text);

            return note;
        }
        public Note CreateNote(Note note)
        {
            Console.Clear();

            string noteName = CreateNewNoteName(note.Title);
            string text = CreateNewNoteText(note.Description);

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
