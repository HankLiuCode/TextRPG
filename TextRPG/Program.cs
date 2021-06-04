using System;
using TextRPG.Graphics;
using Newtonsoft.Json;
using TextRPG.Common;
using System.IO;

namespace TextRPG
{
    
    class Program
    {
        public static void Main(string[] args)
        {
            // wall : # black with brown background
            // ground: . white with dark purple background
            // player: @ yellow with darkpurple background
            // monster: m red with dark purple background

            //Dictionary<char, Pixel> colorMapping = new Dictionary<char, Pixel>();
            //colorMapping.Add('@', new Pixel(ConsoleColor.Black, ConsoleColor.Red));
            //colorMapping.Add('#', new Pixel(ConsoleColor.Black, ConsoleColor.White));
            //colorMapping.Add('.', new Pixel(ConsoleColor.Black, ConsoleColor.White));
            //colorMapping.Add(',', new Pixel(ConsoleColor.DarkBlue, ConsoleColor.White));
            //colorMapping.Add('\"', new Pixel(ConsoleColor.Black, ConsoleColor.Green));

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
            //string level = File.ReadAllText("Levels\\level4.txt");
            string[] level = File.ReadAllLines("Levels\\level2.txt");

            Map map = new Map(level);

            Character player = new Character("Player", '@', map.FindCharPosition('@'), new Stats(1, 10, 1, 1));
            MonsterManager.LoadMonsters(map, 'm');
            Vector2[] wallPositions = map.FindCharPositions('#');

            //MapController.Bind(player, map);


            Window gameWindow = new Window(new Vector2(0, 0), new Vector2(50, 30));
            gameWindow.Write(map.ToStringArray(0));

            PlayerUI playerUI = new PlayerUI(new Vector2(51, 0), new Vector2(50, 14), player);


            MonsterUI monsterUI = new MonsterUI(new Vector2(51, 14), new Vector2(50, 10));

            Window debugWindow = new Window(new Vector2(51, 24), new Vector2(50, 6));
            debugWindow.Write("Test message");

            Renderer.AddWindow(gameWindow);
            Renderer.AddWindows(playerUI.GetWindows());
            Renderer.AddWindow(monsterUI.GetWindow());
            Renderer.AddWindow(debugWindow);
            Renderer.Render();

            ConsoleKeyInfo keyInfo;
            while (player.IsActive)
            {
                keyInfo = Console.ReadKey(true);
                Vector2 nextPos = player.Position;

                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    nextPos += Vector2.Up;
                }
                else if (keyInfo.Key == ConsoleKey.LeftArrow)
                {
                    nextPos += Vector2.Left;
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    nextPos += Vector2.Down;
                }
                else if (keyInfo.Key == ConsoleKey.RightArrow)
                {
                    nextPos += Vector2.Right;
                }

                //check has wall
                bool hasWall = false;
                foreach (Vector2 wallPos in wallPositions)
                {
                    if (nextPos == wallPos)
                    {
                        hasWall = true;
                        break;
                    }
                }

                // check if there is monster
                Character monster = MonsterManager.GetMonster(nextPos);
                if (monster != null)
                {
                    player.Attack(monster);
                    monster.Attack(player);
                }
                else if (hasWall)
                {
                    debugWindow.Write("This is a wall");
                }
                else
                {
                    player.Position = nextPos;
                }


                gameWindow.Clear();
                gameWindow.Write(map.ToStringArray(0));

                Renderer.Render();
            }
        }
    }
}
