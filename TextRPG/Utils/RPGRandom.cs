using System;

namespace TextRPG.Utils
{
    class RPGRandom
    {
        private Random random;

        public RPGRandom()
        {
            random = new Random();
        }

        public float NextFloat(float min, float max)
        {
            if (min > max) 
                throw new ArgumentException("min > max", "min cannot be larger than max");

            float result = MathF.Round((float)(random.NextDouble() * (max - min) + min), 2);
            return result;
        }

        public string PickFrom(string[] strings)
        {
            int index = random.Next(0, strings.Length);
            return strings[index];
        }
    }
}
