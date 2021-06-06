using System.IO;
using System.Collections.Generic;
using System;

namespace TextRPG
{
    public class Door
    {
        public Map map;
        public Vector2 position;


        public Door(Map map, Vector2 position)
        {
            this.map = map;
            this.position = position;
        }
    }

    static class GameManager
    {
        public static Map CurrentMap;
        private static Dictionary<string, Door> paths;

        private static List<Map> maps;

        public static void Initialize()
        {
            paths = new Dictionary<string, Door>();
            maps = new List<Map>();

            Map level1 = ReadLevelFile("Levels\\level1", "level1");
            Map level2 = ReadLevelFile("Levels\\level2", "level2");
            Map level3 = ReadLevelFile("Levels\\level3", "level3");
            Map level4 = ReadLevelFile("Levels\\level4", "level4");

            maps.AddRange(new Map[3] { level1, level2, level3});

            ConnectMaps(level1, level2);
            ConnectMaps(level2, level3);
            ConnectMaps(level2, level4);

            CurrentMap = level1;
            LoadMap(CurrentMap);
        }

        public static void ConnectMaps(Map map1, Map map2)
        {
            for(int i = '0'; i < '9' + 1; i++)
            {
                char c = (char)i;
                Vector2 doorPos = map1.FindCharPosition(c);
                Vector2 doorPos2 = map2.FindCharPosition(c);
                if (doorPos != Vector2.None && doorPos2 != Vector2.None)
                {
                    AddPath(new Door(map1, doorPos), new Door(map2, doorPos2));
                    map1.SetChar(doorPos, '.');
                    map2.SetChar(doorPos2, '.');
                }
            }
        }

        public static Map ReadLevelFile(string path, string levelName)
        {
            string level = File.ReadAllText(path);
            return new Map(level, levelName);
        }

        public static void AddPath(Door doorA, Door doorB)
        {
            string doorAId = doorA.map.Name + doorA.position;
            string doorBId = doorB.map.Name + doorB.position;

            if (paths.ContainsKey(doorAId) || paths.ContainsKey(doorBId))
                throw new Exception("Already contains door");
            paths.Add(doorAId, doorB);
            paths.Add(doorBId, doorA);

        }

        public static Door GetDoor(Vector2 position)
        {
            string doorKey = CurrentMap.Name + position;
            if (paths.ContainsKey(doorKey))
            {
                return paths[doorKey];
            }
            return null;
        }

        public static void LoadMap(Map map)
        {
            CurrentMap = map;
            MonsterManager.UnloadMonsters();
            ObstacleManager.UnloadObstacles();
            MapController.UnBindAll();

            MonsterManager.LoadMonsters(map, 'm');
            ObstacleManager.LoadObstacles(map, '#');
        }

        public static void LoadMap(Door door, GameEntity gameEntity, Vector2 direction)
        {
            CurrentMap.SetChar(gameEntity.Position, '.');
            LoadMap(door.map);

            gameEntity.Position = door.position + direction;

            CurrentMap.SetChar(gameEntity.Position, gameEntity.Symbol);
            MapController.Bind(gameEntity, CurrentMap);
        }
    }
}
