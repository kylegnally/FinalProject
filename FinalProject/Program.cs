﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    class Program
    {

        static void Main(string[] args)
        {
            string stringToEncode = ".kcjvhxjdhcvgzxkjdhvbxdj.rktghsdrjzgeulryskgfcusedtyf476r76r53 654reyfgvjx";
            args = new[] { "wap.txt" };


            if (args.Length > 0)
            {
                CharacterManager manager = new CharacterManager(args);
            }
            else
            {
                CharacterManager manager = new CharacterManager(stringToEncode);
            }
            //string stringToEncode = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaabbbbbbbbbbbbbbbbbbbbccccccccccddddddddfffffggggghhhh";

        }
    }
}
