using System;
using TextRPG.Utils;

namespace TextRPG
{
    public class Monster : LivingEntity
    {
        private const float ATTACKER_STRENGTH_MULTIPLIER = 1.4f;
        private const float ARMOR_CLASS_MULTIPLIER = 2f;
        private const float ATTACK_SUCCESSFUL_THRESHOLD = 1.5f;

        private const float INITIAL_HEALTH_POINT = 100f;
        private const float DEX_MAX = 10f;
        private const float DEX_MIN = 1f;
        private const float ARMOR_CLASS_MAX = 10f;
        private const float ARMOR_CLASS_MIN = 1f;
        private const float EXP_MAX = 20f;
        private const float EXP_MIN = 10f;

        private RPGRandom random;

        private float healthPoints;
        public float dexterity;
        public float armorClass;
        public float experience;

        private bool experienceTaken;
        public event EventHandler Died;

        public Monster()
        {
            random = new RPGRandom();
            healthPoints = INITIAL_HEALTH_POINT;
            dexterity = random.NextFloat(DEX_MIN, DEX_MAX);
            armorClass = random.NextFloat(ARMOR_CLASS_MIN, ARMOR_CLASS_MAX);
            experience = random.NextFloat(EXP_MIN, EXP_MAX);
        }

        public override bool AttackSuccessful(float attackerDexterity, float attackerAccuracy)
        {
            return (attackerDexterity / dexterity + attackerAccuracy) > ATTACK_SUCCESSFUL_THRESHOLD;
        }

        public override void TakeDamage(float strength)
        {
            if (IsDead) return;
            // sometimes the damage will be negative 
            float damage = MathF.Max(strength * ATTACKER_STRENGTH_MULTIPLIER - armorClass * ARMOR_CLASS_MULTIPLIER, 1f);
            healthPoints -= damage;
            if (healthPoints <= 0)
            {
                Die();
            }
            Console.WriteLine("Monster Took: {0} Damage", damage);
            Console.WriteLine("Health: ({0}/{1}) ", healthPoints, INITIAL_HEALTH_POINT);
        }

        public override float TakeExperience()
        {
            if (experienceTaken || !IsDead)
                return 0;

            experienceTaken = true;
            return experience;
        }

        private void OnDied()
        {
            EventHandler handler = Died;
            handler?.Invoke(this, EventArgs.Empty);
        }

        public void Die()
        {
            IsDead = true;
            healthPoints = 0;
            OnDied();
        }

        public override void PrintStatus()
        {
            Console.WriteLine("Monster Status:");
            Console.WriteLine("HealthPoints:{0,-10} Dexterity:{1,-10} ArmorClass:{2,-10} Exp:{3,-10}", healthPoints, dexterity, armorClass, experience);
        }
    }
}
