using System;
using TextRPG.Graphics;

namespace TextRPG.GUI
{
    class MonsterMenu : BadWindow
    {
        public MonsterMenu(int width, int height) : base(width, height)
        {

        }

        public void AddOptionsToBuffer()
        {
            AddToOutputBuffer("(0) Back to Menu");
            AddNewLine();
            AddToOutputBuffer(MonsterManager.GetOptions());
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
                if (char.IsDigit(keyInfo.KeyChar))
                {
                    int index = int.Parse(keyInfo.KeyChar.ToString());
                    MonsterManager.ChooseMonster(index - 1);
                    AddToOutputBuffer(MonsterManager.CurrentMonsterInfo());
                }
                else
                {
                    AddToOutputBuffer("Invalid Input");
                }

                Render(DEFAULT_START_POS_Y, DEFAULT_START_POS_Y);
            }

        }
    }
}
