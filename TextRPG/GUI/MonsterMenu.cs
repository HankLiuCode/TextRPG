﻿using System;
using TextRPG.Graphics;

namespace TextRPG.GUI
{
    class MonsterMenu : Window
    {
        MonsterManager _monsterManager;
        public MonsterMenu(int width, int height, MonsterManager monsterManager) : base(width, height)
        {
            _monsterManager = monsterManager;
        }

        public override void Show()
        {
            ConsoleKeyInfo keyInfo;

            AddToOutputBuffer("(0) Back to Menu");
            AddNewLine();
            AddToOutputBuffer(_monsterManager.GetOptions());
            AddBorder();
            Render(0,0);

            while (true)
            {
                keyInfo = Console.ReadKey(_hidePressedKey);
                if (keyInfo.Key == ConsoleKey.D0)
                {
                    break;
                }

                AddToOutputBuffer("(0) Back to Menu");
                AddNewLine();
                AddToOutputBuffer(_monsterManager.GetOptions());
                AddBorder();

                if (char.IsDigit(keyInfo.KeyChar))
                {
                    int index = int.Parse(keyInfo.KeyChar.ToString());
                    _monsterManager.ChooseMonster(index - 1);
                    AddToOutputBuffer(_monsterManager.CurrentMonsterInfo());
                }
                else
                {
                    AddToOutputBuffer("Invalid Input");
                }

                Render(0, 0);
            }

        }
    }
}