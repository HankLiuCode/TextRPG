using System;
using System.Collections.Generic;
using TextRPG.Common;

namespace TextRPG
{
    static class MonsterManager
    {
        public static List<Character> monsters = new List<Character>();

        public static void LoadMonsters(Map map, char symbol)
        {
            Vector2[] monsterPositions = map.FindCharPositions(symbol);

            for(int i=0; i < monsterPositions.Length; i++)
            {
                Character monster = new Character("Monster "+ i, symbol, monsterPositions[i], new Stats(1, 7, 1, 1));
                monsters.Add(monster);
                MapController.Bind(monster, map);
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
