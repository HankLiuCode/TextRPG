using System;
using System.Collections.Generic;
using System.Text;

namespace TextRPG 
{ 
    public class ItemEntity : GameEntity
    {
        public Item item;
        public ItemEntity(string name, char symbol, Item item, Vector2 position) : base(name, symbol, position)
        {
            this.item = item;
        }
    }
}
