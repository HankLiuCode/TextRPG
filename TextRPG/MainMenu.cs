using System;
using TextRPG.Graphics;

namespace TextRPG
{
    class MainMenu : Window
    {
        private MonsterMenu _monsterMenu;
        private MonsterManager _monsterManager;

        private PlayerMenu _playerMenu;
        public MainMenu(int width, int height, MonsterMenu monsterMenu, MonsterManager monsterManager, PlayerMenu playerMenu) : base(width, height)
        {
            _monsterMenu = monsterMenu;
            _monsterManager = monsterManager;
            _playerMenu = playerMenu;
        }

        public override void Show()
        {
            ConsoleKeyInfo keyInfo;

            AddToOutputBuffer(string.Format("Current Monster:  {0}", _monsterManager.CurrentMonster.Name));
            AddToOutputBuffer("(1)Player Info  (2)Combat  (3)Choose Monster    (0)Quit");
            AddNewLine(2);
            AddBorder();
            Render(FlushOutputBuffer(), 0, 0);

            while (true)
            {
                
                keyInfo = Console.ReadKey(_hidePressedKey);

                if(keyInfo.Key == ConsoleKey.D1)
                {
                    _playerMenu.Show();
                    AddToOutputBuffer(string.Format("Current Monster:  {0}", _monsterManager.CurrentMonster.Name));
                    AddToOutputBuffer("(1)Player Info  (2)Combat  (3)Choose Monster    (0)Quit");
                    AddNewLine(2);
                    AddBorder();
                }
                else if (keyInfo.Key == ConsoleKey.D2)
                {
                    AddToOutputBuffer(string.Format("Current Monster:  {0}", _monsterManager.CurrentMonster.Name));
                    AddToOutputBuffer("(1)Player Info  (2)Combat  (3)Choose Monster    (0)Quit");
                    AddNewLine(2);
                    AddBorder();
                    AddToOutputBuffer("2 is pressed");
                }
                else if (keyInfo.Key == ConsoleKey.D3)
                {
                    _monsterMenu.Show();
                    AddToOutputBuffer(string.Format("Current Monster:  {0}", _monsterManager.CurrentMonster.Name));
                    AddToOutputBuffer("(1)Player Info  (2)Combat  (3)Choose Monster    (0)Quit");
                    AddNewLine(2);
                    AddBorder();
                }
                else if (keyInfo.Key == ConsoleKey.D0)
                {
                    break;
                }
                else
                {
                    AddToOutputBuffer(string.Format("Current Monster:  {0}", _monsterManager.CurrentMonster.Name));
                    AddToOutputBuffer("(1)Player Info  (2)Combat  (3)Choose Monster    (0)Quit");
                    AddNewLine(2);
                    AddBorder();
                    AddToOutputBuffer("Not a valid input.");
                }

                Render(FlushOutputBuffer(), 0, 0);
            }
        }
    }
}
