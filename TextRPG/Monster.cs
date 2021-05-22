using System;
using System.Collections.Generic;
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

        public readonly string[] MONSTER_NAMES = {"Zombie", "Golem", "Goblin", "Werewolf", "Vampire"};

        public HealthStatus Status { get; private set; }
        private byte _buffStatus;

        private RPGRandom _random;
        private float _healthPoints;
        private float _dexterity;
        private float _armorClass;
        private float _experience;
        private bool _experienceTaken;

        private int[] _loot = new int[] { 1, 1, 1 };

        public string Name { get; private set; }
        public Queue<string> ActionQueue { get; private set; }
        public event EventHandler Died;

        public Monster(string name)
        {
            _random = new RPGRandom();
            _healthPoints = HEALTH_POINT_MAX;
            _dexterity = _random.NextFloat(DEX_MIN, DEX_MAX);
            _armorClass = _random.NextFloat(ARMOR_CLASS_MIN, ARMOR_CLASS_MAX);
            _experience = _random.NextFloat(EXP_MIN, EXP_MAX);
            Name = name; //_random.PickFrom(MONSTER_NAMES);
            Status = HealthStatus.FullHealth;
            ActionQueue = new Queue<string>();
        }

        public bool CheckStatus(byte mask)
        {
            return (mask & _buffStatus) == mask;
        }

        public void RemoveStatus(byte mask)
        {
            byte m = mask;
            // cannot use ~ on const
            _buffStatus &= (byte)~m;
        }

        public void SetStatus(byte mask)
        {
            _buffStatus |= mask;
        }

        public bool AttackSuccessful(float attackerDexterity, float attackerAccuracy)
        {
            return (attackerDexterity / _dexterity + attackerAccuracy) > ATTACK_SUCCESSFUL_THRESHOLD;
        }

        public void TakeDamage(float strength)
        {
            if (Status == HealthStatus.Dead)
                return;
            // sometimes the damage will be negative 
            float damage = MathF.Max(strength * ATTACKER_STRENGTH_MULTIPLIER - _armorClass * ARMOR_CLASS_MULTIPLIER, 1f);
            _healthPoints -= damage;
            Status = HealthStatus.HalfHealth;
            ActionQueue.Enqueue(string.Format("{0} Took: {1} Damage", Name, damage));

            if (_healthPoints <= 0)
                Die();

            ActionQueue.Enqueue(string.Format("Health: ({0}/{1}) ", _healthPoints, HEALTH_POINT_MAX));
        }

        public float TakeExperience()
        {
            if (_experienceTaken || Status != HealthStatus.Dead)
                return 0;

            _experienceTaken = true;
            return _experience;
        }

        public int[] Loot()
        {
            if(_random.NextFloat(0f, 1f) > 0.5f)
                return _loot;
            return new int[0];
        }

        public void Revive()
        {
            if(Status == HealthStatus.Dead)
            {
                _healthPoints = HEALTH_POINT_MAX;
                Status = HealthStatus.FullHealth;
                ActionQueue.Enqueue(string.Format("{0} is revived!", Name));
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
            _healthPoints = 0;
            ActionQueue.Enqueue(string.Format("{0} is Dead", Name));
            OnDied();
        }

        public string[] GetInfo()
        {
            string[] info = new string[6];
            string hStatus = "";
            if (Status == HealthStatus.Dead)
                hStatus = "DEAD";

            else if (Status == HealthStatus.FullHealth)
                hStatus = "FULL HEALTH";

            else if (Status == HealthStatus.HalfHealth)
                hStatus = "HALF HEALTH";

            info[0] = string.Format("Name: {0}", Name);
            info[1] = string.Format("   -HealthPoints:({0}/{1})", _healthPoints, HEALTH_POINT_MAX);
            info[2] = string.Format("   -Dexterity:{0,-10}", _dexterity);
            info[3] = string.Format("   -ArmorClass:{0,-10}", _dexterity);
            info[4] = string.Format("   -Exp:{0,-10}", _experience);
            info[5] = hStatus;
            return info;
        }
    }
}
