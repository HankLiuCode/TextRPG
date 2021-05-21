using System;
using TextRPG.Utils;

namespace TextRPG
{
    public class Monster
    {
        private const float ATTACKER_STRENGTH_MULTIPLIER = 1.4f;
        private const float ARMOR_CLASS_MULTIPLIER = 2f;
        private const float ATTACK_SUCCESSFUL_THRESHOLD = 1.5f;

        private const float HEALTH_POINT_MAX = 100f;
        private const float DEX_MAX = 10f;
        private const float DEX_MIN = 1f;
        private const float ARMOR_CLASS_MAX = 10f;
        private const float ARMOR_CLASS_MIN = 1f;
        private const float EXP_MAX = 20f;
        private const float EXP_MIN = 10f;

        public const byte STUNNED_MASK = 0b_0000_0001;
        public const byte POISONED_MASK = 0b_0000_0010;

        public readonly string[] MONSTER_NAMES = {"Bob The Monster", "Ripper Jack", "Deadly Robin", "A Goblem", "Vampire"};

        public HealthStatus Status { get; private set; }
        private byte buffStatus;

        private RPGRandom random;
        private float healthPoints;
        private float dexterity;
        private float armorClass;
        private float experience;
        private bool experienceTaken;

        private int[] loot = new int[] { 1, 1, 1 };

        public string Name { get; private set; }
        public event EventHandler Died;

        public Monster()
        {
            random = new RPGRandom();
            healthPoints = HEALTH_POINT_MAX;
            dexterity = random.NextFloat(DEX_MIN, DEX_MAX);
            armorClass = random.NextFloat(ARMOR_CLASS_MIN, ARMOR_CLASS_MAX);
            experience = random.NextFloat(EXP_MIN, EXP_MAX);
            Name = random.PickFrom(MONSTER_NAMES);
            Status = HealthStatus.FullHealth;
        }

        public bool CheckStatus(byte mask)
        {
            return (mask & buffStatus) == mask;
        }

        public void RemoveStatus(byte mask)
        {
            byte m = mask;
            // cannot use ~ on const
            buffStatus &= (byte)~m;
        }

        public void SetStatus(byte mask)
        {
            buffStatus |= mask;
        }

        public bool AttackSuccessful(float attackerDexterity, float attackerAccuracy)
        {
            return (attackerDexterity / dexterity + attackerAccuracy) > ATTACK_SUCCESSFUL_THRESHOLD;
        }

        public void TakeDamage(float strength)
        {
            if (Status == HealthStatus.Dead)
                return;
            // sometimes the damage will be negative 
            float damage = MathF.Max(strength * ATTACKER_STRENGTH_MULTIPLIER - armorClass * ARMOR_CLASS_MULTIPLIER, 1f);
            healthPoints -= damage;

            Status = HealthStatus.HalfHealth;
            if (healthPoints <= 0)
                Die();

            Console.WriteLine("{0} Took: {1} Damage", Name, damage);
            Console.WriteLine("Health: ({0}/{1}) ", healthPoints, HEALTH_POINT_MAX);
        }

        public float TakeExperience()
        {
            if (experienceTaken || Status != HealthStatus.Dead)
                return 0;

            experienceTaken = true;
            return experience;
        }

        public int[] Loot()
        {
            return loot;
        }

        public void Revive()
        {
            if(Status == HealthStatus.Dead)
            {
                healthPoints = HEALTH_POINT_MAX;
                Status = HealthStatus.FullHealth;
                Console.WriteLine("{0} is revived!", Name);
            }
        }

        private void OnDied()
        {
            EventHandler handler = Died;
            handler?.Invoke(this, EventArgs.Empty);
        }

        public void Die()
        {
            Status = HealthStatus.Dead;
            healthPoints = 0;
            OnDied();
        }

        public void PrintStatus()
        {
            Console.WriteLine("{0} Status:", Name);
            Console.WriteLine("HealthPoints:{0,-10} Dexterity:{1,-10} ArmorClass:{2,-10} Exp:{3,-10}", healthPoints, dexterity, armorClass, experience);
            if (Status == HealthStatus.Dead)
                Console.WriteLine("DEAD");

            else if(Status == HealthStatus.FullHealth)
                Console.WriteLine("FULL HEALTH");

            else if (Status == HealthStatus.HalfHealth)
                Console.WriteLine("HALF HEALTH");
        }
    }
}
