using System;
using System.Collections.Generic;

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
            // Problem: Console flickers
            // Console.Clear();

            // Solution 2
            // Problem: Console will leave a track when moved
            foreach (Window w in renderList)
            {
                if (!w.IsVisible)
                {
                    ClearWindowScreen(w);
                }
            }

            foreach (Window w in renderList)
            {
                if (w.IsVisible)
                {
                    for (int y = 0; y < w.Rect.y; y++)
                    {
                        if (y < w.Buffer.Count)
                        {
                            // black and white version
                            Console.SetCursorPosition(w.Position.x, w.Position.y + y);
                            Console.Write(w.Buffer[y]);
                        }
                    }
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
