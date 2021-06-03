using TextRPG.Graphics;
using TextRPG.Common;

namespace TextRPG
{
    class PlayerUI
    {
        public Window options;
        public Window console;
        public Character character;

        public PlayerUI(Vector2 position, Vector2 rect, Character character)
        {
            options = new Window(position, new Vector2(rect.x, (rect.y / 2) + 1));
            console = new Window(new Vector2(position.x, rect.y / 2), new Vector2(rect.x, rect.y / 2));
            this.character = character;
            this.character.OnAttack += Character_OnAttack;
            this.character.OnHealthModified += Character_OnHealthModified;

            options.Write("(1) Player Status ");
            options.Write("(2) Inventory ");

            console.Clear();
            console.Write(string.Format("Health: ({0}/{1})", character.Health, Character.MAX_HEALTH));
        }

        private void Character_OnHealthModified(object sender, OnHealthModifiedEventArgs e)
        {
            console.Clear();
            console.Write(string.Format("Health: ({0}/{1})", character.Health , Character.MAX_HEALTH));
            if (e.amount < 0)
                console.Write(string.Format("{0} damaged dealt", -e.amount));
            else
                console.Write(string.Format("{0} amount healed", e.amount));

            console.Write(string.Format("{0}", e.healthState));
        }

        private void Character_OnAttack(object sender, OnAttackEventArgs e)
        {
            console.Clear();
            console.Write(string.Format("Health: ({0}/{1})", character.Health, Character.MAX_HEALTH));
            if(e.victim.healthState != HealthState.Dead)
            {
                console.Write(string.Format("{0} attacked {1}", e.attacker.name, e.victim.name));
                console.Write(string.Format("{0}", e.success ? "Attack Success" : "Missed"));
            }
            else
            {
                console.Write("Already Dead");
            }
        }

        public Window GetWindow()
        {
            return console;
        }

        public Window[] GetWindows()
        {
            return new Window[2] { options, console };
        }
    }
}
