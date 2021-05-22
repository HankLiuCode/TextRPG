using System;
using TextRPG.Graphics;

namespace TextRPG
{
    class PlayerMenu : Window
    {
        private Player _player;
        private MonsterManager _monsterManager;
        public PlayerMenu(int width, int height, Player player, MonsterManager monsterManager) : base(width, height)
        {
            _player = player;
            _monsterManager = monsterManager;
        }
        public override void Show()
        {
            ConsoleKeyInfo keyInfo;

            AddToOutputBuffer("(0)Back to Menu");
            AddToOutputBuffer("(1)Stats  (2)Inventory");
            AddNewLine();
            AddBorder();
            Render(0, 0);

            while (true)
            {
                keyInfo = Console.ReadKey(_hidePressedKey);
                if (keyInfo.Key == ConsoleKey.D0)
                {
                    break;
                }

                AddToOutputBuffer("(0)Back to Menu");
                AddToOutputBuffer("(1)Stats  (2)Inventory");
                AddNewLine();
                AddBorder();

                if (keyInfo.Key == ConsoleKey.D1)
                {
                    AddToOutputBuffer("1 is pressed");
                }
                else if (keyInfo.Key == ConsoleKey.D2)
                {
                    AddToOutputBuffer("2 is pressed");
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
