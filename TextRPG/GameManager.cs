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

        public static void Initialize()
        {
            paths = new Dictionary<string, Door>();


            Map room1 = ReadRoomFile("Rooms\\room1", "room1");
            Map room2 = ReadRoomFile("Rooms\\room2", "room2");
            Map room3 = ReadRoomFile("Rooms\\room3", "room3");
            Map room4 = ReadRoomFile("Rooms\\room4", "room4");

            ConnectMaps(room1, room2);
            ConnectMaps(room2, room3);
            ConnectMaps(room2, room4);

            CurrentMap = room1;
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

        public static Map ReadRoomFile(string path, string roomName)
        {
            string room = File.ReadAllText(path);
            return new Map(room, roomName);
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
