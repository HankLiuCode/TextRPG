using System;
using System.Collections.Generic;

namespace TextRPG
{

    public class OnMonsterDiedEventArgs : EventArgs
    {
        public Monster diedMonster;
        public OnMonsterDiedEventArgs(Monster monster)
        {
            this.diedMonster = monster;
        }

    }
    static class MonsterManager
    {
        public static List<Monster> monsters = new List<Monster>();
        public static event EventHandler<OnMonsterDiedEventArgs> OnMonsterDied;
        public static event EventHandler OnReload;

        public static void LoadMonsters(Map map)
        {
            char symbol = 'm';
            Vector2[] monsterPositions = map.FindCharPositions(symbol);

            for(int i=0; i < monsterPositions.Length; i++)
            {
                Monster monster = new Monster("Monster"+ i, symbol, monsterPositions[i], new Stats(1, 4, 1, 1));
                monsters.Add(monster);
                monster.OnHealthModified += Monster_OnHealthModified;
                MapController.Bind(monster, map);
            }
            OnReload?.Invoke(null, EventArgs.Empty);
        }

        public static void UnloadMonsters()
        {
            foreach(Monster monster in monsters)
            {
                monster.OnHealthModified -= Monster_OnHealthModified;
                MapController.UnBind(monster);
            }
            monsters.Clear();
        }

        private static void Monster_OnHealthModified(object sender, OnHealthModifiedEventArgs e)
        {
            if(e.healthState == HealthState.Dead)
            {
                monsters.Remove((Monster)e.character);
                if (OnMonsterDied != null) OnMonsterDied.Invoke(e, new OnMonsterDiedEventArgs((Monster) e.character));
            }
        }

        public static Monster GetMonster(Vector2 checkPos)
        {
            foreach(Monster m in monsters)
            {
                if (m.Position == checkPos && m.IsActive)
                    return m;
            }
            return null;
        }
    }
}
