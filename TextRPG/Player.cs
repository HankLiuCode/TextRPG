using System.Collections.Generic;
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

        private Inventory inventory;
        
        public Queue<string> ActionQueue;

        public Player()
        {
            random = new RPGRandom();
            strength = random.NextFloat(STRENGTH_MIN, STRENGTH_MAX);
            dexterity = random.NextFloat(DEX_MIN, DEX_MAX);
            accuracy = random.NextFloat(ACCURACY_MIN, ACCURACY_MAX);
            inventory = new Inventory();
            ActionQueue = new Queue<string>();
            experience = 0f;
        }

      
        

        public void Attack(Monster monster)
        {
            if (monster.Status == HealthStatus.Dead)
            {
                ActionQueue.Enqueue(string.Format("{0} is already dead", monster.Name));
                return;
            }

            if (!monster.AttackSuccessful(dexterity, accuracy))
            {
                ActionQueue.Enqueue("Attack Unsuccessful");
                return;
            }
            else
            {
                ActionQueue.Enqueue("Attack Successful!");
            }

            monster.TakeDamage(strength);

            if (monster.Status == HealthStatus.Dead)
            {
                experience += monster.TakeExperience();
                int[] items = monster.Loot();
                inventory.AddToEmptySlots(items, ActionQueue);

                ActionQueue.Enqueue(string.Format("Player Experience: " + experience));
            }


        }

        public void BombAttack(Monster monster)
        {
            if (inventory.GetItem() == -1)
            {
                ActionQueue.Enqueue(string.Format("Inventory is Empty"));
                return;
            }


            if (monster.Status == HealthStatus.Dead)
            {
                ActionQueue.Enqueue(string.Format("{0} is already dead", monster.Name));
                return;
            }

            monster.TakeDamage(BOMB_DAMAGE);
            if (monster.Status == HealthStatus.Dead)
            {
                experience += monster.TakeExperience();
                int[] items = monster.Loot();
                inventory.AddToEmptySlots(items, ActionQueue);
                ActionQueue.Enqueue(string.Format("Player Experience: " + experience));
            }
        }

        public string[] GetInfo()
        {
            string[] info = new string[5];
            info[0] = "Status:";
            info[1] = string.Format("   Strength:{0,-10}", strength);
            info[2] = string.Format("   Dexterity:{0,-10}", dexterity);
            info[3] = string.Format("   Accuracy:{0,-10}", accuracy);
            info[4] = string.Format("   Exp: {0,-10}", experience);
            return info;
        }

        public string[] GetInventoryInfo()
        {
            return inventory.GetInfo();
        }
    }
}
