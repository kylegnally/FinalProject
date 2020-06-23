﻿using System;
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

        /// ***********************************************************************************
        ///         FIELDS
        /// ***********************************************************************************


        private int _leafCounter = 0;
        private string CurrentDir { get; }

        private CharacterFrequency[] _characterFrequencyObjectArray;

        private List<Node<CharacterFrequency>> _sortedNodeList;

        private CharacterFrequency[] _compactedArray;

        private CharacterFrequency[] _sortedArray;


        /// ***********************************************************************************
        ///         CONSTRUCTORS
        /// ***********************************************************************************


        /// <summary>
        /// String constructor for the CharacterManager class. 
        /// </summary>
        /// <param name="s"></param>
        public CharacterManager(string s)
        {
            ProcessString(s);
        }

        /// <summary>
        /// File (name as args) constructor for the CharacterManager class. 
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

        /// ***********************************************************************************
        ///         METHODS
        /// ***********************************************************************************


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
            //MakeNodeArray(SortFrequencies(CompactArray(CharacterFrequencyObjectArray)));
            _compactedArray = CompactArray(_characterFrequencyObjectArray);
            _sortedArray = SortFrequencies(_compactedArray);
            _sortedNodeList = MakeSortedNodeList(_sortedArray);
            BuildBinaryTree(_sortedNodeList);
        }

        private CharacterFrequency[] SortFrequencies(CharacterFrequency[] array)
        {
            int size = array.Length;
            bool sorted = false;
            while (!sorted)
            {
                foreach (CharacterFrequency freq in array)
                {
                    if (freq != null)
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
            }
            _characterFrequencyObjectArray = array;
            return array;
        }

        private void HandleInput(char[] chars)
        {
            char[] foundChars = new char[256];
            _characterFrequencyObjectArray = new CharacterFrequency[256];

            foreach (char aChar in chars)
            {
                for (int i = 0; i < _characterFrequencyObjectArray.Length; i++)
                {
                    int asciiVal = (int)aChar;
                    if (_characterFrequencyObjectArray[asciiVal] == null)
                    {
                        _characterFrequencyObjectArray[asciiVal] = new CharacterFrequency(aChar);
                        break;
                    }
                    else if (_characterFrequencyObjectArray[asciiVal].Equals(aChar))
                    {
                        _characterFrequencyObjectArray[asciiVal].IncrementFrequency();
                        break;
                    }
                }
            }
        }

        private void BuildBinaryTree(List<Node<CharacterFrequency>> sortedNodes)
        {
            // don't use a foreach here, use a for loop
            int combinedFrequencies;
            Node<CharacterFrequency> nextNode;
            Node<CharacterFrequency> currentNode;
            Node<CharacterFrequency> newNode = null;
            CharacterFrequency emptyNodeChar = new CharacterFrequency("\\");
            foreach (Node<CharacterFrequency> aNode in sortedNodes)
            {
                newNode = new Node<CharacterFrequency>(emptyNodeChar);
                combinedFrequencies = 0;
                int nodeIndex = sortedNodes.IndexOf(sortedNodes.GetEnumerator().Current);
                currentNode = aNode;
                nextNode = sortedNodes[nodeIndex + 1];

                combinedFrequencies = currentNode.Element.Frequency + nextNode.Element.Frequency;
                newNode.Element.Frequency = combinedFrequencies;
                newNode.Left = currentNode;
                newNode.Right = newNode;
            }
        }

        private void HandleInput(string[] s)
        {
            _characterFrequencyObjectArray = new CharacterFrequency[256];
            CharacterFrequency characterFrequencyObject;
            
            foreach (string aString in s)
            {
                int asciiVal = (int) char.Parse(aString);

                if (_characterFrequencyObjectArray[asciiVal] == null)
                {
                    _characterFrequencyObjectArray[asciiVal] = new CharacterFrequency(aString);
                }
                else if (_characterFrequencyObjectArray[asciiVal].Frequency > 0)
                    _characterFrequencyObjectArray[asciiVal].IncrementFrequency();
                else _characterFrequencyObjectArray[asciiVal] = new CharacterFrequency();
            }
        }

        private CharacterFrequency[] CompactArray(CharacterFrequency[] freqArray)
        {
            List<CharacterFrequency> tempList = new List<CharacterFrequency>();
            int i = 0;
            foreach (CharacterFrequency freqObj in freqArray)
            {
                if (freqObj != null) tempList.Add(freqObj);
            }

            _compactedArray = new CharacterFrequency[tempList.Count];


            foreach (CharacterFrequency freqObj in tempList)
            {
                _compactedArray[i] = freqObj;
                i++;
            }

            return _compactedArray;
        }

        private List<Node<CharacterFrequency>> MakeSortedNodeList(CharacterFrequency[] freqArray)
        {
            List<Node<CharacterFrequency>> _nodesList = new List<Node<CharacterFrequency>>();
            
            for (int i = 0; i < freqArray.Length; i++)
            {
                _nodesList.Add(new Node<CharacterFrequency>(freqArray[i]));
            }

            return _nodesList;
        }
    }
}
