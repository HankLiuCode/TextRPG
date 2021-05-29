using System;
using System.Collections.Generic;
using System.Text;

namespace TextRPG
{
    public class Loot
    {
        public static Loot Empty { 
            get {
                return new Loot(0, new Item[0]);
            }
        }
        public float Experience { get; private set; }
        public Item[] Items { get; private set; }

        private Loot()
        {

        }

        public Loot(float experience, Item[] items)
        {
            Experience = experience;
            Items = items;
            if (items == null)
                items = new Item[0];
        }

        public void Clear()
        {
            Experience = 0;
            Items = new Item[0];
        }
    }
}
