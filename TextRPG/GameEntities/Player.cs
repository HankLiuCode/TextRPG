using System;
using System.Collections.Generic;
using System.Text;

namespace TextRPG
{
    class Player : Character
    {
        public const float MAX_EXP = 100f;
        public float Experience { get; private set; }
        public Inventory Inventory { get; private set; }
        


        ConsoleKeyInfo keyInfo;


        public Player(string name, char symbol, Vector2 position, Stats stats) : base(name, symbol, position, stats)
        {
            Experience = 0;
            Inventory = new Inventory(5);
            MonsterManager.OnMonsterDied += MonsterManager_OnMonsterDied;
        }

        private void MonsterManager_OnMonsterDied(object sender, OnMonsterDiedEventArgs e)
        {
            Reward reward = e.diedMonster.Reward;
            Experience += reward.exp;

            foreach(Item item in reward.items)
            {
                Inventory.AddItem(item);
            }
        }

        public override void Update(int step)
        {
            if (healthState == HealthState.Dead)
                return;

            keyInfo = Console.ReadKey(true);
            Vector2 nextPos = Position;

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

            int index = -1;
            switch (keyInfo.Key)
            {
                case (ConsoleKey.D1):
                    index = 0;
                    break;
                case (ConsoleKey.D2):
                    index = 1;
                    break;
                case (ConsoleKey.D3):
                    index = 2;
                    break;
                case (ConsoleKey.D4):
                    index = 3;
                    break;
                case (ConsoleKey.D5):
                    index = 4;
                    break;
            }

            if(index != -1)
            {
                Item item = Inventory.UseItem(index);
                if (item == Item.Bomb)
                {
                    Monster[] bombedMonsters = new Monster[4];
                    bombedMonsters[0] = MonsterManager.GetMonster(Position + Vector2.Right);
                    bombedMonsters[1] = MonsterManager.GetMonster(Position + Vector2.Down);
                    bombedMonsters[2] = MonsterManager.GetMonster(Position + Vector2.Left);
                    bombedMonsters[3] = MonsterManager.GetMonster(Position + Vector2.Up);

                    for (int i = 0; i < bombedMonsters.Length; i++)
                    {
                        if (bombedMonsters[i] != null)
                        {
                            bombedMonsters[i].ModifyHealth(-50);
                        }
                    }
                }
                else if (item == Item.HealthPotion)
                {
                    ModifyHealth(20);
                }
                else if (item == Item.StrengthPotion)
                {
                    Stats = new Stats(Stats.armorClass, Stats.strength + 2, Stats.dexerity, Stats.accuracy);
                }
            }



            Monster monster = MonsterManager.GetMonster(nextPos);
            Obstacle obstacle = ObstacleManager.GetObstacle(nextPos);
            if (monster != null)
            {
                Attack(monster);
                monster.Attack(this);
            }
            else if (obstacle != null)
            {
               
            }
            else
            {
                Position = nextPos;
            }
        }
    }
}
