using System;
using System.Collections.Generic;
using TextRPG.Utils;

namespace TextRPG
{
    public class ReviveEventArgs
    {
        public bool revive;
        public ReviveEventArgs(bool revive)
        {
            this.revive = revive;
        }
    }
    public class Monster : Character, ILootable
    {
        public const byte STUNNED_MASK = 0b_0000_0001;
        public const byte POISONED_MASK = 0b_0000_0010;

        private byte _buffStatus;
        private Loot _loot;

        public event EventHandler<ReviveEventArgs> ReviveHappened;

        public Monster(string name) : base(name)
        {
            Stats = new CharacterStats();
            Stats.GenerateRandom();
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
            bool success = _healthState == HealthState.Dead;
            if(success)
                ModifyHealth(MAX_HEALTH);

            EventHandler<ReviveEventArgs> eventHandler = ReviveHappened;
            eventHandler?.Invoke(this, new ReviveEventArgs(success));
        }
    }
}
