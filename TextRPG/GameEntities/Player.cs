using System;

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
            OnAttack += Player_OnAttack;
        }

        public override void Update(int step)
        {
            if (healthState == HealthState.Dead)
                return;

            keyInfo = Console.ReadKey(true);
            bool upPressed = keyInfo.Key == ConsoleKey.UpArrow || keyInfo.Key == ConsoleKey.W;
            bool leftPressed = keyInfo.Key == ConsoleKey.LeftArrow || keyInfo.Key == ConsoleKey.A;
            bool rightPressed = keyInfo.Key == ConsoleKey.RightArrow || keyInfo.Key == ConsoleKey.D;
            bool downPressed = keyInfo.Key == ConsoleKey.DownArrow || keyInfo.Key == ConsoleKey.S;
            bool movePressed = upPressed || leftPressed || downPressed || rightPressed;

            bool num1Pressed = keyInfo.Key == ConsoleKey.D1 || keyInfo.Key == ConsoleKey.NumPad1;
            bool num2Pressed = keyInfo.Key == ConsoleKey.D2 || keyInfo.Key == ConsoleKey.NumPad2;
            bool num3Pressed = keyInfo.Key == ConsoleKey.D3 || keyInfo.Key == ConsoleKey.NumPad3;
            bool num4Pressed = keyInfo.Key == ConsoleKey.D4 || keyInfo.Key == ConsoleKey.NumPad4;
            bool num5Pressed = keyInfo.Key == ConsoleKey.D5 || keyInfo.Key == ConsoleKey.NumPad5;

            Vector2 nextPos = Position;
            if (upPressed)
            {
                nextPos += Vector2.Up;
            }
            else if (leftPressed)
            {
                nextPos += Vector2.Left;
            }
            else if (downPressed)
            {
                nextPos += Vector2.Down;
            }
            else if (rightPressed)
            {
                nextPos += Vector2.Right;
            }


            int numPressed = -1;
            if (num1Pressed)
                numPressed = 1;
            else if (num2Pressed)
                numPressed = 2;
            else if (num3Pressed)
                numPressed = 3;
            else if (num4Pressed)
                numPressed = 4;
            else if (num5Pressed)
                numPressed = 5;

            if (Experience >= MAX_EXP)
            {
                UseExperience(numPressed);
            }
            else
            {
                UseItem(numPressed - 1);
            }



            Item item = ItemManager.GetItem(nextPos);
            Door door = GameManager.GetDoor(nextPos);
            Monster monster = MonsterManager.GetMonster(nextPos);
            Obstacle obstacle = ObstacleManager.GetObstacle(nextPos);
            Locker locker = LockerManager.GetLocker(nextPos);

            if (monster != null)
            {
                Attack(monster);
                monster.Attack(this);
            }
            else if (door != null)
            {
                Vector2 direction = nextPos - Position;
                GameManager.LoadMap(door, this, direction);
            }
            else if (item != Item.Null)
            {
                Inventory.AddItem(item);
                Console.Beep(600, 100);
            }
            else if (obstacle != null)
            {
                GameConsole.Write("It's a Wall");
            }
            else if (locker != null)
            {
                int key1 = Inventory.ItemIndex(Item.Key1);
                int key2 = Inventory.ItemIndex(Item.Key2);
                int key3 = Inventory.ItemIndex(Item.Key3);

                if(key1 != -1)
                {
                    locker.Open(Inventory[key1]);
                }
                else if(key2 != -1)
                {
                    locker.Open(Inventory[key2]);
                }
                else if (key3 != -1)
                {
                    locker.Open(Inventory[key3]);
                }
                else
                {
                    GameConsole.Write("Need A Key to open");
                }
            }
            else
            {
                Position = nextPos;
            }
        }

        public void UseExperience(int index)
        {
            if (index > 4 || index < 1)
                return;

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
            Console.Beep(300, 50);
            Console.Beep(400, 100);
        }

        public void UseItem(int index)
        {
            if (index >= 0 )
            {
                Item item = Inventory.UseItem(index);
                if (item == Item.Bomb)
                {
                    int bombRange = 2;
                    for(int i = -bombRange; i < bombRange + 1; i++)
                    {
                        for(int j = -bombRange; j < bombRange + 1; j++)
                        {
                            Monster m =MonsterManager.GetMonster(Position + new Vector2(i, j));
                            if (m != null)
                                m.ModifyHealth(-50);
                        }
                    }
                    Console.Beep(350, 50);
                    Console.Beep(250, 100);
                }
                else if (item == Item.HealthPotion)
                {
                    ModifyHealth(20);
                    Console.Beep(400, 100);
                    Console.Beep(450, 100);
                    Console.Beep(500, 100);
                }
                else if (item == Item.StrengthPotion)
                {
                    Stats = Stats.PlusStrength(2);
                    Console.Beep(400, 100);
                    Console.Beep(450, 100);
                    Console.Beep(500, 100);
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

        private void Player_OnAttack(object sender, OnAttackEventArgs e)
        {
            GameConsole.Clear();
            GameConsole.Write(string.Format("{0} atk-> {1} ({2})", e.attacker.name, e.victim.name, e.success ? (-e.damage).ToString() : "Miss"));
            
            if(e.success)
                Console.Beep(600, 100);
            else
                Console.Beep(450, 100);
        }
    }
}
