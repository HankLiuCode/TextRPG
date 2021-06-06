using System;
using System.Collections.Generic;
using System.Text;
using TextRPG.Graphics;

namespace TextRPG
{
    public static class GameConsole
    {
        private static Window window;
        private static int MAX_LINE = 5;
        private static int line = 0;

        static GameConsole()
        {
            window = new Window(new Vector2(51, 22), new Vector2(50, 8));
        }

        public static void Write(string msg)
        {
            if(line > MAX_LINE)
            {
                window.Dequeue();
                window.Dequeue();
                line -= 2;
            }

            line += 2;
            window.Write(msg);
            window.Write("");
        }

        public static void Clear()
        {
            line = 0;
            window.Clear();
        }

        public static Window GetWindow()
        {
            return window;
        }
    }
}
