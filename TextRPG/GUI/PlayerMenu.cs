using System;
using TextRPG.Graphics;

namespace TextRPG.GUI
{
    class PlayerMenu : Window
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
                    AddToOutputBuffer(_player.GetInfo());
                }
                else if (keyInfo.Key == ConsoleKey.D2)
                {
                    AddToOutputBuffer(_player.GetInventoryInfo());
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
