using System;
using System.Collections.Generic;
using TextRPG.Common;

namespace TextRPG
{
    static class MonsterManager
    {
        public static List<Character> monsters = new List<Character>();
        static Map _map;

        public static void FindMonsters(Map map, char monsterSymbol)
        {
            _map = map;
            Vector2[] monsterPositions = map.FindAll(monsterSymbol);
            foreach (Vector2 mp in monsterPositions)
            {
                Character monster = new Character("Monster", monsterSymbol, mp, new Stats(1, 7, 1, 1));
                monsters.Add(monster);
                map.Bind(monster);
            }
        }

        public static Character GetMonster(Vector2 checkPos)
        {
            foreach(Character m in monsters)
            {
                if (m.Position == checkPos && m.IsActive)
                    return m;
            }
            return null;
        }
    }
}
