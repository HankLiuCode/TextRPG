using System;
using System.Collections.Generic;
using System.Text;

namespace TextRPG.Utils
{
    public static class RPGMath
    {
        public static float Clamp(float num, float min, float max)
        {
            if (num < min)
                return min;
            if (num > max)
                return max;
            return num;
        }

        public static float Clamp01(float num)
        {
            return Clamp(num, 0, 1);
        }
    }
}
