using System;
using TextRPG.Graphics;

namespace TextRPG.GUI
{
    class CombatMenu : Window
    {
        private Player _player;
        private MonsterManager _monsterManager;

        public CombatMenu(int width, int height, Player player, MonsterManager monsterManager) : base(width, height)
        {
            _player = player;
            _monsterManager = monsterManager;
        }

        public void AddOptionsToBuffer()
        {
            AddToOutputBuffer("(0)Back to Menu");
            AddNewLine();
            AddToOutputBuffer(string.Format("Current Monster:  {0}", _monsterManager.CurrentMonster.Name));
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
                    _player.Attack(_monsterManager.CurrentMonster);
                    AddToOutputBuffer(_player.GetActions());
                    AddToOutputBuffer(_monsterManager.CurrentMonster.GetActions());
                }
                else if (keyInfo.Key == ConsoleKey.D2)
                {
                    _player.BombAttack(_monsterManager.CurrentMonster);
                    AddToOutputBuffer(_player.GetActions());
                    AddToOutputBuffer(_monsterManager.CurrentMonster.GetActions());
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
