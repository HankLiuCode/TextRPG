using System;
using TextRPG.GUI;

namespace TextRPG
{
    class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player();
            MonsterManager monsterManager = new MonsterManager();
            MonsterMenu monsterMenu = new MonsterMenu(80, 25, monsterManager);
            PlayerMenu playerMenu = new PlayerMenu(80, 25, player);
            CombatMenu combatMenu = new CombatMenu(80, 25, player, monsterManager);
            MainMenu mainMenu = new MainMenu(80, 25, monsterMenu, monsterManager, playerMenu, combatMenu);
            mainMenu.Show();
        }
    }
}
