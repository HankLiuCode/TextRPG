using System;
using System.Collections.Generic;

namespace TextRPG.Graphics
{   
    static class Renderer
    {
        static List<Window> renderList;
        static Dictionary<char, Pixel> _colorMapping;

        static Renderer()
        {
            renderList = new List<Window>();
            _colorMapping = new Dictionary<char, Pixel>();
            Console.CursorVisible = false;
        }

        public static void SetColorMapping(Dictionary<char, Pixel> colorMapping)
        {
            _colorMapping = colorMapping;
        }

        public static void AddWindow(Window window)
        {
            renderList.Add(window);
        }

        public static void AddWindows(Window[] windows)
        {
            foreach(Window w in windows)
            {
                AddWindow(w);
            }
        }

        public static void RemoveWindow(Window window)
        {
            renderList.Remove(window);
        }

        public static void Render()
        {

            // Solution 1
            //Console.Clear();



            foreach(Window w in renderList)
            {
                if (w.IsVisible)
                {
                    for (int y = 0; y < w.Rect.y; y++)
                    {
                        if (y < w.Buffer.Count)
                        {
                            
                            // colored version but too slow
                            //Console.SetCursorPosition(w.Position.x, w.Position.y + y);
                            //string line = w.Buffer[y];
                            //for (int i = 0; i < line.Length; i++)
                            //{
                            //    char c = line[i];
                            //    Console.BackgroundColor = _colorMapping.ContainsKey(c) ? _colorMapping[c].bgColor : ConsoleColor.Black;
                            //    Console.ForegroundColor = _colorMapping.ContainsKey(c) ? _colorMapping[c].color : ConsoleColor.White;
                            //    Console.Write(c);
                            //}

                            Console.SetCursorPosition(w.Position.x, w.Position.y + y);
                            Console.Write(w.Buffer[y]);
                        }
                    }
                }
                else
                {
                    // Solution 2
                    ClearWindowScreen(w);
                }
            }
        }

        private static void ClearWindowScreen(Window window)
        {
            for (int y = 0; y < window.Rect.y; y++)
            {
                if (y < window.Buffer.Count)
                {
                    Console.SetCursorPosition(window.Position.x, window.Position.y + y);
                    Console.Write(new string(' ', window.Buffer[y].Length));
                }
            }
        }


    }
}
