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
            inventory.InventoryFull += Inventory_InventoryFull;
            _stats = new Stats();
            _stats.GenerateRandom();
            _experience = 0f;
        }

        private void Inventory_InventoryFull(Item item)
        {
            Actions.Add(string.Format("Inventory Full, {0} is discarded", item.ItemType));
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

        public void BombAttack(Character character)
        {
            if (IsDead)
            {
                Actions.Add(string.Format("{0} is dead, cannot Attack", Name));
                return;
            }

            if (character.IsDead)
            {
                Actions.Add(string.Format("{0} is already dead", character.Name));
                return;
            }

            if (inventory.Get(Item.Type.Bomb) == null)
            {
                Actions.Add("No Bomb in Inventory");
                return;
            }

            character.TakeDamage(BOMB_DAMAGE);
            Actions.Add(string.Format("{0} Attack {1}",Name, character.Name));
            if (character.IsDead && character is ILootable)
            {
                ((ILootable)character).GetLoot();
            }
        }

        public string[] InventorySummary()
        {
            return inventory.Summary();
        }
    }
}
