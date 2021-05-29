using System;
using System.Collections.Generic;
using TextRPG.GUI;
using Newtonsoft.Json;

namespace TextRPG
{
    
    class Program
    {
        public static void Main(string[] args)
        {
            Player player = new Player("Player");
            MonsterMenu monsterMenu = new MonsterMenu(80, 25);
            PlayerMenu playerMenu = new PlayerMenu(80, 25, player);
            CombatMenu combatMenu = new CombatMenu(80, 25, player);
            MainMenu mainMenu = new MainMenu(80, 25, monsterMenu, playerMenu, combatMenu);
            mainMenu.Show();

            //string playerJson = JsonConvert.SerializeObject(player);
            //Player deserializedObject = (Player)JsonConvert.DeserializeObject(playerJson);
            //Console.WriteLine(deserializedObject);
        }
    }
}
