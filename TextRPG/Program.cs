using System;
using TextRPG.Graphics;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;

namespace TextRPG
{
    
    class Program
    {
        public static void Main(string[] args)
        {
            //byte[] levelbytes = File.ReadAllBytes("Levels\\level1.txt");
            //Console.Write(BitConverter.ToString(levelbytes).Replace("-"," "));

            //string level = File.ReadAllText("Levels\\level2.txt", System.Text.Encoding.UTF8);
            //Map map = new Map(level);

            //char a = 'Y';
            //Vector2 from = Vector2.Zero;
            //Vector2 to = Vector2.One;
            //Console.WriteLine(string.Format("{0} {1} {2}", a, map.GetChar(from), map.GetChar(to)));
            //map.RoundSwitch(ref a, from, to);
            //Console.WriteLine(string.Format("{0} {1} {2}", a, map.GetChar(from), map.GetChar(to)));
            //Console.WriteLine(map);


            //Character player = new Character("Player", '@', Vector2.One, new Stats(1, 10, 1, 1));
            //MapController.Bind(player, map);

            //ConsoleKeyInfo keyInfo;
            //while (true)
            //{
            //    keyInfo = Console.ReadKey(true);
            //    if(keyInfo.Key == ConsoleKey.Enter)
            //    {
            //        player.Position = player.Position + Vector2.Down;
            //    }
            //}

            Game();
        }


        public static void Game()
        {
            Dictionary<char, Pixel> colorMapping = new Dictionary<char, Pixel>();
            colorMapping.Add('@', new Pixel(ConsoleColor.White));
            colorMapping.Add('#', new Pixel(ConsoleColor.White));
            colorMapping.Add('.', new Pixel(ConsoleColor.White));
            colorMapping.Add(',', new Pixel(ConsoleColor.Black, ConsoleColor.Green));
            colorMapping.Add('\"', new Pixel(ConsoleColor.Black, ConsoleColor.Green));
            Renderer.SetColorMapping(colorMapping);


            string level = File.ReadAllText("Levels\\level2.txt");
            Map map = new Map(level);
            Player player = new Player("Hank", '@', map.FindCharPosition('@'), new Stats(1, 10, 1, 1));
            MonsterManager.LoadMonsters(map, 'm');
            ObstacleManager.LoadObstacles(map, '#');

            MapController.Bind(player, map);



            Window gameWindow = new Window(new Vector2(0, 0), new Vector2(50, 30));
            PlayerUI playerUI = new PlayerUI(new Vector2(51, 0), new Vector2(50, 14), player);
            MonsterUI monsterUI = new MonsterUI(new Vector2(51, 14), new Vector2(50, 8));

            Window infoWindow = new Window(new Vector2(51, 22), new Vector2(50, 6));
            infoWindow.Write("Welcome to the summoners rift");

            gameWindow.Write(map.ToStringArray());

            Renderer.AddWindow(gameWindow);
            Renderer.AddWindows(playerUI.GetWindows());
            Renderer.AddWindow(monsterUI.GetWindow());
            Renderer.AddWindow(infoWindow);
            Renderer.Render();

            // Game Loop
            int step = 0;
            while (true)
            {
                step++;
                player.Update(step);


                gameWindow.Clear();
                gameWindow.Write(map.ToStringArray());
                Renderer.Render();
            }
        }
    }
}
