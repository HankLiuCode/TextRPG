using TextRPG.Graphics;

namespace TextRPG
{
    class MonsterUI
    {
        public Window window;

        public MonsterUI(Vector2 position, Vector2 rect)
        {
            window = new Window(position, rect);
            foreach (Character monster in MonsterManager.monsters)
            {
                monster.OnAttack += Character_OnAttack;
                monster.OnHealthModified += Character_OnHealthModified;
            }
            MonsterManager.OnReload += MonsterManager_OnReload;
        }



        private void MonsterManager_OnReload(object sender, System.EventArgs e)
        {
            foreach (Character monster in MonsterManager.monsters)
            {
                monster.OnAttack += Character_OnAttack;
                monster.OnHealthModified += Character_OnHealthModified;
            }
        }

        public void ShowMonsterStats(Character character)
        {
            window.Clear();
            window.Write(string.Format("{0} Health: ({1}/{2})", character.name, character.Health.ToString("0"), Character.MAX_HEALTH));
            window.Write(string.Format("Strength:   {0}", character.Stats.strength));
            window.Write(string.Format("ArmorClass: {0}", character.Stats.armorClass));
            window.Write(string.Format("Dexerity:   {0}", character.Stats.dexerity));
            window.Write(string.Format("Accuracy:   {0}", character.Stats.accuracy));
        }

        private void Character_OnHealthModified(object sender, OnHealthModifiedEventArgs e)
        {
            ShowMonsterStats(e.character);
            if (e.healthState == HealthState.Dead)
                window.Clear();
        }

        private void Character_OnAttack(object sender, OnAttackEventArgs e)
        {
            
        }

        public Window GetWindow()
        {
            return window;
        }
    }
}
