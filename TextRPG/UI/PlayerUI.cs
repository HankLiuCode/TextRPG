using TextRPG.Graphics;

namespace TextRPG
{
    class PlayerUI
    {
        public Window playerStatus;
        public Window inventoryUI;

        public Player player;

        public PlayerUI(Vector2 position, Vector2 rect, Player player)
        {
            playerStatus = new Window(position, new Vector2(rect.x, rect.y / 2));
            inventoryUI = new Window(new Vector2(position.x, rect.y / 2), new Vector2(rect.x, rect.y / 2));

            this.player = player;
            this.player.OnHealthModified += Player_OnHealthModified;
            this.player.OnStatsModified += Player_OnStatsModified;
            this.player.Inventory.OnItemChanged += Inventory_OnItemChanged;
            this.player.Inventory.OnInventoryFull += Inventory_OnInventoryFull;
            this.player.OnExpModified += Player_OnExpModified;

            ShowPlayerStatus();
            ShowInventory();
        }

        public void ShowInventory()
        {
            inventoryUI.Clear();
            for(int i=0; i < player.Inventory.Capacity; i++)
            {
                if(i < player.Inventory.Count)
                {
                    inventoryUI.Write(string.Format("({0}) {1}", i+1, player.Inventory[i]));
                }
                else
                {
                    inventoryUI.Write(string.Format("({0})", i+1));
                }
            }
        }

        public void ShowPlayerStatus()
        {
            playerStatus.Clear();
            playerStatus.Write(string.Format("{0} Health: ({1}/{2})    Exp: ({3}/{4})", 
                    player.name, 
                    player.Health.ToString("0"), 
                    Player.MAX_HEALTH, 
                    player.Experience.ToString("0"),
                    Player.MAX_EXP
                ));
            
            bool canLevelUp = player.Experience >= Player.MAX_EXP;
            playerStatus.Write(string.Format("{0} Strength:   {1}", canLevelUp ? "Press (1) Levelup" : "", player.Stats.strength));
            playerStatus.Write(string.Format("{0} ArmorClass: {1}", canLevelUp ? "Press (2) Levelup" : "", player.Stats.armorClass));
            playerStatus.Write(string.Format("{0} Dexerity:   {1}", canLevelUp ? "Press (3) Levelup" : "", player.Stats.dexerity));
            playerStatus.Write(string.Format("{0} Accuracy:   {1}", canLevelUp ? "Press (4) Levelup" : "", player.Stats.accuracy));

        }

        private void Player_OnExpModified(object sender, OnExpModifiedEventArgs e)
        {
            ShowPlayerStatus();
            bool canLevelUp = player.Experience >= Player.MAX_EXP;
            if (canLevelUp)
            {
                inventoryUI.Hide();
            }
            else
            {
                inventoryUI.Show();
            }
        }

        private void Player_OnHealthModified(object sender, OnHealthModifiedEventArgs e)
        {
            ShowPlayerStatus();
        }

        private void Player_OnStatsModified(object sender, System.EventArgs e)
        {
            ShowPlayerStatus();
        }

        private void Inventory_OnInventoryFull(object sender, System.EventArgs e)
        {
            ShowInventory();
        }

        private void Inventory_OnItemChanged(object sender, System.EventArgs e)
        {
            ShowInventory();
        }

        public Window[] GetWindows()
        {
            return new Window[2] { playerStatus, inventoryUI};
        }
    }
}
