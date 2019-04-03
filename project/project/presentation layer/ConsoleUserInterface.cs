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
            Console.WriteLine("Note Description\n" +
                              "(Press escape to stop writting)\n"+
                              "(Press anykey to continue)");
            Console.ReadKey();
            Console.Clear();


            List<string> text = new List<string>();
            text.Add("");
            int posX = 0;
            int posY = 0;

            ConsoleKeyInfo keyPressed;
            while (true)
            {
                keyPressed = Console.ReadKey();

                switch (keyPressed.Key)
                {
                    case ConsoleKey.Backspace:
                        {
                            if(posX==0 && text[posY].Length != 0)
                            {
                                posY--;
                                text[posY] += text[posY++];
                            }
                            if(posX == 0)
                            {
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
                            if(posX != text[posY].Length)
                            {
                                newRow = text[posY].Substring(posX);
                                text[posY] = text[posY].Remove(posX);
                            }

                            posY++;
                            posX = 0;
                            text.Insert(posY,newRow);

                            Console.Write(PrintText(text));
                            Console.SetCursorPosition(posX, posY);
                            break;
                        }
                    case ConsoleKey.Delete:
                        {
                            //todo
                            break;
                        }
                    case ConsoleKey.LeftArrow:
                        {
                            posX--;
                            if(posX<0)
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
                            Console.SetCursorPosition(posX,posY);
                            break;
                        }
                    case ConsoleKey.RightArrow:
                        {
                            posX++;
                            if(posX>text[posY].Length)
                            {
                                if(posY == text.Count)
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
                            if(posY < 0)
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
                            if(posY > text.Count - 1)
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
        string PrintText(List<string> text)
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
