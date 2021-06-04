using System;
using System.Collections.Generic;

namespace TextRPG
{
    static class ObstacleManager
    {
        public static List<Obstacle> obstacles = new List<Obstacle>();

        public static void LoadObstacles(Map map, char symbol)
        {
            Vector2[] obstaclePositions = map.FindCharPositions(symbol);

            for (int i = 0; i < obstaclePositions.Length; i++)
            {
                Obstacle obstacle = new Obstacle("Obstacle" + i, symbol, obstaclePositions[i]);
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
