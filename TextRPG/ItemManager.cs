using System;
using System.Collections.Generic;
using System.Text;

namespace TextRPG
{
    static class ItemManager
    {
        static Dictionary<Vector2, ItemEntity> items = new Dictionary<Vector2, ItemEntity>();
        static Dictionary<char, Item> itemMapping = new Dictionary<char, Item>();

        static ItemManager()
        {
            itemMapping.Add('!', Item.HealthPotion);
            itemMapping.Add('i', Item.StrengthPotion);
            itemMapping.Add('O', Item.Bomb);
            itemMapping.Add('{', Item.Key1);
            itemMapping.Add('[', Item.Key2);
            itemMapping.Add('(', Item.Key3);
        }

        public static void LoadItems(Map map)
        {
            for(int i=0; i<map.Width; i++)
            {
                for(int j=0; j < map.Height; j++)
                {
                    if (itemMapping.ContainsKey(map[i, j]))
                    {
                        Item item = itemMapping[map[i, j]];
                        ItemEntity itemEntity = new ItemEntity(item.ToString(), map[i, j], item, new Vector2(i, j));
                        items.Add(itemEntity.Position, itemEntity);
                        MapController.Bind(itemEntity, map);
                    }
                }
            }
        }

        public static void UnloadItems()
        {
            foreach(GameEntity itemEntity in items.Values)
            {
                MapController.UnBind(itemEntity);
            }
            items.Clear();
        }

        public static Item GetItem(Vector2 position)
        {
            if (items.ContainsKey(position))
            {
                ItemEntity itemEntity = items[position];
                Item item = itemEntity.item;
                itemEntity.Destroy();

                items.Remove(position);
                return item;
            }
            return Item.Null;
        }
        
    }
}
