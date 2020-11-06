using System;
using System.Linq;
using CodeBreach.Logic.BruteForce;
using CodeBreach.Logic.Vocabulary;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using System.IO;
using CodeBreach.Logic;
using System.Threading;

namespace CodeBreach
{
    class Program
    {
        private static string password = "p123";
        private static string result;

        private static bool isMatched = false;

        private static int charactersToTestLength = 0;
        private static int charactersToTestLength2 = 0;
        private static long computedKeys = 0;

        private static char[] charactersToTest =
        {
        'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j',
        'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't',
        'u', 'v', 'w', 'x', 'y', 'z','A','B','C','D','E',
        'F','G','H','I','J','K','L','M','N','O','P','Q','R',
        'S','T','U','V','W','X','Y','Z','1','2','3','4','5',
        '6','7','8','9','0','!','$','#','@','-'
        };

        private static char[] charactersToTest2 =
        {
        'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j',
        'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't',
        'u', 'v', 'w', 'x', 'y', 'z'
        };

        private static char[] charactersToTest3 =
        {
        'z', 'y', 'x', 'w', 'v', 'u', 't', 's', 'r', 'q',
        'p', 'o', 'n', 'm', 'l', 'k', 'j', 'i', 'h', 'g',
        'f', 'e', 'd', 'c', 'b', 'a'
        };




        public static bool cracked = false;
        public static long computedWords;

        static void Main(string[] args)
        {

            Vocab_Main vocab = new Vocab_Main();
            //vocab.startVocab_Names("");

            //startProgramBrute1();
            Parallel.Invoke(
                () => startProgramBrute1()
                );

            //password = Console.ReadLine();

            /*Parallel.Invoke(
                () => startVocab_HoneyPot(password),
                () => startVocab_Names(password),
                () => startVocab_KeyboardComb(password),
                () => startVocab_FamilyNames(password),
                () => startVocab_FemaleNames(password),
                () => startVocab_MaleNames(password),
                () => startVocab_PopularLetterPasses(password),
                () => startVocab_UserPassCombo(password),
                () => startVocab_10M_Usernames1(password),
                () => startVocab_10M_Usernames2(password),
                () => startVocab_10M_Usernames3(password),
                () => startVocab_10M_Usernames4(password),
                () => startVocab_10M_Usernames5(password),
                () => startVocab_10M_Usernames6(password),
                () => startVocab_10M_Usernames7(password),
                () => startVocab_10M_Usernames8(password),
                () => startVocab_10M_Usernames9(password)
            );*/

            Console.ReadLine();
        }

        private static void startProgramBrute1 ()
        {
            password = Console.ReadLine();

            var timeStarted = DateTime.Now;
            Console.WriteLine("Start BruteForce - {0}", timeStarted.ToString());

            // The length of the array is stored permanently during runtime
            charactersToTestLength2 = charactersToTest2.Length;

            // The length of the password is unknown, so we have to run trough the full search space
            var estimatedPasswordLength = 0;

            while (!isMatched)
            {
                /* The estimated length of the password will be increased and every possible key for this
                 * key length will be created and compared against the password */
                estimatedPasswordLength++;
                startBruteForce(estimatedPasswordLength);
            }

            Console.WriteLine("Password matched. - {0}", DateTime.Now.ToString());
            Console.WriteLine("Time passed: {0}s", DateTime.Now.Subtract(timeStarted).TotalSeconds);
            Console.WriteLine("Resolved password: {0}", result);
            Console.WriteLine("Computed keys: {0}", computedKeys);
        }
        private static void startBruteForce(int keyLength)
        {
            var keyChars = createCharArray(keyLength, charactersToTest2[0]);
            // The index of the last character will be stored for slight perfomance improvement
            var indexOfLastChar = keyLength - 1;
            createNewKey(0, keyChars, keyLength, indexOfLastChar);
        }
        private static char[] createCharArray(int length, char defaultChar)
        {
            return (from c in new char[length] select defaultChar).ToArray();
        }
        private static void createNewKey(int currentCharPosition, char[] keyChars, int keyLength, int indexOfLastChar)
        {
            var nextCharPosition = currentCharPosition + 1;
            // We are looping trough the full length of our charactersToTest array
            for (int i = 0; i < charactersToTestLength2; i++)
            {
                /* The character at the currentCharPosition will be replaced by a
                 * new character from the charactersToTest array => a new key combination will be created */
                keyChars[currentCharPosition] = charactersToTest2[i];

                // The method calls itself recursively until all positions of the key char array have been replaced
                if (currentCharPosition < indexOfLastChar)
                {
                    createNewKey(nextCharPosition, keyChars, keyLength, indexOfLastChar);
                }
                else
                {
                    // A new key has been created, remove this counter to improve performance
                    computedKeys++;

                    Console.ForegroundColor = ConsoleColor.Green;

                    Console.Write($"Thread: {Thread.CurrentThread.ManagedThreadId}" + " - ");
                    Console.WriteLine(keyChars);

                    /* The char array will be converted to a string and compared to the password. If the password
                     * is matched the loop breaks and the password is stored as result. */
                    if ((new String(keyChars)) == password)
                    {
                        if (!isMatched)
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            isMatched = true;
                            result = new String(keyChars);
                        }
                        return;
                    }
                }
            }
        }


        private static void startProgramBrute2()
        {
            password = Console.ReadLine();

            var timeStarted = DateTime.Now;
            Console.WriteLine("Start BruteForce - {0}", timeStarted.ToString());

            // The length of the array is stored permanently during runtime
            charactersToTestLength = charactersToTest3.Length;

            // The length of the password is unknown, so we have to run trough the full search space
            var estimatedPasswordLength2 = 0;

            while (!isMatched)
            {
                /* The estimated length of the password will be increased and every possible key for this
                 * key length will be created and compared against the password */
                estimatedPasswordLength2++;
                startBruteForce2(estimatedPasswordLength2);
            }

            Console.WriteLine("Password matched. - {0}", DateTime.Now.ToString());
            Console.WriteLine("Time passed: {0}s", DateTime.Now.Subtract(timeStarted).TotalSeconds);
            Console.WriteLine("Resolved password: {0}", result);
            Console.WriteLine("Computed keys: {0}", computedKeys);
        }
        private static void startBruteForce2(int keyLength)
        {
            var keyChars = createCharArray2(keyLength, charactersToTest3[0]);
            // The index of the last character will be stored for slight perfomance improvement
            var indexOfLastChar2 = keyLength - 1;
            createNewKey2(0, keyChars, keyLength, indexOfLastChar2);
        }
        private static char[] createCharArray2(int length, char defaultChar)
        {
            return (from c in new char[length] select defaultChar).ToArray();
        }
        private static void createNewKey2(int currentCharPosition, char[] keyChars, int keyLength, int indexOfLastChar)
        {
            var nextCharPosition = currentCharPosition + 1;
            // We are looping trough the full length of our charactersToTest array
            for (int i = 0; i < charactersToTestLength; i++)
            {
                /* The character at the currentCharPosition will be replaced by a
                 * new character from the charactersToTest array => a new key combination will be created */
                keyChars[currentCharPosition] = charactersToTest3[i];

                // The method calls itself recursively until all positions of the key char array have been replaced
                if (currentCharPosition < indexOfLastChar)
                {
                    createNewKey2(nextCharPosition, keyChars, keyLength, indexOfLastChar);
                }
                else
                {
                    // A new key has been created, remove this counter to improve performance
                    computedKeys++;

                    Console.ForegroundColor = ConsoleColor.Green;

                    Console.Write($"Thread: {Thread.CurrentThread.ManagedThreadId}" + " - ");
                    Console.WriteLine(keyChars);

                    /* The char array will be converted to a string and compared to the password. If the password
                     * is matched the loop breaks and the password is stored as result. */
                    if ((new String(keyChars)) == password)
                    {
                        if (!isMatched)
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            isMatched = true;
                            result = new String(keyChars);
                        }
                        return;
                    }
                }
            }
        }
















        static void startVocab_HoneyPot(string password) //sort by len, here, csv
        {
            var timeStarted = DateTime.Now;

            List<List_Type> list = new List<List_Type>();
            
            Console.ForegroundColor = ConsoleColor.Green;
            
            if (cracked == false)
            {
                using (var reader = new StreamReader(@"C:/Users/lpurnoch/source\repos/CodeBreach/CodeBreach/Logic/Vocabulary/Source/HoneyPot.csv"))
                {

                    while (!reader.EndOfStream && cracked == false)
                    {
                        computedWords += 1;
                        
                        var line = reader.ReadLine();

                        Console.Write($"Thread: {Thread.CurrentThread.ManagedThreadId}" + " - ");
                        Console.WriteLine(line);
                        
                        if (line == password)
                        {
                            Console.WriteLine();

                            Console.ForegroundColor = ConsoleColor.White;

                            Console.WriteLine("Password matched. - {0}", DateTime.Now.ToString());
                            Console.WriteLine("Time passed: {0}s", DateTime.Now.Subtract(timeStarted).TotalSeconds);
                            Console.WriteLine("Resolved password: {0}", password);
                            Console.WriteLine("Computed words: {0}", computedWords);
                                                        
                            cracked = true;

                            break;
                        }
                    }
                }
            }
        }
        static void startVocab_Names(string password)
        {
            var timeStarted = DateTime.Now;
            
            Console.ForegroundColor = ConsoleColor.Green;

            if (cracked == false)
            {
                using (var reader = new StreamReader(@"C:/Users/lpurnoch/source\repos/CodeBreach/CodeBreach/Logic/Vocabulary/Source/Names.csv"))
                {
                    List<string> list = new List<string>();

                    while (!reader.EndOfStream && cracked == false)
                    {
                        computedWords += 1;
                        
                        var line = reader.ReadLine();

                        Console.Write($"Thread: {Thread.CurrentThread.ManagedThreadId}" + " - ");
                        Console.WriteLine(line);

                        if (line == password)
                        {
                            Console.WriteLine();

                            Console.ForegroundColor = ConsoleColor.White;

                            Console.WriteLine("Password matched. - {0}", DateTime.Now.ToString());
                            Console.WriteLine("Time passed: {0}s", DateTime.Now.Subtract(timeStarted).TotalSeconds);
                            Console.WriteLine("Resolved password: {0}", password);
                            Console.WriteLine("Computed words: {0}", computedWords);
                            
                            cracked = true;

                            break;
                        }
                    }
                }
            }
        }
        static void startVocab_KeyboardComb(string password)
        {
            var timeStarted = DateTime.Now;

            Console.ForegroundColor = ConsoleColor.Green;

            if (cracked == false)
            {
                using (var reader = new StreamReader(@"C:/Users/lpurnoch/source\repos/CodeBreach/CodeBreach/Logic/Vocabulary/Source/Keyboard_Comb.csv"))
                {
                    List<string> list = new List<string>();

                    while (!reader.EndOfStream && cracked == false)
                    {
                        computedWords += 1;
                                                
                        var line = reader.ReadLine();

                        Console.Write($"Thread: {Thread.CurrentThread.ManagedThreadId}" + " - ");
                        Console.WriteLine(line);

                        if (line == password)
                        {
                            Console.WriteLine();

                            Console.ForegroundColor = ConsoleColor.White;

                            Console.WriteLine("Password matched. - {0}", DateTime.Now.ToString());
                            Console.WriteLine("Time passed: {0}s", DateTime.Now.Subtract(timeStarted).TotalSeconds);
                            Console.WriteLine("Resolved password: {0}", password);
                            Console.WriteLine("Computed words: {0}", computedWords);

                            cracked = true;

                            break;
                        }
                    }
                }
            }
        }
        static void startVocab_FamilyNames(string password)
        {
            var timeStarted = DateTime.Now;

            Console.ForegroundColor = ConsoleColor.Green;

            if (cracked == false)
            {
                using (var reader = new StreamReader(@"C:/Users/lpurnoch/source\repos/CodeBreach/CodeBreach/Logic/Vocabulary/Source/FamilyNames.csv"))
                {
                    List<string> list = new List<string>();

                    while (!reader.EndOfStream && cracked == false)
                    {
                        computedWords += 1;

                        var line = reader.ReadLine();

                        Console.Write($"Thread: {Thread.CurrentThread.ManagedThreadId}" + " - ");
                        Console.WriteLine(line);

                        if (line == password)
                        {
                            Console.WriteLine();

                            Console.ForegroundColor = ConsoleColor.White;

                            Console.WriteLine("Password matched. - {0}", DateTime.Now.ToString());
                            Console.WriteLine("Time passed: {0}s", DateTime.Now.Subtract(timeStarted).TotalSeconds);
                            Console.WriteLine("Resolved password: {0}", password);
                            Console.WriteLine("Computed words: {0}", computedWords);

                            cracked = true;

                            break;
                        }
                    }
                }
            }
        }
        static void startVocab_FemaleNames(string password)
        {
            var timeStarted = DateTime.Now;

            Console.ForegroundColor = ConsoleColor.Green;

            if (cracked == false)
            {
                using (var reader = new StreamReader(@"C:/Users/lpurnoch/source\repos/CodeBreach/CodeBreach/Logic/Vocabulary/Source/FemaleNames.csv"))
                {
                    List<string> list = new List<string>();

                    while (!reader.EndOfStream && cracked == false)
                    {
                        computedWords += 1;

                        var line = reader.ReadLine();

                        Console.Write($"Thread: {Thread.CurrentThread.ManagedThreadId}" + " - ");
                        Console.WriteLine(line);

                        if (line == password)
                        {
                            Console.WriteLine();

                            Console.ForegroundColor = ConsoleColor.White;

                            Console.WriteLine("Password matched. - {0}", DateTime.Now.ToString());
                            Console.WriteLine("Time passed: {0}s", DateTime.Now.Subtract(timeStarted).TotalSeconds);
                            Console.WriteLine("Resolved password: {0}", password);
                            Console.WriteLine("Computed words: {0}", computedWords);

                            cracked = true;

                            break;
                        }
                    }
                }
            }
        }
        static void startVocab_MaleNames(string password)
        {
            var timeStarted = DateTime.Now;

            Console.ForegroundColor = ConsoleColor.Green;

            if (cracked == false)
            {
                using (var reader = new StreamReader(@"C:/Users/lpurnoch/source\repos/CodeBreach/CodeBreach/Logic/Vocabulary/Source/MaleNames.csv"))
                {
                    List<string> list = new List<string>();

                    while (!reader.EndOfStream && cracked == false)
                    {
                        computedWords += 1;

                        var line = reader.ReadLine();

                        Console.Write($"Thread: {Thread.CurrentThread.ManagedThreadId}" + " - ");
                        Console.WriteLine(line);

                        if (line == password)
                        {
                            Console.WriteLine();

                            Console.ForegroundColor = ConsoleColor.White;

                            Console.WriteLine("Password matched. - {0}", DateTime.Now.ToString());
                            Console.WriteLine("Time passed: {0}s", DateTime.Now.Subtract(timeStarted).TotalSeconds);
                            Console.WriteLine("Resolved password: {0}", password);
                            Console.WriteLine("Computed words: {0}", computedWords);

                            cracked = true;

                            break;
                        }
                    }
                }
            }
        }
        static void startVocab_PopularLetterPasses(string password)
        {
            var timeStarted = DateTime.Now;

            Console.ForegroundColor = ConsoleColor.Green;

            if (cracked == false)
            {
                using (var reader = new StreamReader(@"C:/Users/lpurnoch/source\repos/CodeBreach/CodeBreach/Logic/Vocabulary/Source/Popular_Letter_Passes.csv"))
                {
                    List<string> list = new List<string>();

                    while (!reader.EndOfStream && cracked == false)
                    {
                        computedWords += 1;

                        var line = reader.ReadLine();

                        Console.Write($"Thread: {Thread.CurrentThread.ManagedThreadId}" + " - ");
                        Console.WriteLine(line);

                        if (line == password)
                        {
                            Console.WriteLine();

                            Console.ForegroundColor = ConsoleColor.White;

                            Console.WriteLine("Password matched. - {0}", DateTime.Now.ToString());
                            Console.WriteLine("Time passed: {0}s", DateTime.Now.Subtract(timeStarted).TotalSeconds);
                            Console.WriteLine("Resolved password: {0}", password);
                            Console.WriteLine("Computed words: {0}", computedWords);

                            cracked = true;

                            break;
                        }
                    }
                }
            }
        }
        static void startVocab_UserPassCombo(string password)
        {
            var timeStarted = DateTime.Now;

            Console.ForegroundColor = ConsoleColor.Green;

            if (cracked == false)
            {
                using (var reader = new StreamReader(@"C:/Users/lpurnoch/source\repos/CodeBreach/CodeBreach/Logic/Vocabulary/Source/User_Pass_Combo.csv"))
                {
                    List<string> list = new List<string>();

                    while (!reader.EndOfStream && cracked == false)
                    {
                        computedWords += 1;

                        var line = reader.ReadLine();

                        Console.Write($"Thread: {Thread.CurrentThread.ManagedThreadId}" + " - ");
                        Console.WriteLine(line);

                        if (line == password)
                        {
                            Console.WriteLine();

                            Console.ForegroundColor = ConsoleColor.White;

                            Console.WriteLine("Password matched. - {0}", DateTime.Now.ToString());
                            Console.WriteLine("Time passed: {0}s", DateTime.Now.Subtract(timeStarted).TotalSeconds);
                            Console.WriteLine("Resolved password: {0}", password);
                            Console.WriteLine("Computed words: {0}", computedWords);

                            cracked = true;

                            break;
                        }
                    }
                }
            }
        }
        static void startVocab_10M_Usernames1(string password)
        {
            var timeStarted = DateTime.Now;

            Console.ForegroundColor = ConsoleColor.Green;

            if (cracked == false)
            {
                using (var reader = new StreamReader(@"C:/Users/lpurnoch/source\repos/CodeBreach/CodeBreach/Logic/Vocabulary/Source/10M_Usernames_1.csv"))
                {
                    List<string> list = new List<string>();

                    while (!reader.EndOfStream && cracked == false)
                    {
                        computedWords += 1;

                        var line = reader.ReadLine();

                        Console.Write($"Thread: {Thread.CurrentThread.ManagedThreadId}" + " - ");
                        Console.WriteLine(line);

                        if (line == password)
                        {
                            Console.WriteLine();

                            Console.ForegroundColor = ConsoleColor.White;

                            Console.WriteLine("Password matched. - {0}", DateTime.Now.ToString());
                            Console.WriteLine("Time passed: {0}s", DateTime.Now.Subtract(timeStarted).TotalSeconds);
                            Console.WriteLine("Resolved password: {0}", password);
                            Console.WriteLine("Computed words: {0}", computedWords);

                            cracked = true;

                            break;
                        }
                    }
                }
            }
        }
        static void startVocab_10M_Usernames2(string password)
        {
            var timeStarted = DateTime.Now;

            Console.ForegroundColor = ConsoleColor.Green;

            if (cracked == false)
            {
                using (var reader = new StreamReader(@"C:/Users/lpurnoch/source\repos/CodeBreach/CodeBreach/Logic/Vocabulary/Source/10M_Usernames_2.csv"))
                {
                    List<string> list = new List<string>();

                    while (!reader.EndOfStream && cracked == false)
                    {
                        computedWords += 1;

                        var line = reader.ReadLine();

                        Console.Write($"Thread: {Thread.CurrentThread.ManagedThreadId}" + " - ");
                        Console.WriteLine(line);

                        if (line == password)
                        {
                            Console.WriteLine();

                            Console.ForegroundColor = ConsoleColor.White;

                            Console.WriteLine("Password matched. - {0}", DateTime.Now.ToString());
                            Console.WriteLine("Time passed: {0}s", DateTime.Now.Subtract(timeStarted).TotalSeconds);
                            Console.WriteLine("Resolved password: {0}", password);
                            Console.WriteLine("Computed words: {0}", computedWords);

                            cracked = true;

                            break;
                        }
                    }
                }
            }
        }
        static void startVocab_10M_Usernames3(string password)
        {
            var timeStarted = DateTime.Now;

            Console.ForegroundColor = ConsoleColor.Green;

            if (cracked == false)
            {
                using (var reader = new StreamReader(@"C:/Users/lpurnoch/source\repos/CodeBreach/CodeBreach/Logic/Vocabulary/Source/10M_Usernames_3.csv"))
                {
                    List<string> list = new List<string>();

                    while (!reader.EndOfStream && cracked == false)
                    {
                        computedWords += 1;

                        var line = reader.ReadLine();

                        Console.Write($"Thread: {Thread.CurrentThread.ManagedThreadId}" + " - ");
                        Console.WriteLine(line);

                        if (line == password)
                        {
                            Console.WriteLine();

                            Console.ForegroundColor = ConsoleColor.White;

                            Console.WriteLine("Password matched. - {0}", DateTime.Now.ToString());
                            Console.WriteLine("Time passed: {0}s", DateTime.Now.Subtract(timeStarted).TotalSeconds);
                            Console.WriteLine("Resolved password: {0}", password);
                            Console.WriteLine("Computed words: {0}", computedWords);

                            cracked = true;

                            break;
                        }
                    }
                }
            }
        }
        static void startVocab_10M_Usernames4(string password)
        {
            var timeStarted = DateTime.Now;

            Console.ForegroundColor = ConsoleColor.Green;

            if (cracked == false)
            {
                using (var reader = new StreamReader(@"C:/Users/lpurnoch/source\repos/CodeBreach/CodeBreach/Logic/Vocabulary/Source/10M_Usernames_4.csv"))
                {
                    List<string> list = new List<string>();

                    while (!reader.EndOfStream && cracked == false)
                    {
                        computedWords += 1;

                        var line = reader.ReadLine();

                        Console.Write($"Thread: {Thread.CurrentThread.ManagedThreadId}" + " - ");
                        Console.WriteLine(line);

                        if (line == password)
                        {
                            Console.WriteLine();

                            Console.ForegroundColor = ConsoleColor.White;

                            Console.WriteLine("Password matched. - {0}", DateTime.Now.ToString());
                            Console.WriteLine("Time passed: {0}s", DateTime.Now.Subtract(timeStarted).TotalSeconds);
                            Console.WriteLine("Resolved password: {0}", password);
                            Console.WriteLine("Computed words: {0}", computedWords);

                            cracked = true;

                            break;
                        }
                    }
                }
            }
        }
        static void startVocab_10M_Usernames5(string password)
        {
            var timeStarted = DateTime.Now;

            Console.ForegroundColor = ConsoleColor.Green;

            if (cracked == false)
            {
                using (var reader = new StreamReader(@"C:/Users/lpurnoch/source\repos/CodeBreach/CodeBreach/Logic/Vocabulary/Source/10M_Usernames_5.csv"))
                {
                    List<string> list = new List<string>();

                    while (!reader.EndOfStream && cracked == false)
                    {
                        computedWords += 1;

                        var line = reader.ReadLine();

                        Console.Write($"Thread: {Thread.CurrentThread.ManagedThreadId}" + " - ");
                        Console.WriteLine(line);

                        if (line == password)
                        {
                            Console.WriteLine();

                            Console.ForegroundColor = ConsoleColor.White;

                            Console.WriteLine("Password matched. - {0}", DateTime.Now.ToString());
                            Console.WriteLine("Time passed: {0}s", DateTime.Now.Subtract(timeStarted).TotalSeconds);
                            Console.WriteLine("Resolved password: {0}", password);
                            Console.WriteLine("Computed words: {0}", computedWords);

                            cracked = true;

                            break;
                        }
                    }
                }
            }
        }
        static void startVocab_10M_Usernames6(string password)
        {
            var timeStarted = DateTime.Now;

            Console.ForegroundColor = ConsoleColor.Green;

            if (cracked == false)
            {
                using (var reader = new StreamReader(@"C:/Users/lpurnoch/source\repos/CodeBreach/CodeBreach/Logic/Vocabulary/Source/10M_Usernames_6.csv"))
                {
                    List<string> list = new List<string>();

                    while (!reader.EndOfStream && cracked == false)
                    {
                        computedWords += 1;

                        var line = reader.ReadLine();

                        Console.Write($"Thread: {Thread.CurrentThread.ManagedThreadId}" + " - ");
                        Console.WriteLine(line);

                        if (line == password)
                        {
                            Console.WriteLine();

                            Console.ForegroundColor = ConsoleColor.White;

                            Console.WriteLine("Password matched. - {0}", DateTime.Now.ToString());
                            Console.WriteLine("Time passed: {0}s", DateTime.Now.Subtract(timeStarted).TotalSeconds);
                            Console.WriteLine("Resolved password: {0}", password);
                            Console.WriteLine("Computed words: {0}", computedWords);

                            cracked = true;

                            break;
                        }
                    }
                }
            }
        }
        static void startVocab_10M_Usernames7(string password)
        {
            var timeStarted = DateTime.Now;

            Console.ForegroundColor = ConsoleColor.Green;

            if (cracked == false)
            {
                using (var reader = new StreamReader(@"C:/Users/lpurnoch/source\repos/CodeBreach/CodeBreach/Logic/Vocabulary/Source/10M_Usernames_7.csv"))
                {
                    List<string> list = new List<string>();

                    while (!reader.EndOfStream && cracked == false)
                    {
                        computedWords += 1;

                        var line = reader.ReadLine();

                        Console.Write($"Thread: {Thread.CurrentThread.ManagedThreadId}" + " - ");
                        Console.WriteLine(line);

                        if (line == password)
                        {
                            Console.WriteLine();

                            Console.ForegroundColor = ConsoleColor.White;

                            Console.WriteLine("Password matched. - {0}", DateTime.Now.ToString());
                            Console.WriteLine("Time passed: {0}s", DateTime.Now.Subtract(timeStarted).TotalSeconds);
                            Console.WriteLine("Resolved password: {0}", password);
                            Console.WriteLine("Computed words: {0}", computedWords);

                            cracked = true;

                            break;
                        }
                    }
                }
            }
        }
        static void startVocab_10M_Usernames8(string password)
        {
            var timeStarted = DateTime.Now;

            Console.ForegroundColor = ConsoleColor.Green;

            if (cracked == false)
            {
                using (var reader = new StreamReader(@"C:/Users/lpurnoch/source\repos/CodeBreach/CodeBreach/Logic/Vocabulary/Source/10M_Usernames_8.csv"))
                {
                    List<string> list = new List<string>();

                    while (!reader.EndOfStream && cracked == false)
                    {
                        computedWords += 1;

                        var line = reader.ReadLine();

                        Console.Write($"Thread: {Thread.CurrentThread.ManagedThreadId}" + " - ");
                        Console.WriteLine(line);

                        if (line == password)
                        {
                            Console.WriteLine();

                            Console.ForegroundColor = ConsoleColor.White;

                            Console.WriteLine("Password matched. - {0}", DateTime.Now.ToString());
                            Console.WriteLine("Time passed: {0}s", DateTime.Now.Subtract(timeStarted).TotalSeconds);
                            Console.WriteLine("Resolved password: {0}", password);
                            Console.WriteLine("Computed words: {0}", computedWords);

                            cracked = true;

                            break;
                        }
                    }
                }
            }
        }
        static void startVocab_10M_Usernames9(string password)
        {
            var timeStarted = DateTime.Now;

            Console.ForegroundColor = ConsoleColor.Green;

            if (cracked == false)
            {
                using (var reader = new StreamReader(@"C:/Users/lpurnoch/source\repos/CodeBreach/CodeBreach/Logic/Vocabulary/Source/10M_Usernames_9.csv"))
                {
                    List<string> list = new List<string>();

                    while (!reader.EndOfStream && cracked == false)
                    {
                        computedWords += 1;

                        var line = reader.ReadLine();

                        Console.Write($"Thread: {Thread.CurrentThread.ManagedThreadId}" + " - ");
                        Console.WriteLine(line);

                        if (line == password)
                        {
                            Console.WriteLine();

                            Console.ForegroundColor = ConsoleColor.White;

                            Console.WriteLine("Password matched. - {0}", DateTime.Now.ToString());
                            Console.WriteLine("Time passed: {0}s", DateTime.Now.Subtract(timeStarted).TotalSeconds);
                            Console.WriteLine("Resolved password: {0}", password);
                            Console.WriteLine("Computed words: {0}", computedWords);

                            cracked = true;

                            break;
                        }
                    }
                }
            }
        }
    }
}
