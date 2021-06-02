using System;
using TextRPG.Graphics;

namespace TextRPG.GUI
{
    class MainMenu : BadWindow
    {
        private MonsterMenu _monsterMenu;

        private PlayerMenu _playerMenu;
        private CombatMenu _combatMenu;
        public MainMenu(int width, int height, MonsterMenu monsterMenu, PlayerMenu playerMenu, CombatMenu combatMenu) : base(width, height)
        {
            _monsterMenu = monsterMenu;
            _playerMenu = playerMenu;
            _combatMenu = combatMenu;
        }

        public void AddOptionsToBuffer()
        {
            AddToOutputBuffer(string.Format("Current Monster:  {0}", MonsterManager.CurrentMonster.Name));
            AddToOutputBuffer("(1)Player Info  (2)Combat  (3)Choose Monster  (4)Revive Monster  (q)Quit");
            AddNewLine(2);
            AddBorder();
        }

        public override void Show()
        {
            ConsoleKeyInfo keyInfo;
            AddOptionsToBuffer();
            Render(DEFAULT_START_POS_X, DEFAULT_START_POS_Y);

            while (true)
            {
                
                keyInfo = Console.ReadKey(_hidePressedKey);

                if(keyInfo.Key == ConsoleKey.D1)
                {
                    _playerMenu.Show();
                    AddOptionsToBuffer();
                }
                else if (keyInfo.Key == ConsoleKey.D2)
                {
                    _combatMenu.Show();
                    AddOptionsToBuffer();
                }
                else if (keyInfo.Key == ConsoleKey.D3)
                {
                    _monsterMenu.Show();
                    AddOptionsToBuffer();
                }
                else if (keyInfo.Key == ConsoleKey.D4)
                {
                    AddOptionsToBuffer();
                    MonsterManager.CurrentMonster.Revive();
                    //AddToOutputBuffer(MonsterManager.CurrentMonster.GetActions(true));
                }
                else if (keyInfo.Key == ConsoleKey.Q)
                {
                    AddToOutputBuffer("GoodBye Fellow Hero.");
                    AddToOutputBuffer("See you again soon.");
                    Render(DEFAULT_START_POS_Y, DEFAULT_START_POS_Y);
                    break;
                }
                else
                {
                    AddOptionsToBuffer();
                    AddToOutputBuffer("Not a valid input.");
                }
                Render(DEFAULT_START_POS_Y, DEFAULT_START_POS_Y);
            }
        }
    }
}
