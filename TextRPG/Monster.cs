using System;
using System.Collections.Generic;
using TextRPG.Utils;

namespace TextRPG
{
    public class Monster : Character, ILootable
    {
        private const float ARMOR_CLASS = 10f;
        private const float STRENGTH = 16f;
        private const float DEXERITY = 10f;
        private const float ACCURACY = 0.5f;

        public const byte STUNNED_MASK = 0b_0000_0001;
        public const byte POISONED_MASK = 0b_0000_0010;

        private byte _buffStatus;
        private Loot _loot;

        public Monster(string name) : base(name)
        {
            _stats = new Stats();
            _stats.GenerateRandom();
            _loot = Loot.Empty;
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

        public Loot GetLoot()
        {
            return _loot;
        }

        public void Revive()
        {
            if(_healthState == HealthState.Dead)
            {
                _healthState = HealthState.FullHealth;
                _healthPoints = MAX_HEALTH;
                Actions.Add(string.Format("{0} is revived", Name));
            }
            else
            {
                Actions.Add(string.Format("Cannot Revive monster that is still alive"));
            }
        }
    }
}
