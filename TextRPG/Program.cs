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

        public void Temp()
        {
            Player player = new Player();
            MonsterManager monsterManager = new MonsterManager();

            bool hidePressedKey = true;
            bool quit = false;
            ConsoleKeyInfo keyInfo;
            do
            {

                Console.WriteLine("(0)Quit (1)Player Status (2)Choose Monster (3)Attack (4)Inventory (5)Revive Monster (6)Spawn Monster");
                keyInfo = Console.ReadKey(hidePressedKey);
                Console.WriteLine();

                switch (keyInfo.Key)
                {
                    case (ConsoleKey.D0):
                        Console.Write("Are you sure you want to quit? (0)Yes (1)No");
                        keyInfo = Console.ReadKey(hidePressedKey);
                        if (keyInfo.Key == ConsoleKey.D0)
                            quit = true;
                        break;

                    // Player Status
                    case (ConsoleKey.D1):
                        //player.PrintStatus();
                        Console.WriteLine();
                        break;

                    // Choose Monster
                    case (ConsoleKey.D2):
                        //monsterManager.PrintMonsters();
                        keyInfo = Console.ReadKey(hidePressedKey);
                        int index = int.Parse(keyInfo.KeyChar.ToString());
                        monsterManager.ChooseMonster(index);
                        break;

                    // Attack
                    case (ConsoleKey.D3):
                        player.Attack(monsterManager.CurrentMonster);
                        break;

                    // Bomb Attack
                    case (ConsoleKey.D4):
                        player.BombAttack(monsterManager.CurrentMonster);
                        break;

                    // Revive Monster
                    case (ConsoleKey.D5):
                        monsterManager.CurrentMonster.Revive();
                        break;

                    // Spawn Monster
                    case (ConsoleKey.D6):
                        monsterManager.SpawnMonster("Monster");
                        break;

                    default:
                        Console.WriteLine("Not a valid input.");
                        break;
                }

                Console.WriteLine("----------------------------------");
            } while (!quit);
        }
    }
}
