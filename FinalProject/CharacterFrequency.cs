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
        public string String { get; set; }
        public char Character { get; set; }
        public int Frequency { get; set; }
        public int ASCVal { get; set; }

        public CharacterFrequency() { }

        public CharacterFrequency(char charToCheck)
        {
            Frequency = 0;
            Character = charToCheck;
            int _ascii = (int)charToCheck;
            ASCVal = _ascii;
            IncrementFrequency();
        }

        public CharacterFrequency(string stringToCheck)
        {
            String = stringToCheck;
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