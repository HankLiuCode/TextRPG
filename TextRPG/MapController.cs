using System;
using System.Collections.Generic;

namespace TextRPG
{
    static class MapController
    {
        public static Dictionary<GameEntity, Map> bindings = new Dictionary<GameEntity, Map>();
        public static Dictionary<GameEntity, char> groundChar = new Dictionary<GameEntity, char>();
        private static char defaultChar = '.';

        public static void UnBind(GameEntity gameEntity)
        {
            if (!bindings.ContainsKey(gameEntity))
            {
                throw new Exception(string.Format("Map Controller doesn't contain {0}", gameEntity.name));
            }
            
            bindings.Remove(gameEntity);
            groundChar.Remove(gameEntity);

            gameEntity.OnMove -= GameEntity_OnMove;
            gameEntity.OnDestroy -= GameEntity_OnDestroy;
            gameEntity.IsActive = false;
        }

        public static void Bind(GameEntity gameEntity, Map map)
        {
            if(map.GetChar(gameEntity.Position) != gameEntity.Symbol)
            {
                throw new Exception(string.Format("{0}.Position is not the same in Map", gameEntity.name));
            }
            if (bindings.ContainsKey(gameEntity))
            {
                throw new Exception(string.Format("Map already contains {0}", gameEntity.name));
            }

            bindings.Add(gameEntity, map);
            groundChar.Add(gameEntity, defaultChar);

            gameEntity.OnMove += GameEntity_OnMove;
            gameEntity.OnDestroy += GameEntity_OnDestroy;
            gameEntity.IsActive = true;
        }

        public static void UnBindAll()
        {
            List<GameEntity> toUnbind = new List<GameEntity>();
            foreach(GameEntity gameEntity in bindings.Keys)
            {
                toUnbind.Add(gameEntity);
            }
            foreach(GameEntity gameEntity in toUnbind)
            {
                UnBind(gameEntity);
            }
        }

        private static void GameEntity_OnDestroy(object sender, OnDestroyEventArgs e)
        {
            if (bindings.ContainsKey(e.destroyTarget))
            {
                Vector2 destroyPosition = e.destroyTarget.Position;
                bindings[e.destroyTarget].SetChar(destroyPosition, groundChar[e.destroyTarget]);
                groundChar.Remove(e.destroyTarget);
                UnBind(e.destroyTarget);
            }
            else
            {
                throw new Exception(string.Format( "Destroy Target {0} doesn't exists", e.destroyTarget.name));
            }
        }

        private static void GameEntity_OnMove(object sender, OnMoveEventArgs e)
        {
            if (bindings.ContainsKey(e.moveTarget))
            {
                if(e.moveTarget.Position != e.targetPosition)
                {
                    char temp = groundChar[e.moveTarget];
                    Vector2 fromPosition = e.moveTarget.Position;
                    bindings[e.moveTarget].RoundSwitch(ref temp, fromPosition, e.targetPosition);
                    groundChar[e.moveTarget] = temp;
                    // Console.WriteLine(string.Format("{0} has moved from {1} to {2}", e.moveTarget, e.moveTarget.Position, e.targetPosition));
                }
            }
            else
            {
                throw new Exception(string.Format("Move Target {0} doesn't exists", e.moveTarget.name));
            }
        }
    }
}
