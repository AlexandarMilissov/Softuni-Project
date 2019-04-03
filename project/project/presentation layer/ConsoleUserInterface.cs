﻿using System;
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
            Console.Clear();

            Console.Write("Note name: \n");
            string result = Console.ReadLine();
            return result;
        }
        private string CreateNewNoteName(string oldName)
        {
            Console.Clear();
            Console.WriteLine("Press Enter to use the old title or write the new from the begging.");
            Console.WriteLine(oldName);
            string answer = Console.ReadLine();
            if(answer == "")
            {
                answer = oldName;
            }
            return answer;
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
                    default:
                        {
                            text[posY] = text[posY].Insert(posX, keyPressed.KeyChar.ToString());
                            posX++;
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
                              "(Press escape to stop writting)\n"+
                              "(Press anykey to continue)");
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
                              "(Press escape to stop writting)\n" +
                              "(Press anykey to continue)");
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
            Console.Write(note.Title + "\n\n" +
                          note.Description + "\n\n" +
                          "Press any key to continue");
            Console.ReadLine();
        }
        public string ViewNotesNames(List<string> noteNames)
        {
            int count = 1;
            foreach(string name in noteNames)
            {
                Console.WriteLine(count + ". " + name);
                count++;
            }
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
                          "5.Logout\n" +
                          "6.Exit the program\n" +
                          "Please write the number of the function you want to use and press 'Enter'\n");
            string answer = Console.ReadLine();
            return answer;
        }
        public User RegisterUser()
        {
            Console.Clear();
            Console.WriteLine("Username:");
            string username = Console.ReadLine();

            Console.WriteLine("Password:");
            string passowrd = Console.ReadLine();

            User user = new User(username,passowrd);
            return user;
        }
        public string StartUpMenu()
        {
            Console.Clear();
            Console.Write("What do you want to do?:\n" +
                          "1.Login\n" +
                          "2.Register new user\n" +
                          "Please write the number of the function you want to use and press 'Enter'\n");
            string answer = Console.ReadLine();
            return answer;
        }
        public string SelectColour()
        {
            Console.Clear();
            Console.WriteLine("Please write the number of the colour you want to use and press 'Enter'");

            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("1.Black");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("2.Blue");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("3.Cyan");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("5.DarkBlue");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("6.DarkCyan");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("7.DarkGray");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("8.DarkGreen");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("9.DarkMagenta");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("10.DarkRed");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("11.DarkYellow");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("12.Gray");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("13.Green");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("14.Magenta");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("15.Red");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("16.White");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("17.Yellow");

            string result = Console.ReadLine();
            return result;
        }
    }
}
