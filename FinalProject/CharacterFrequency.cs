using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    class CharacterFrequency
    {
        private string _stringToCheck;
        public char Character { get; set; }

        public string CharacterAsAString { get; set; }

        public int Frequency { get; set; }

        public int ASCII { get; set; }

        public CharacterFrequency()
        {
            Frequency = 0;
        }

        public CharacterFrequency(char charToCheck)
        {
            Character = charToCheck;
            CharacterAsAString = charToCheck.ToString();
            int ascii = (int)charToCheck;
            ASCII = ascii;
            IncrementFrequency();
        }

        public CharacterFrequency(string stringToCheck)
        {
            _stringToCheck = stringToCheck;
            CharacterAsAString = _stringToCheck;
            char c = char.Parse(stringToCheck);
            int ascii = (int)c;
            Character = c;
            ASCII = ascii;
            IncrementFrequency();
        }

        public bool Equals(char charToCheck)
        {
            if (Character == charToCheck)
            {
                return true;
            }
            else
            {
                Character = Character;
            }
            return false;
        }

        public bool Equals(string stringToCheck)
        {
            if (_stringToCheck == stringToCheck)
            {
                return true;
            }
            else
            {
                Character = Character;
            }
            return false;
        }

        public void IncrementFrequency()
        {
            Frequency = Frequency + 1;
        }

        public override string ToString()
        {
            return Character.ToString();
        }
    }
}