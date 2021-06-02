using System;
using TextRPG.Graphics;

namespace TextRPG.GUI
{
    class CombatMenu : BadWindow
    {
        private Player _player;

        public CombatMenu(int width, int height, Player player) : base(width, height)
        {
            _player = player;
        }

        public void AddOptionsToBuffer()
        {
            AddToOutputBuffer("(0)Back to Menu");
            AddNewLine();
            AddToOutputBuffer(string.Format("Current Monster:  {0}", MonsterManager.CurrentMonster.Name));
            AddToOutputBuffer("(1)Attack  (2)Bomb Attack");
            AddNewLine();
            AddBorder();
        }

        public override void Show()
        {
            ConsoleKeyInfo keyInfo;

            AddOptionsToBuffer();
            Render(0, 0);

            while (true)
            {
                keyInfo = Console.ReadKey(_hidePressedKey);
                if (keyInfo.Key == ConsoleKey.D0)
                {
                    break;
                }

                AddOptionsToBuffer();

                if (keyInfo.Key == ConsoleKey.D1)
                {
                    //_player.Attack(MonsterManager.CurrentMonster);
                    //AddToOutputBuffer(_player.GetActions(true));
                    //AddToOutputBuffer(MonsterManager.CurrentMonster.GetActions(true));
                    //MonsterManager.CurrentMonster.Attack(_player);
                    //AddToOutputBuffer(MonsterManager.CurrentMonster.GetActions(true));
                    //AddToOutputBuffer(_player.GetActions(true));
                }
                else if (keyInfo.Key == ConsoleKey.D2)
                {
                    //_player.BombAttack(MonsterManager.CurrentMonster);
                    //AddToOutputBuffer(_player.GetActions(true));
                    //AddToOutputBuffer(MonsterManager.CurrentMonster.GetActions(true));
                    //MonsterManager.CurrentMonster.Attack(_player);
                    //AddToOutputBuffer(MonsterManager.CurrentMonster.GetActions(true));
                    //AddToOutputBuffer(_player.GetActions(true));
                }
                else if (keyInfo.Key == ConsoleKey.D0)
                {
                    break;
                }
                else
                {
                    AddToOutputBuffer("Not a valid input.");
                }

                Render(0, 0);
            }
        }
    }
}
