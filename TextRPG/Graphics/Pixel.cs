using System;

namespace TextRPG.Graphics
{
    class Pixel
    {
        public ConsoleColor bgColor;
        public ConsoleColor color;

        public Pixel(ConsoleColor bgColor, ConsoleColor color)
        {
            this.bgColor = bgColor;
            this.color = color;
        }

        public Pixel(ConsoleColor color)
        {
            this.bgColor = ConsoleColor.Black;
            this.color = color;
        }
    }
}
