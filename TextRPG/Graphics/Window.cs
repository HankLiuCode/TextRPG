using System;
using System.Collections.Generic;
using System.Text;

namespace TextRPG.Graphics
{
    public abstract class Window
    {
        public const int DEFAULT_START_POS_X = 0;
        public const int DEFAULT_START_POS_Y = 0;

        protected int _width;
        protected int _height;
        protected bool _hidePressedKey;
        private List<string> _outputBuffer;

        public Window(int width, int height, bool hidePressedKey = true)
        {
            _width = width;
            _height = height;
            _hidePressedKey = hidePressedKey;
            _outputBuffer = new List<string>();
            Console.CursorVisible = false;
        }

        protected void Render(int cursorX, int cursorY)
        {
            Render(FlushOutputBuffer(), cursorX, cursorY);
        }
        
        protected void Render(string[] renderString, int cursorX, int cursorY)
        {
            Console.SetCursorPosition(cursorX, cursorY);

            string boldHBorder = new string('=', _width);
            string hBorder = new string('-', _width);
            string emptyLine = "|" + new string(' ', _width - 2) + "|";
            int cursorPositionY = 0;

            
            Console.WriteLine(boldHBorder);
            Console.WriteLine(emptyLine);
            cursorPositionY += 2;

            string targetFormatString = "|"+ "{0," + -(_width - 2) + "}" + "|";

            for(int i=0; i < renderString.Length; i++)
            {
                if (cursorPositionY > _height - 1)
                    break;
                string resultString = string.Format(targetFormatString, renderString[i]);
                Console.WriteLine(resultString);
                cursorPositionY += 1;
            }

            for(int i=cursorPositionY; i<_height - 1; i++)
            {
                Console.WriteLine(emptyLine);
            }
            Console.WriteLine(hBorder);
        }

        protected void AddBorder()
        {
            string boldHBorder = new string('=', _width - 2);
            AddToOutputBuffer(boldHBorder);
        }

        protected void AddNewLine(int count = 1)
        {
            for(int i = 0; i < count; i++)
            {
                AddToOutputBuffer("");
            }
        }

        protected void InsertIntoOutputBuffer(int index, string output)
        {
            _outputBuffer.Insert(index, output);
        }

        protected void AddToOutputBuffer(string output)
        {
            _outputBuffer.Add(output);
        }

        protected void AddToOutputBuffer(string[] output)
        {
            for(int i = 0; i < output.Length; i++)
            {
                AddToOutputBuffer(output[i]);
            }
        }

        protected string[] FlushOutputBuffer()
        {
            string[] arr = _outputBuffer.ToArray();
            _outputBuffer.Clear();
            return arr;
        }

        public abstract void Show();
    }
}
