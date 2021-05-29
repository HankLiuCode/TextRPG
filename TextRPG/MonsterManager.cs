using System;
using System.Collections.Generic;
using System.Text;

namespace TextRPG
{
    public static class MonsterManager
    {
        public static Monster CurrentMonster {
            get
            {
                return monsters[currentMonsterIndex];
            }
        }
        private static List<Monster> monsters;
        private static int currentMonsterIndex;

        static MonsterManager()
        {

            monsters = new List<Monster>();

            for(int i = 0; i < 8; i++)
            {
                SpawnMonster("Monster " + (i+1));
            }
            currentMonsterIndex = 0;
        }

        public static void ChooseMonster(int index)
        {
            if (index < monsters.Count && index > -1)
            {
                currentMonsterIndex = index;
            }
        }

        public static string[] CurrentMonsterInfo()
        {
            return monsters[currentMonsterIndex].Summary();
        }

        public static void SpawnMonster(string name)
        {
            Monster m = new Monster(name);
            m.Died += M_Died;
            monsters.Add(m);
        }

        private static void M_Died(object sender, EventArgs e)
        {
            
        }

        public static string[] GetOptions()
        {
            string[] options = new string[monsters.Count];
            for(int i=0; i<options.Length; i++)
            {
                options[i] = string.Format("({0}) {1}", i+1, monsters[i].Name);
            }
            return options;
        }
    }
}
