using System;
using TextRPG.Graphics;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace TextRPG
{
    
    class Program
    {
        public static void Main(string[] args)
        {
            //Dictionary<string, Door> doorDict = new Dictionary<string, Door>();

            //Door door = new Door(new Map(".", "map2"), Vector2.One);
            //Door door2 = new Door(new Map("#", "map3"), Vector2.One * 2);

            //doorDict.Add(door.map.Name + door.position, door2);

            //Door keyDoor = new Door(new Map(".", "map2"), Vector2.One);

            //Console.WriteLine(doorDict[keyDoor.map.Name + keyDoor.position]);

            Game();
        }

        public static void Game()
        {
            // Question: How to accelerate render process
            Dictionary<char, Pixel> colorMapping = new Dictionary<char, Pixel>();
            colorMapping.Add('@', new Pixel(ConsoleColor.White));
            colorMapping.Add('#', new Pixel(ConsoleColor.White));
            colorMapping.Add('.', new Pixel(ConsoleColor.White));
            colorMapping.Add(',', new Pixel(ConsoleColor.Black, ConsoleColor.Green));
            colorMapping.Add('\"', new Pixel(ConsoleColor.Black, ConsoleColor.Green));
            Renderer.SetColorMapping(colorMapping);

            GameManager.Initialize();

            //Vector2 playerPos = MapManager.CurrentMap.FindCharPosition('@');
            Player player = new Player("Player", '@', Vector2.One, new Stats(1, 10, 1, 1));
            MapController.Bind(player, GameManager.CurrentMap);

            Window gameWindow = new Window(new Vector2(0, 0), new Vector2(50, 30));
            MonsterUI monsterUI = new MonsterUI(new Vector2(51, 14), new Vector2(50, 8));
            PlayerUI playerUI = new PlayerUI(new Vector2(51, 0), new Vector2(50, 14), player);
            Window infoWindow = new Window(new Vector2(51, 22), new Vector2(50, 6));
            infoWindow.Write("Welcome to the summoners rift");

            gameWindow.Write(GameManager.CurrentMap.ToStringArray(1));

            Renderer.AddWindow(gameWindow);
            Renderer.AddWindow(monsterUI.GetWindow());
            Renderer.AddWindows(playerUI.GetWindows());
            Renderer.AddWindow(infoWindow);
            Renderer.Render();

            // Game Loop
            int step = 0;
            while (true)
            {
                step++;
                player.Update(step);

                gameWindow.Clear();
                gameWindow.Write(GameManager.CurrentMap.ToStringArray(1));
                Renderer.Render();
            }
        }
    }
}
