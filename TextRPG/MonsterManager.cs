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
            SpawnMonster();
            currentMonsterIndex = 0;
        }

        public void ChooseMonster(int inputIndex)
        {
            int index = inputIndex - 1;
            if (index < monsters.Count && index > -1)
            {
                currentMonsterIndex = index;
                Console.WriteLine("You chose {0} as your opponent", monsters[currentMonsterIndex].Name);
                monsters[currentMonsterIndex].PrintStatus();
            }
            else
                Console.WriteLine("Invalid Monster Index!");
        }

        public void SpawnMonster()
        {
            Monster m = new Monster();
            m.Died += M_Died;
            monsters.Add(m);
        }

        private void M_Died(object sender, EventArgs e)
        {
            
        }

        public void PrintMonsters()
        {
            string monstersUI = "";
            for(int i=0; i<monsters.Count; i++)
            {
                monstersUI += string.Format("{0,-3}{1,-20}", i + 1, monsters[i].Name);
                if ((i+1) % 5 == 0)
                    monstersUI += "\n";
            }
            Console.WriteLine(monstersUI);
        }
    }
}
