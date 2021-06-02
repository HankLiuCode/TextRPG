using TextRPG.Utils;

namespace TextRPG
{
    public struct CharacterStats
    {
        private const float ARMOR_CLASS_MAX = 10f;
        private const float ARMOR_CLASS_MIN = 5f;
        private const float STRENGTH_MAX = 16f;
        private const float STRENGTH_MIN = 10f;
        private const float DEXERITY_MAX = 10f;
        private const float DEXERITY_MIN = 1f;
        private const float ACCURACY_MAX = 0.5f;
        private const float ACCURACY_MIN = 0.1f;

        public float armorClass;
        public float strength;
        public float dexerity;
        public float accuracy;

        public CharacterStats(float armorClass, float strength, float dexerity, float accuracy)
        {
            this.armorClass = armorClass;
            this.strength = strength;
            this.dexerity = dexerity;
            this.accuracy = accuracy;
        }

        public void GenerateRandom()
        {
            this.armorClass = RPGRandom.NextFloat(ARMOR_CLASS_MIN, ARMOR_CLASS_MAX);
            this.strength = RPGRandom.NextFloat(STRENGTH_MIN, STRENGTH_MAX);
            this.dexerity = RPGRandom.NextFloat(DEXERITY_MIN, DEXERITY_MAX);
            this.accuracy = RPGRandom.NextFloat(ACCURACY_MIN, ACCURACY_MAX);
        }

        public string[] Summary()
        {
            string[] info = new string[5];
            info[0] = "Stats:";
            info[1] = string.Format("   -ArmorClass: {0,-10}", armorClass);
            info[2] = string.Format("   -Strength:   {0,-10}", strength);
            info[3] = string.Format("   -Dexterity:  {0,-10}", dexerity);
            info[4] = string.Format("   -Accuracy:   {0,-10}", accuracy);
            return info;
        }
    }
}
