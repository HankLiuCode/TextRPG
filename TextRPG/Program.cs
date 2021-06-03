using System;
using System.Collections.Generic;
using TextRPG.GUI;
using TextRPG.Graphics;
using Newtonsoft.Json;
using TextRPG.Common;
namespace TextRPG
{
    
    class Program
    {
        public static void Main(string[] args)
        {
            Window debugWindow = new Window(new Vector2(51, 6), new Vector2(20, 6));

            string rawMap = String.Join("",
                    "####################\n",
                    "#@...........,,,,,,#\n",
                    "#.......m...#,,,,,,#\n",
                    "#...........#,,,,,,#\n",
                    "#...........########\n",
                    "#......#...........#\n",
                    "#...#.......m......#\n",
                    "#..................#\n",
                    "####################\n"
                    );
            Map map = new Map(rawMap);
            Player player = new Player("Player", '@', map.Find('@'));

            map.Bind(player);

            Window gameWindow = new Window(new Vector2(0, 0), new Vector2(50, 30));
            foreach (string line in map.GetStateStringArray())
            {
                gameWindow.Write(line);
            }

            Renderer.AddWindow(gameWindow);
            Renderer.AddWindow(debugWindow);
            
            ConsoleKeyInfo keyInfo;
            while (true)
            {
                keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    player.SetPosition(player.Position + Vector2.Up);
                }
                else if (keyInfo.Key == ConsoleKey.LeftArrow)
                {
                    player.SetPosition(player.Position + Vector2.Left);
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    player.SetPosition(player.Position + Vector2.Down);
                }
                else if (keyInfo.Key == ConsoleKey.RightArrow)
                {
                    player.SetPosition(player.Position + Vector2.Right);
                }

                if(keyInfo.Key == ConsoleKey.Enter)
                {
                    player.TakeDamage(30);
                }

                gameWindow.Clear();
                gameWindow.Write(map.GetStateStringArray());

                Renderer.Render();
            }
        }
    }
}
