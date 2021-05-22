using System;
using System.Collections.Generic;
using System.Text;

namespace TextRPG
{
    class Inventory
    {
        private int[] slots;
        public Inventory()
        {
            slots = new int[10] { 1, 1, 1, 1, 1, 1, 1, 0, 0, 0 };
        }

        public int GetItem()
        {
            int item = -1;
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i] != 0)
                {
                    item = slots[i];
                    slots[i] = 0;
                    break;
                }
            }

            return item;
        }

        public void AddToEmptySlots(int[] items, Queue<string> actionQueue)
        {
            int j = 0;
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i] != 0)
                    continue;
                if (j >= items.Length)
                    break;
                slots[i] = items[j];
                actionQueue.Enqueue(string.Format("Added {0} to slots", GetName(items[j])));
                j++;
            }

            for (int i = j; i < items.Length; i++)
            {
                actionQueue.Enqueue(string.Format("slots is full {0} is discarded", GetName(items[j])));
            }
        }

        public string[] GetInfo()
        {
            string[] info = new string[slots.Length];
            for(int i=0; i < info.Length; i++)
            {
                info[i] = string.Format("({0}) {1}", i, GetName(slots[i]));
            }
            return info;
        }

        public string GetName(int id)
        {
            return Enum.GetName(typeof(ItemType), id);
        }

    }
}
