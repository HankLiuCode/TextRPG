using System;

namespace TextRPG
{
    class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player();
            MonsterManager monsterManager = new MonsterManager();

            string userInput = "";
            do
            {
                Console.WriteLine("(1) Attack (2) Check Status (3) Change Monster |  (quit) Quit Game");
                Console.Write("Action: ");
                userInput = Console.ReadLine();
                Console.WriteLine();

                switch (userInput)
                {
                    case ("1"):
                        player.Attack(monsterManager.CurrentMonster);
                        break;

                    case ("2"):
                        player.PrintStatus();
                        Console.WriteLine();
                        monsterManager.CurrentMonster.PrintStatus();
                        break;

                    case ("3"):
                        monsterManager.NextMonster();
                        break;

                    default:
                        Console.WriteLine("Not a valid input.");
                        break;
                }

                Console.WriteLine();
            } while (userInput != "quit");
        }
    }
}
