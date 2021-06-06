using System;
using System.Collections.Generic;

namespace TextRPG
{
    static class ObstacleManager
    {
        public static List<Obstacle> obstacles = new List<Obstacle>();
        private static char obstacleSymbol = '#';



        public static void LoadObstacles(Map map)
        {

            Vector2[] obstaclePositions = map.FindCharPositions(obstacleSymbol);

            for (int i = 0; i < obstaclePositions.Length; i++)
            {
                Obstacle obstacle = new Obstacle("Obstacle" + i, obstacleSymbol, obstaclePositions[i]);
                obstacles.Add(obstacle);
                MapController.Bind(obstacle, map);
            }
        }

        public static void UnloadObstacles()
        {
            foreach (Obstacle obstacle in obstacles)
            {
                MapController.UnBind(obstacle);
            }
            obstacles.Clear();
        }

        public static Obstacle GetObstacle(Vector2 checkPos)
        {
            foreach (Obstacle m in obstacles)
            {
                if (m.Position == checkPos && m.IsActive)
                    return m;
            }
            return null;
        }
    }
}
