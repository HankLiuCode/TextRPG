using System;
using System.Collections.Generic;
using System.Text;

namespace TextRPG
{
    static class ItemManager
    {
        static Dictionary<Vector2, Item> items = new Dictionary<Vector2, Item>();
        static Dictionary<char, Item> itemMapping = new Dictionary<char, Item>();

        static ItemManager()
        {
            itemMapping.Add('!', Item.HealthPotion);
            itemMapping.Add('i', Item.StrengthPotion);
            itemMapping.Add('O', Item.Bomb);
        }

        public static void LoadItems(Map map)
        {
            for(int i=0; i<map.Width; i++)
            {
                for(int j=0; j < map.Height; j++)
                {
                    if (itemMapping.ContainsKey(map[i, j]))
                    {
                        items.Add(new Vector2(i, j), itemMapping[map[i, j]]);
                    }
                }
            }
        }

        public static void UnloadItems()
        {
            items.Clear();
        }

        public static Item GetItem(Vector2 position)
        {
            if (items.ContainsKey(position))
            {
                return items[position];
            }
            return Item.Null;
        }
        
    }
}
