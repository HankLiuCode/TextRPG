using System;
using System.Collections.Generic;
using System.Text;

namespace TextRPG
{
    class Locker : Obstacle
    {
        private char lockerSymbol;
        public Locker (string name, char symbol, Vector2 position) : base(name, symbol, position)
        {
            lockerSymbol = symbol;
        }

        public bool Open(Item item)
        {
            char keySymbol = ' ';

            if (item == Item.Key1)
            {
                keySymbol = '{';
            }
            else if (item == Item.Key2)
            {
                keySymbol = '[';
            }
            else if (item == Item.Key2)
            {
                keySymbol = '(';
            }
            return Open(keySymbol);
        }

        public bool Open(char keySymbol)
        {
            if(keySymbol == '{' && lockerSymbol == '}')
            {
                Destroy();
                return true;
            }
            else if (keySymbol == '[' && lockerSymbol == ']')
            {
                Destroy();
                return true;
            }
            else if (keySymbol == '(' && lockerSymbol == ')')
            {
                Destroy();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
