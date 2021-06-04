using System;
using System.Collections.Generic;
using System.Text;

namespace TextRPG
{
    public enum Item
    {
        Null,
        Bomb,
        HealthPotion,
        StrengthPotion
    }
    class Inventory
    {
        private List<Item> slots;
        public int Capacity { get; private set; }
        public int Count { get { return slots.Count; } }
        public Item this[int index] { get { return slots[index]; } }

        public event EventHandler OnItemChanged;
        public event EventHandler OnInventoryFull;

        public Inventory(int capacity) 
        {
            Capacity = capacity;
            slots = new List<Item>(Capacity); 
        }
        public Inventory(int capacity, Item[] items)
        {
            Capacity = capacity;
            slots = new List<Item>(Capacity);
            foreach (Item item in items)
            {
                slots.Add(item);
            }
        }
        public Item UseItem(int index)
        {
            if(index < slots.Count)
            {
                Item item = slots[index];
                slots.Remove(slots[index]);
                OnItemChanged?.Invoke(this, EventArgs.Empty);
                return item;
            }
            return Item.Null;
        }
        public void AddItem(Item item)
        {
            if (slots.Count < Capacity)
            {
                slots.Add(item);
                OnItemChanged?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                OnInventoryFull?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
