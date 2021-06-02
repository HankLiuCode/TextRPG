using System;
using System.Collections.Generic;
using TextRPG.GUI;
using TextRPG.Graphics;
using Newtonsoft.Json;

namespace TextRPG
{
    
    class Program
    {
        public static void Main(string[] args)
        {
            
            Window window1 = new Window(new Vector2(1, 1), new Vector2(20, 5));
            window1.Write("Test11234563142412342");
            window1.Write("Test23213213124323423421");
            window1.Write("Test3321312314341241432142");

            Window window2 = new Window(new Vector2(15, 0), new Vector2(10, 5));
            window2.Write("nTest1");
            window2.Write("nTest2");
            window2.Write("nTest3");

            Window window3 = new Window(new Vector2(10, 10), new Vector2(8, 8));
            window2.Write("nTest1");
            window2.Write("nTest2");
            window2.Write("nTest3");

            Renderer.AddWindow(window1);
            Renderer.AddWindow(window2);
            Renderer.AddWindow(window3);
            Renderer.Render();

            ConsoleKeyInfo keyInfo = Console.ReadKey();
            while (true)
            {
                keyInfo = Console.ReadKey(true);
                if(keyInfo.Key == ConsoleKey.UpArrow)
                {
                    window1.Position = new Vector2(window1.Position.x, window1.Position.y - 1);
                }
                else if (keyInfo.Key == ConsoleKey.LeftArrow)
                {
                    window1.Position = new Vector2(window1.Position.x - 1, window1.Position.y);
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    window1.Position = new Vector2(window1.Position.x, window1.Position.y + 1);
                }
                else if (keyInfo.Key == ConsoleKey.RightArrow)
                {
                    window1.Position = new Vector2(window1.Position.x + 1, window1.Position.y);
                }

                if (keyInfo.Key == ConsoleKey.Spacebar)
                {
                    if (window1.IsVisible)
                        window1.Hide();
                    else
                        window1.Show();
                }
                Renderer.Render();
            }


            //Player player = new Player("Player");
            //MonsterMenu monsterMenu = new MonsterMenu(80, 25);
            //PlayerMenu playerMenu = new PlayerMenu(80, 25, player);
            //CombatMenu combatMenu = new CombatMenu(80, 25, player);
            //MainMenu mainMenu = new MainMenu(80, 25, monsterMenu, playerMenu, combatMenu);
            //mainMenu.Show();

            //string playerJson = JsonConvert.SerializeObject(player);
            //Player deserializedObject = (Player)JsonConvert.DeserializeObject(playerJson);
            //Console.WriteLine(deserializedObject);
        }
    }
}
