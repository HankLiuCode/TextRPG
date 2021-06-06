using TextRPG.Utils;


namespace TextRPG
{
    public struct Reward
    {
        public float exp;
        public Item[] items;

        public Reward(float exp, int itemCount)
        {
            this.exp = exp;
            items = new Item[itemCount];
            for(int i=0; i < itemCount; i++)
            {
                items[i] = RPGRandom.ChooseFrom(new Item[] { Item.Bomb, Item.HealthPotion, Item.StrengthPotion });
            }
        }
        public void Clear()
        {
            this.exp = 0;
            items = new Item[0];
        }
    }

    public class Monster : Character
    {
        public const byte STUNNED_MASK = 0b_0000_0001;
        public const byte POISONED_MASK = 0b_0000_0010;
        private byte _buffStatus;

        private Reward _reward;
        public Reward Reward 
        { 
            get 
            {
                Reward temp = _reward;
                _reward.Clear();
                return temp;
            }
            set
            {
                _reward = value;
            }
        }

        public Monster(string name, char symbol, Vector2 position, Stats stats) : base(name,symbol, position, stats)
        {
            Reward = new Reward(100, 2);
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
    }
}
