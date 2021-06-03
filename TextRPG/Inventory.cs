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

    class Inventory
    {
        private int _maxSlots = 10;
        private List<Item> slots;
        public event Action<Item> InventoryFull;

        public Inventory()
        {
            slots = new List<Item>(_maxSlots);
        }

        public void Add(Item item)
        {
            if (slots.Count >= _maxSlots)
            {
                InventoryFull.Invoke(item);
                return;
            }
            slots.Add(item);

        }

        public void Remove(Item item)
        {
            slots.Remove(item);
        }

        public Item Get(Item.Type itemType)
        {
            Item item = slots.Find((Item item) => item.ItemType == itemType);
            return item;
        }

        public string[] Summary()
        {
            string[] info = new string[_maxSlots];
            for(int i=0; i < _maxSlots; i++)
            {
                if(i < slots.Count && slots[i] != null)
                {
                    info[i] = string.Format("({0}) {1}", i, slots[i].ItemType);
                }
                else
                {
                    info[i] = string.Format("({0}) {1}", i, "");
                }
            }
            return info;
        }
    }
}
