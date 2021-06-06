using System;
using TextRPG.Graphics;

namespace TextRPG
{
    public class OnExpModifiedEventArgs : EventArgs
    {
        public float exp;
        public float maxExp;
        public OnExpModifiedEventArgs(float exp, float maxExp)
        {
            this.exp = exp;
            this.maxExp = maxExp;
        }
    }

    class Player : Character
    {
        public const float MAX_EXP = 100f;

        private float _experience;
        public float Experience 
        { 
            get 
            {
                return _experience;
            } 
            set 
            {
                _experience = value;
                OnExpModified?.Invoke(this, new OnExpModifiedEventArgs(value, MAX_EXP));
            } 
        }

        public Inventory Inventory { get; private set; }

        ConsoleKeyInfo keyInfo;

        public event EventHandler<OnExpModifiedEventArgs> OnExpModified;

        public Player(string name, char symbol, Vector2 position, Stats stats) : base(name, symbol, position, stats)
        {
            
            Experience = 0;
            Inventory = new Inventory(5);
            MonsterManager.OnMonsterDied += MonsterManager_OnMonsterDied;
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

            int numPressed = -1;
            switch (keyInfo.Key)
            {
                case (ConsoleKey.D1):
                    numPressed = 1;
                    break;
                case (ConsoleKey.D2):
                    numPressed = 2;
                    break;
                case (ConsoleKey.D3):
                    numPressed = 3;
                    break;
                case (ConsoleKey.D4):
                    numPressed = 4;
                    break;
                case (ConsoleKey.D5):
                    numPressed = 5;
                    break;
            }

            if (Experience >= MAX_EXP)
            {
                UseExperience(numPressed);
            }
            else
            {
                UseItem(numPressed - 1);
            }



            Door door = GameManager.GetDoor(nextPos);
            Monster monster = MonsterManager.GetMonster(nextPos);
            Obstacle obstacle = ObstacleManager.GetObstacle(nextPos);

            if (monster != null)
            {
                Attack(monster);
                monster.Attack(this);
            }
            else if (door != null)
            {
                Vector2 direction = nextPos - Position;
                GameManager.CurrentMap.SetChar(Position, '.');
                GameManager.LoadMap(door.map);
                Position = door.position + direction;
                GameManager.CurrentMap.SetChar(Position, '@');
                MapController.Bind(this, GameManager.CurrentMap);
            }
            else if (obstacle != null)
            {

            }
            else
            {
                Position = nextPos;
            }
        }

        public void UseExperience(int index)
        {
            if (index == 1)
            {
                Stats = Stats.PlusStrength(1);
                Experience -= MAX_EXP;
            }
            else if(index == 2)
            {
                Stats = Stats.PlusArmorClass(1);
                Experience -= MAX_EXP;
            }
            else if (index == 3)
            {
                Stats = Stats.PlusDexerity(1);
                Experience -= MAX_EXP;
            }
            else if (index == 4)
            {
                Stats = Stats.PlusAccuracy(1);
                Experience -= MAX_EXP;
            }
        }

        public void UseItem(int index)
        {
            if (index >= 0 )
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
                    Stats = Stats.PlusStrength(2);
                }
            }
        }

        private void MonsterManager_OnMonsterDied(object sender, OnMonsterDiedEventArgs e)
        {
            Reward reward = e.diedMonster.Reward;
            Experience += reward.exp;

            foreach (Item item in reward.items)
            {
                Inventory.AddItem(item);
            }
        }
    }
}
