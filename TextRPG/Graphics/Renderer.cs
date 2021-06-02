using System;
using System.Collections.Generic;
using System.Text;

namespace TextRPG.Graphics
{
    public struct WindowData
    {
        public Vector2 position;
        public Vector2 rect;
    }
    // how to clear console ????
    static class Renderer
    {
        static List<Window> renderList;
        static List<WindowData> windowData;

        static Renderer()
        {
            renderList = new List<Window>();
            windowData = new List<WindowData>();
        }

        public static void AddWindow(Window window)
        {
            renderList.Add(window);
        }

        public static void RemoveWindow(Window window)
        {
            ClearWindowScreen(window);
            renderList.Remove(window);
        }

        public static void Render()
        {
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
