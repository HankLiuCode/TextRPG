using System;
using System.Collections.Generic;
using System.Text;

namespace TextRPG
{
    public class MonsterManager
    {
        public Monster CurrentMonster { get; private set; }

        public MonsterManager()
        {
            NextMonster();
        }


        public void NextMonster()
        {
            Monster m = new Monster();
            CurrentMonster = m;
            m.Died += M_Died;
        }

        private void M_Died(object sender, EventArgs e)
        {
            NextMonster();
        }
    }
}
