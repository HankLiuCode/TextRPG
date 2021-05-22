using System;
using System.Collections.Generic;
using System.Text;

namespace TextRPG
{
    public class MonsterManager
    {
        public Monster CurrentMonster {
            get
            {
                return monsters[currentMonsterIndex];
            }
        }
        private List<Monster> monsters;
        private int currentMonsterIndex;

        public MonsterManager()
        {
            monsters = new List<Monster>();

            for(int i = 0; i < 8; i++)
            {
                SpawnMonster("Monster " + (i+1));
            }
            currentMonsterIndex = 0;
        }

        public void ChooseMonster(int index)
        {
            if (index < monsters.Count && index > -1)
            {
                currentMonsterIndex = index;
            }
        }

        public string[] CurrentMonsterInfo()
        { 
            return monsters[currentMonsterIndex].GetInfo();
        }

        public void SpawnMonster(string name)
        {
            Monster m = new Monster(name);
            m.Died += M_Died;
            monsters.Add(m);
        }

        private void M_Died(object sender, EventArgs e)
        {
            
        }

        public string[] GetOptions()
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
