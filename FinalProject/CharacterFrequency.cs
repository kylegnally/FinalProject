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
        public char Character { get; set; }

        public int Frequency { get; set; }

        public int ASCII { get; set; }

        public CharacterFrequency() { }

        public CharacterFrequency(char charToCheck)
        {
            Character = charToCheck;
            int _ascii = (int)charToCheck;
            ASCII = _ascii;
            IncrementFrequency();
        }

        public CharacterFrequency(string stringToCheck)
        {

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