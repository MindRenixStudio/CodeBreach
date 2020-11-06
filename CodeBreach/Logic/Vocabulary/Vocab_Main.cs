using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace CodeBreach.Logic.Vocabulary
{
    public class Vocab_Main
    {
        public void startVocab_HoneyPot(string password) //sort by len, here, csv
        {
            var timeStarted = DateTime.Now;
            long cumputedWords = 0;

            bool cracked = false;

            Console.ForegroundColor = ConsoleColor.Green;

            if (cracked == false)
            {
                using (var reader = new StreamReader(@"C:/Users/lpurnoch/source\repos/CodeBreach/CodeBreach/Logic/Vocabulary/Source/HoneyPot.csv"))
                {
                    List<string> list = new List<string>();

                    while (!reader.EndOfStream)
                    {
                        cumputedWords += 1;

                        var line = reader.ReadLine();
                        Console.WriteLine(line);

                        if (line == password)
                        {
                            Console.WriteLine();

                            Console.ForegroundColor = ConsoleColor.White;

                            Console.WriteLine("Password matched. - {0}", DateTime.Now.ToString());
                            Console.WriteLine("Time passed: {0}s", DateTime.Now.Subtract(timeStarted).TotalSeconds);
                            Console.WriteLine("Resolved password: {0}", password);
                            Console.WriteLine("Computed keys: {0}", cumputedWords);

                            Console.ForegroundColor = ConsoleColor.Green;

                            cracked = true;

                            break;
                        }
                    }
                }
            }
        }

        public void startVocab_Names(string password)
        {
            var timeStarted = DateTime.Now;
            long cumputedWords = 0;

            bool cracked = false;

            Console.ForegroundColor = ConsoleColor.Green;

            if (cracked == false)
            {
                using (var reader = new StreamReader(@"C:/Users/lpurnoch/source\repos/CodeBreach/CodeBreach/Logic/Vocabulary/Source/Names.csv"))
                {
                    List<string> list = new List<string>();

                    while (!reader.EndOfStream)
                    {
                        cumputedWords += 1;

                        var line = reader.ReadLine();
                        Console.WriteLine(line);

                        if (line == password)
                        {
                            Console.WriteLine();

                            Console.ForegroundColor = ConsoleColor.White;

                            Console.WriteLine("Password matched. - {0}", DateTime.Now.ToString());
                            Console.WriteLine("Time passed: {0}s", DateTime.Now.Subtract(timeStarted).TotalSeconds);
                            Console.WriteLine("Resolved password: {0}", password);
                            Console.WriteLine("Computed keys: {0}", cumputedWords);

                            Console.ForegroundColor = ConsoleColor.Green;

                            cracked = true;

                            break;
                        }
                    }
                }
            }
        }

        public static void startVocab_Keyboard_Comb(string password)
        {
            var timeStarted = DateTime.Now;
            long cumputedWords = 0;

            bool cracked = false;

            Console.ForegroundColor = ConsoleColor.Green;

            if (cracked == false)
            {
                using (var reader = new StreamReader(@"C:/Users/lpurnoch/source\repos/CodeBreach/CodeBreach/Logic/Vocabulary/Source/Keyboard_Comb.csv"))
                {
                    List<string> list = new List<string>();

                    while (!reader.EndOfStream)
                    {
                        cumputedWords += 1;

                        var line = reader.ReadLine();
                        Console.WriteLine(line);

                        if (line == password)
                        {
                            Console.WriteLine();

                            Console.ForegroundColor = ConsoleColor.White;

                            Console.WriteLine("Password matched. - {0}", DateTime.Now.ToString());
                            Console.WriteLine("Time passed: {0}s", DateTime.Now.Subtract(timeStarted).TotalSeconds);
                            Console.WriteLine("Resolved password: {0}", password);
                            Console.WriteLine("Computed keys: {0}", cumputedWords);

                            Console.ForegroundColor = ConsoleColor.Green;

                            cracked = true;

                            break;
                        }
                    }
                }
            }
        }
    }
}
