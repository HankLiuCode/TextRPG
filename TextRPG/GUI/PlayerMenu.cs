using System;
using TextRPG.Graphics;

namespace TextRPG.GUI
{
    class PlayerMenu : BadWindow
    {
        private Player _player;
        public PlayerMenu(int width, int height, Player player) : base(width, height)
        {
            _player = player;
        }

        public void AddOptionsToBuffer()
        {
            AddToOutputBuffer("(0)Back to Menu");
            AddToOutputBuffer("(1)Stats  (2)Inventory");
            AddNewLine();
            AddBorder();
        }
        public override void Show()
        {
            ConsoleKeyInfo keyInfo;
            AddOptionsToBuffer();
            Render(DEFAULT_START_POS_Y, DEFAULT_START_POS_Y);

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
                    AddToOutputBuffer(_player.Summary());
                }
                else if (keyInfo.Key == ConsoleKey.D2)
                {
                    AddToOutputBuffer(_player.InventorySummary());
                }
                else if (keyInfo.Key == ConsoleKey.D0)
                {
                    break;
                }
                else
                {
                    AddToOutputBuffer("Not a valid input.");
                }

                Render(DEFAULT_START_POS_Y, DEFAULT_START_POS_Y);
            }
        }
    }
}
