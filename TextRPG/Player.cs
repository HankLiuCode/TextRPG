using System;
using System.Collections.Generic;
using TextRPG.Utils;


namespace TextRPG
{
    public class Player : Character
    {
        private const float BOMB_DAMAGE = 30f;
        private float _experience;
        private Inventory inventory;

        public Player(string name) : base(name)
        {
            inventory = new Inventory();
            Stats = new CharacterStats();
            Stats.GenerateRandom();
            _experience = 0f;
        }

        public override void Attack(Character character)
        {
            base.Attack(character);
        }

        public void Loot(Monster monster)
        {
            Loot loot = monster.GetLoot();
            AddExperience(loot.Experience);
            AddLootToInventory(loot.Items);

        }

        public void AddLootToInventory(Item[] items)
        {
            for(int i = 0; i < items.Length; i++)
            {
                inventory.Add(items[i]);
            }
        }

        public void AddExperience(float exp)
        {
            _experience += exp;
        }

        public void BombAttack(Character victim)
        {
            bool success = inventory.Get(Item.Type.Bomb) != null;
            float damage = 30;
            victim.ModifyHealth(-damage);

            InvokeAttackHappened(this, new AttackEventArgs(damage, success, this, victim, AttackType.BombAttack));
        }

        public string[] InventorySummary()
        {
            return inventory.Summary();
        }
    }
}
