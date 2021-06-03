using System;
using System.Collections.Generic;
using System.Text;
using TextRPG.Common;

namespace TextRPG
{
    public class Player : GameEntity
    {
        public float Health { get; private set; }
        public Player(string name, char symbol, Vector2 position) : base(name, symbol, position)
        {
            Health = 100f;
        }

        public void TakeDamage(float damage)
        {
            Health -= damage;
            if (Health <= 0)
                Destroy(this);
        }
    }
}
