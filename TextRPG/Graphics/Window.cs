using System;
using System.Collections.Generic;
using System.Text;

namespace TextRPG.Graphics
{
    class Window
    {
        public Window()
        {

        }

        public static void Render()
        {
            Console.WriteLine("==============================================================");
            Console.WriteLine("#  (0)Quit (1)Combat (2)Inventory                |");
            Console.WriteLine("#                                                            |");
            Console.WriteLine("#------------------------------------------------------------|");
            Console.WriteLine("#  Player Status:                                            |");
            Console.WriteLine("#  Strength: 19.04   Dexterity: 9.57   Accuracy: 0.73 Exp: 0 |");
            Console.WriteLine("#                                                            |");
            Console.WriteLine("#                                                            |");
            Console.WriteLine("#                                                            |");
            Console.WriteLine("==============================================================");
        }

        public static void AddLine(string line)
        {

        }
    }
}
