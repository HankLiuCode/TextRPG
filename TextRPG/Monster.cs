using TextRPG.Common;

namespace TextRPG
{
    public class Monster : Character, ILootable
    {
        public const byte STUNNED_MASK = 0b_0000_0001;
        public const byte POISONED_MASK = 0b_0000_0010;
        private byte _buffStatus;
        private Loot _loot;

        public Monster(string name, char symbol, Vector2 position, Stats stats) : base(name,symbol,position,stats)
        {

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
    }
}
