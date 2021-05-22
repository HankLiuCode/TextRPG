using System;
using TextRPG.Utils;


namespace TextRPG
{
    public class Player
    {
        private RPGRandom random;

        private const float STRENGTH_MAX = 20f;
        private const float STRENGTH_MIN = 16f;
        private const float DEX_MAX = 10f;
        private const float DEX_MIN = 8f;
        private const float ACCURACY_MAX = 0.5f;
        private const float ACCURACY_MIN = 0f;

        private const float BOMB_DAMAGE = 30f;

        private float strength;
        private float dexterity;
        private float accuracy;
        private float experience;

        private int[] bag;

        public Player()
        {
            random = new RPGRandom();
            strength = random.NextFloat(STRENGTH_MIN, STRENGTH_MAX);
            dexterity = random.NextFloat(DEX_MIN, DEX_MAX);
            accuracy = random.NextFloat(ACCURACY_MIN, ACCURACY_MAX);

            bag = new int[10] {1,1,1,1,1,1,1,1,1,1};
            experience = 0f;
        }

        private int GetItem()
        {
            int item = -1;
            for(int i=0; i<bag.Length; i++)
            {
                if (bag[i] != 0)
                {
                    item = bag[i];
                    bag[i] = 0;
                    break;
                }
            }

            return item;
        }

        private void AddToEmptySlots(int[] items)
        {
            int j = 0;
            for(int i=0; i < bag.Length; i++)
            {
                if (bag[i] != 0)
                    continue;
                if (j >= items.Length)
                    break;
                bag[i] = items[j];
                j++;
            }

            for(int i=j; i<items.Length; i++)
            {
                Console.WriteLine("Bag is full {0} is discarded", items[j]);
            }

        }

        

        public void Attack(Monster monster)
        {
            if (monster.Status == HealthStatus.Dead)
            {
                Console.WriteLine("{0} is already dead", monster.Name);
                return;
            }

            if (monster.AttackSuccessful(dexterity, accuracy))
            {
                monster.TakeDamage(strength);
                Console.WriteLine("Attack Successful!");
                if (monster.Status == HealthStatus.Dead)
                {
                    experience += monster.TakeExperience();
                    int[] items = monster.Loot();
                    AddToEmptySlots(items);
                    Console.WriteLine("Player Experience: " + experience);
                }
            }
            else
                Console.WriteLine("Attack Unsuccessful");
        }

        public void BombAttack(Monster monster)
        {
            if (GetItem() == -1)
            {
                Console.WriteLine("Inventory is Empty");
                return;
            }


            if (monster.Status == HealthStatus.Dead)
            {
                Console.WriteLine("{0} is already dead", monster.Name);
                return;
            }

            monster.TakeDamage(BOMB_DAMAGE);
            if (monster.Status == HealthStatus.Dead)
            {
                experience += monster.TakeExperience();
                int[] items = monster.Loot();
                AddToEmptySlots(items);
                Console.WriteLine("Player Experience: " + experience);
            }
        }

        public void GetInfo()
        {
            string[] info = new string[5];
            info[0] = "Player Status:";
            info[1] = string.Format("Strength:{0,-10}", strength);
            info[2] = string.Format("Dexterity:{0,-10}", dexterity);
            info[3] = string.Format("Accuracy:{0,-10}", accuracy);
            info[4] = string.Format("Exp: {0,-10}", experience);
        }

        public void PrintStatus()
        {
            Console.WriteLine("Player Status:");
            Console.WriteLine("Strength:{0,-10} Dexterity:{1,-10} Accuracy:{2,-10} Exp: {3,-10}", strength, dexterity, accuracy, experience);

            string bagContent = "[";
            for (int i = 0; i < bag.Length; i++)
            {
                bagContent += bag[i];
                if (i != bag.Length - 1)
                    bagContent += ",";
            }
            bagContent += "]";
            Console.WriteLine(bagContent);
        }


    }
}
