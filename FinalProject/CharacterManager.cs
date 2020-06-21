using System;
using System.IO;
using System.Linq;

namespace FinalProject
{
    class CharacterManager
    {
        /// <summary>
        /// Property to store the current directory as a string.
        /// </summary>
        private string CurrentDir { get; }

        /// <summary>
        /// An array of CharacterFrequency objects.
        /// </summary>
        public CharacterFrequency[] CharacterFrequencyObjectArray;

        public CharacterManager(string s)
        {
            ProcessString(s);
        }

        /// <summary>
        /// Constructor for the CounterManager class. This construc
        /// </summary>
        /// <param name="args"></param>
        public CharacterManager(string[] args)
        {
            if (args.Length > 1)
            { string[] newArgs = new string[1];
                args = newArgs;
            }

            CurrentDir = Environment.CurrentDirectory;
            ReadFile(args);
            Environment.Exit(0);
        }
        /// <summary>
        /// Method to read the file from the current directory.
        /// Includes exception types. Assigns the appropriate exception
        /// to the Calls a method to process the file.
        /// </summary>
        /// <param name="args"></param>
        private void ReadFile(string[] args)
        {
            if (args != null)
            {

                if (File.Exists(args[0]))
                {
                    try
                    {
                        string fullPath = CurrentDir + "\\" + args[0];
                        using (StreamReader reader = new StreamReader(fullPath))
                        {
                            ProcessFile(reader);
                        }
                    }
                    catch
                    {
                        // Since we can use types in switch statements now, we can
                        // use the type of the exception as the case in the switch when
                        // determining which message the user should see.
                        // Exception messages are always obscure to users who aren't 
                        // programmers. This technique allows for an explanation.
                        throw new FileLoadException("Failed to load!");
                    }
                }
                else
                {
                    throw new FileNotFoundException("File not found!");
                }

            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public int[] SortArray(int[] array)
        {
            bool sorted = false;
            int size = array.Length;
            while (!sorted)
            {
                // the whole array
                foreach (int arrayMember in array)
                {
                    // for every array member 
                    for (int i = 1; i < size; i++)
                    {
                        // do a comparison
                        if (array[i] <= array[i - 1])
                        {
                            // do a swap
                            int temp = array[i];
                            array[i] = array[i - 1];
                            array[i - 1] = temp;
                            sorted = true;
                        }
                    }
                    size = size - 1;
                }
            }
            _resultingArray = array;
            return Result;
        }

        private void ProcessString(string s)
        {
            string[] sa = new string[s.Length];
            int i = 0;
            foreach (char c in s)
            {
                sa[i] = c.ToString();
                i++;
            }
        }

        /// <summary>
        /// Method to process the input file.
        /// </summary>
        /// <param name="file"></param>
        private void ProcessFile(StreamReader file)
        {
            // Are you going to use chars again? Maybe you should 
            // consider using strings. 
            char[] chars;
            string contents = file.ReadToEnd();
            chars = contents.ToCharArray();
            HandleInput(chars);
        }

        private void HandleInput(char[] chars)
        {
            char[] foundChars = new char[256];
            CharacterFrequencyObjectArray = new CharacterFrequency[256];

            foreach (char aChar in chars)
            {
                for (int i = 0; i < CharacterFrequencyObjectArray.Length; i++)
                {
                    int asciiVal = (int)aChar;
                    if (CharacterFrequencyObjectArray[asciiVal] == null)
                    {
                        CharacterFrequencyObjectArray[asciiVal] = new CharacterFrequency(aChar);
                        break;
                    }
                    else if (CharacterFrequencyObjectArray[asciiVal].Equals(aChar))
                    {
                        CharacterFrequencyObjectArray[asciiVal].IncrementFrequency();
                        break;
                    }
                }
            }

            SortFrequencies(CharacterFrequencyObjectArray);

        }

        private CharacterFrequency[] SortFrequencies(CharacterFrequency[] characterFrequencyObjectArray)
        {
            bool sorted = false;
            int size = characterFrequencyObjectArray.Length;
            while (!sorted)
            {
                // the whole array
                foreach (CharacterFrequency freqObj in characterFrequencyObjectArray)
                {
                    // for every array member 
                    for (int i = 1; i < size; i++)
                    {
                        // do a comparison
                        if (characterFrequencyObjectArray[i] <= characterFrequencyObjectArray[i - 1])
                        {
                            // do a swap
                            int temp = array[i];
                            array[i] = array[i - 1];
                            array[i - 1] = temp;
                            sorted = true;
                        }
                    }
                    size = size - 1;
                }
            }
            _resultingArray = array;
            return Result;
        }

        private void HandleInput(string[] s)
        {
            char[] foundChars = new char[256];
            CharacterFrequencyObjectArray = new CharacterFrequency[256];
            CharacterFrequency characterFrequencyObject;

            foreach (string aString in s)
            {
                for (int i = 0; i < CharacterFrequencyObjectArray.Length; i++)
                {
                    int asciiVal = int.Parse(aString);
                    if (CharacterFrequencyObjectArray[asciiVal] == null)
                    {
                        CharacterFrequencyObjectArray[asciiVal] = new CharacterFrequency(aString);
                        break;
                    }
                    else if (CharacterFrequencyObjectArray[asciiVal].Equals(aString))
                    {
                        CharacterFrequencyObjectArray[asciiVal].IncrementFrequency();
                        break;
                    }
                }
            }

            // CharacterFrequencyObjectArray is what we want to use further. We'll sort it first.

        }

        private void SortStringsArray(CharacterFrequency[] stringsArray)
        {
            // Time for a bubble sort!
        }
    }
}
