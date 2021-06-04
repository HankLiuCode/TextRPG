using System.Collections.Generic;


namespace TextRPG.Graphics
{
    // TODO: 
    // WriteLine and Write function

    class Window
    {
        public Vector2 Position { get; set; }
        public Vector2 Rect { get; set; }

        //public int RenderOrder { get; set; }
        public bool HasBorder { get; private set; }
        public bool IsVisible { get; private set; }

        private char top;
        private char side;

        private List<string> _buffer = new List<string>();
        public List<string> Buffer { 
            get 
            {
                return HasBorder ? BufferWithBorder(_buffer) : _buffer;
            }
        }

        public Window(Vector2 position, Vector2 rect, bool hasBorder = true, bool isVisible = true, char top = '=', char side='|')
        {
            Position = position;
            Rect = rect;
            IsVisible = isVisible;
            HasBorder = hasBorder;
            this.top = top;
            this.side = side;
            _buffer = new List<string>();
        }

        public void Write(string targetString)
        {
            targetString = targetString.Replace("\n", " ").Replace("\t", " ").Trim();
            _buffer.Add(targetString);
        }

        public void Write(string[] targetStrings)
        {
            foreach(string s in targetStrings)
            {
                Write(s);
            }
        }

        public void Write(Vector2 vec2)
        {
            Write(vec2.ToString());
        }

        public void Write(float val)
        {
            Write(val.ToString());
        }

        public void Clear()
        {
            _buffer.Clear();
        }

        public void Hide()
        {
            IsVisible = false;
        }

        public void Show()
        {
            IsVisible = true;
        }

        public void SetBorder(char top, char side)
        {
            this.top = top;
            this.side = side;
            HasBorder = true;
        }

        public void RemoveBorder()
        {
            HasBorder = false;
        }

        private List<string> BufferWithBorder(List<string> buffer)
        {
            List<string> tempBuffer = new List<string>();
            tempBuffer.Add(new string(top, Rect.x));
            for (int i = 0; i < Rect.y - 2; i++)
            {
                if (i < buffer.Count)
                {
                    string tempStr = buffer[i];
                    if (tempStr.Length > Rect.x - 2)
                    {
                        tempStr = tempStr.Remove(Rect.x - 2, tempStr.Length - (Rect.x - 2));
                    }
                    else if (tempStr.Length < Rect.x - 2)
                    {
                        tempStr = tempStr + new string(' ', (Rect.x - 2) - tempStr.Length);
                    }
                    tempStr = side + tempStr + side;

                    tempBuffer.Add(tempStr);
                }
                else
                {
                    tempBuffer.Add(side + new string(' ', Rect.x - 2) + side);
                }
            }
            tempBuffer.Add(new string(top, Rect.x));

            return tempBuffer;
        }
    }
}
