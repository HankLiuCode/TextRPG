using TextRPG.Graphics;
using System.Collections.Generic;

namespace TextRPG
{
    class MonsterUI
    {
        public Window window;

        public MonsterUI(Vector2 position, Vector2 rect)
        {
            window = new Window(position, rect);
            List<Monster> monsters = GameManager.gameEntityManager.Find<Monster>();
            foreach (Monster monster in monsters)
            {
                monster.OnHealthModified += Character_OnHealthModified;
            }
            GameManager.gameEntityManager.OnLoad += GameEntityManager_OnLoad;
        }

        private void GameEntityManager_OnLoad(object sender, System.EventArgs e)
        {
            List<Monster> monsters = GameManager.gameEntityManager.Find<Monster>();
            foreach (Character monster in monsters)
            {
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

        public Window GetWindow()
        {
            return window;
        }
    }
}
