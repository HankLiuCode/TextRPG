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
        private const float DEX_MIN = 7f;
        private const float ACCURACY_MAX = 1f;
        private const float ACCURACY_MIN = 0f;


        private float strength;
        private float dexterity;
        private float accuracy;
        private float experience;

        public Player()
        {
            random = new RPGRandom();
            strength = random.NextFloat(STRENGTH_MIN, STRENGTH_MAX);
            dexterity = random.NextFloat(DEX_MIN, DEX_MAX);
            accuracy = random.NextFloat(ACCURACY_MIN, ACCURACY_MAX);

            experience = 0f;
        }

        public void Attack(LivingEntity livingEntity)
        {
            if(livingEntity.AttackSuccessful(dexterity, accuracy))
            {
                Console.WriteLine("Attack Successful!");
                livingEntity.TakeDamage(strength);
                if (livingEntity.IsDead)
                {
                    experience += livingEntity.TakeExperience();
                    Console.WriteLine("Player Experience: " + experience);
                }
            }
            else
            {
                Console.WriteLine("Attack Unsuccessful");
            }
        }

        public void PrintStatus()
        {
            Console.WriteLine("Player Status:");
            Console.WriteLine("Strength:{0,-10} Dexterity:{1,-10} Accuracy:{2,-10} Exp: {3,-10}", strength, dexterity, accuracy, experience );
        }


    }
}
