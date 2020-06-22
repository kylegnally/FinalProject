using System;
using System.IO;
using System.Linq;
using System.Management.Instrumentation;

namespace FinalProject
{
    class CharacterManager
    {
        /// <summary>
        /// Property to store the current directory as a string.
        /// </summary>
        private string CurrentDir { get; }

        private CharacterFrequency[] _characterFreqencyArrayField;

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

        

        /// <summary>
        /// Processes a given string into a string[] where each individual character of the original string is a member of the resulting array.
        /// </summary>
        /// <param name="s"></param>
        private void ProcessString(string s)
        {
            string[] sa = new string[s.Length];
            int i = 0;
            foreach (char c in s)
            {
                sa[i] = c.ToString();
                i++;
            }

            HandleInput(sa);
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


        //private void SortFrequencies(CharacterFrequency[] characterFrequencyObjectArray)
        //{
        //    bool sorted = false;
        //    int size = characterFrequencyObjectArray.Length;
        //    while (!sorted)
        //    {
        //        // the whole array
        //        foreach (CharacterFrequency freqObj in characterFrequencyObjectArray)
        //        {
        //            // for every array member 
        //            for (int i = 1; i < size + 1; i++)
        //            {
        //                // do a comparison
        //                if (characterFrequencyObjectArray[i].Frequency <= characterFrequencyObjectArray[i - 1].Frequency)
        //                {
        //                    // do a swap
        //                    CharacterFrequency temp = characterFrequencyObjectArray[i];
        //                    characterFrequencyObjectArray[i] = characterFrequencyObjectArray[i - 1];
        //                    characterFrequencyObjectArray[i - 1] = temp;
        //                    sorted = true;
        //                }
        //            }
        //            size = size - 1;
        //        }
        //    }
        //    CharacterFrequencyObjectArray = characterFrequencyObjectArray;
        //}

        public void SortFrequencies(CharacterFrequency[] array)
        {
            int size = array.Length;
            bool sorted = false;

            while (!sorted)
            {
                foreach (CharacterFrequency freq in array)
                {
                    //if (freq != null)
                    //{
                    //    frequency = freq.Frequency;
                    //    asciiVal = freq.ASCII;
                    //}

                    for (int i = 1; i < size; i++)
                    {
                        if (array[i] != null && array[i - 1] != null)
                        {
                            if (array[i].Frequency <= array[i - 1].Frequency)
                            {
                                // do a swap
                                CharacterFrequency temp = array[i];
                                array[i] = array[i - 1];
                                array[i - 1] = temp;
                                sorted = true;
                            }
                        }
                    }

                    size = size - 1;
                }
            }
            //bool sorted = false;
            //int size = array.Length;
            //while (!sorted)
            //{
            //    // the whole array
            //    foreach (CharacterFrequency arrayMember in array)
            //    {
            //        if (arrayMember != null)
            //        {
            //            // for every array member 
            //        for (int i = 1; i < size; i++)
            //        {
            //            // do a comparison
            //            if (array[i].Frequency <= array[i - 1].Frequency)
            //            {
            //                // do a swap
            //                CharacterFrequency temp = array[i];
            //                array[i] = array[i - 1];
            //                array[i - 1] = temp;
            //                sorted = true;
            //            }
            //        }
            //        size = size - 1;
            //        }

            //    }
            //}
            CharacterFrequencyObjectArray = array;
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

        private void HandleInput(string[] s)
        {
            //char[] foundChars = new char[256];
            CharacterFrequencyObjectArray = new CharacterFrequency[256];
            CharacterFrequency characterFrequencyObject;
            
            foreach (string aString in s)
            {
                // get our asciiVal
                int asciiVal = (int) char.Parse(aString);

                // if the entry at the index corresponding to the ascii value is null
                if (CharacterFrequencyObjectArray[asciiVal] == null)
                {
                    // make a CF object from that
                    CharacterFrequencyObjectArray[asciiVal] = new CharacterFrequency(aString);
                }
                else if (CharacterFrequencyObjectArray[asciiVal].Frequency > 0)
                    CharacterFrequencyObjectArray[asciiVal].IncrementFrequency();
                else CharacterFrequencyObjectArray[asciiVal] = new CharacterFrequency();
            }

            SortFrequencies(CharacterFrequencyObjectArray);

        }

        private void SortStringsArray(CharacterFrequency[] stringsArray)
        {
            // Time for a bubble sort!
        }
    }
}
