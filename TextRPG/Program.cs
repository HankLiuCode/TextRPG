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
            //string level1 = File.ReadAllText("Levels\\level1.txt");
            //Console.WriteLine(level1);


            //foreach(string s in Map.CharArrayToStringArray(Map.StringToCharArray(level1)))
            //{
            //    Console.WriteLine(s);
            //}
            Game();

            
        }
        public static void Game()
        {
            // there is still a bug offset strange
            // string level1 = File.ReadFile("Levels\\level1.txt");

            string[] level = File.ReadAllLines("Levels\\level3.txt");
            
            Map map = new Map(level);

            Character player = new Character("Player", '@', map.Find('@'), new Stats(1, 10, 1, 1));
            map.Bind(player);

            Vector2[] wallPositions = map.FindAll('#');
            MonsterManager.FindMonsters(map, 'm');


            Window gameWindow = new Window(new Vector2(0, 0), new Vector2(50, 30));
            gameWindow.Write(map.GetStateStringArray());

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
            while (true)
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
                    player.SetPosition(nextPos);
                }


                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    player.ModifyHealth(-30);
                }

                gameWindow.Clear();
                gameWindow.Write(map.GetStateStringArray());

                Renderer.Render();
            }
        }
    }
}
