using System;
using TextRPG.Graphics;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using TextRPG.Audio;

namespace TextRPG
{
    
    class Program
    {
        public static void Main(string[] args)
        {
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

            Vector2 playerStartPos = GameManager.CurrentMap.FindCharPosition('@');
            Player player = new Player("Player", '@', playerStartPos, new Stats(1, 10, 1, 1));
            MapController.Bind(player, GameManager.CurrentMap);

            Window gameWindow = new Window(new Vector2(0, 0), new Vector2(50, 30));
            MonsterUI monsterUI = new MonsterUI(new Vector2(51, 14), new Vector2(50, 8));
            PlayerUI playerUI = new PlayerUI(new Vector2(51, 0), new Vector2(50, 14), player);

            gameWindow.Write(GameManager.CurrentMap.ToStringArray());

            Renderer.AddWindow(gameWindow);
            Renderer.AddWindow(monsterUI.GetWindow());
            Renderer.AddWindows(playerUI.GetWindows());
            Renderer.AddWindow(GameConsole.GetWindow());
            Renderer.Render();

            // Game Loop
            int step = 0;
            while (true)
            {
                step++;
                player.Update(step);

                gameWindow.Clear();
                gameWindow.Write(GameManager.CurrentMap.ToStringArray());
                Renderer.Render();
            }
        }
    }
}
