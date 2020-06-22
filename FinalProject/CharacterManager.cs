using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Instrumentation;
using System.Runtime.CompilerServices;
using System.Text;

namespace FinalProject
{
    class CharacterManager
    {
        /// <summary>
        /// Property to store the current directory as a string.
        /// </summary>

        private int _leafCounter = 0;
        private string CurrentDir { get; }

        private CharacterFrequency[] _characterFreqencyArrayField;
        private Node<CharacterFrequency>[] _nodesArray;

        public CharacterFrequency[] CharacterFrequencyObjectArray { get; set; }
        public Node<CharacterFrequency>[] SortedNodesArray { get; set; }

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
            char[] chars;
            string contents = file.ReadToEnd();
            chars = contents.ToCharArray();
            HandleInput(chars);
        }

        public CharacterFrequency[] SortFrequencies(CharacterFrequency[] array)
        {
            int size = array.Length;
            bool sorted = false;
            while (!sorted)
            {
                foreach (CharacterFrequency freq in array)
                {
                    for (int i = 1; i < size; i++)
                    {
                        int freq1 = array[i].Frequency;
                        int freq2 = array[i - 1].Frequency;
                        if (freq1 <= freq2)
                        {
                            CharacterFrequency temp = array[i];
                            array[i] = array[i - 1];
                            array[i - 1] = temp;
                            sorted = true;
                        }
                    }
                    
                    size = size - 1;
                }
            }
            CharacterFrequencyObjectArray = array;
            return array;
        }

        private void HandleInput(char[] chars)
        {
            char[] foundChars = new char[256];
            _characterFreqencyArrayField = new CharacterFrequency[256];

            foreach (char aChar in chars)
            {
                for (int i = 0; i < _characterFreqencyArrayField.Length; i++)
                {
                    int asciiVal = (int)aChar;
                    if (_characterFreqencyArrayField[asciiVal] == null)
                    {
                        _characterFreqencyArrayField[asciiVal] = new CharacterFrequency(aChar);
                        break;
                    }
                    else if (_characterFreqencyArrayField[asciiVal].Equals(aChar))
                    {
                        _characterFreqencyArrayField[asciiVal].IncrementFrequency();
                        break;
                    }
                }
            }

            CharacterFrequencyObjectArray = _characterFreqencyArrayField;
            MakeNodeArray(SortFrequencies(CompactArray(CharacterFrequencyObjectArray)));
            BuildBinaryTree(SortedNodesArray);
        }

        private void BuildBinaryTree(Node<CharacterFrequency>[] sortedNodesArray)
        {
            int pairFreqCount;
            int lowerFreq;
            int higherFreq;
            char nonLeafString = '\\';
            string contentsString;

            Node<CharacterFrequency> newNode;


            for (int i = 0; i < sortedNodesArray.Length; i++)
            {
                lowerFreq = sortedNodesArray[i].Element.Frequency;
                higherFreq = sortedNodesArray[i + 1].Element.Frequency;
                pairFreqCount = lowerFreq + higherFreq;
                newNode = new Node<CharacterFrequency>(new CharacterFrequency());
                newNode.Element.Character = nonLeafString;
                newNode.Element.Frequency = pairFreqCount;
                newNode.Left.Element = sortedNodesArray[i].Element;
                newNode.Right.Element = sortedNodesArray[i + 1].Element;
            }
        }

        private void HandleInput(string[] s)
        {
            CharacterFrequencyObjectArray = new CharacterFrequency[256];
            CharacterFrequency characterFrequencyObject;
            
            foreach (string aString in s)
            {
                int asciiVal = (int) char.Parse(aString);

                if (CharacterFrequencyObjectArray[asciiVal] == null)
                {
                    CharacterFrequencyObjectArray[asciiVal] = new CharacterFrequency(aString);
                }
                else if (CharacterFrequencyObjectArray[asciiVal].Frequency > 0)
                    CharacterFrequencyObjectArray[asciiVal].IncrementFrequency();
                else CharacterFrequencyObjectArray[asciiVal] = new CharacterFrequency();
            }

            SortFrequencies(CompactArray(CharacterFrequencyObjectArray));
        }

        private CharacterFrequency[] CompactArray(CharacterFrequency[] freqArray)
        {
            List<CharacterFrequency> tempList = new List<CharacterFrequency>();
            int i = 0;
            foreach (CharacterFrequency freqObj in freqArray)
            {
                if (freqObj != null) tempList.Add(freqObj);
            }

            CharacterFrequency[] compacted = new CharacterFrequency[tempList.Count];


            foreach (CharacterFrequency freqObj in tempList)
            {
                compacted[i] = freqObj;
                i++;
            }

            return compacted;
        }

        private Node<CharacterFrequency>[] MakeNodeArray(CharacterFrequency[] freqArray)
        {
            _nodesArray = new Node<CharacterFrequency>[freqArray.Length];

            for (int i = 0; 1 < freqArray.Length; i++)
            {
                _nodesArray[i] = new Node<CharacterFrequency>(freqArray[i]);
            }

            SortedNodesArray = _nodesArray;
            return _nodesArray;
        }
    }
}
