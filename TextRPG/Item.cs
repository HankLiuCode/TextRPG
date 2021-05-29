using System;
using System.Collections.Generic;
using System.Text;

namespace TextRPG
{
    public class Item
    {
        public enum Type
        {
            Bomb,
            HealthPotion,
            StrengthPotion
        }

        public Type ItemType { get; private set; }

        public Item(Type type)
        {
            ItemType = type;
        }

    }
}
