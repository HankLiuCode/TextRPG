using System;
using System.Collections.Generic;
using System.Text;
using TextRPG.Common;

namespace TextRPG.Graphics
{   
    static class Renderer
    {
        static List<Window> renderList;

        static Renderer()
        {
            renderList = new List<Window>();
            Console.CursorVisible = false;
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
