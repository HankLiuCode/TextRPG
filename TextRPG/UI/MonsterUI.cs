using TextRPG.Graphics;
using TextRPG.Common;

namespace TextRPG
{
    class MonsterUI
    {
        public Window window;

        public MonsterUI(Vector2 position, Vector2 rect)
        {
            window = new Window(position, rect);
            foreach(Character monster in MonsterManager.monsters)
            {
                monster.OnAttack += Character_OnAttack;
                monster.OnHealthModified += Character_OnHealthModified;
            }
        }

        private void Character_OnHealthModified(object sender, OnHealthModifiedEventArgs e)
        {
            window.Clear();
            window.Write(string.Format("Health: ({0}/{1})", e.player.Health, Character.MAX_HEALTH));
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
