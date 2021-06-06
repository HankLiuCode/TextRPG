using System;
using System.Collections.Generic;
using System.Text;

namespace TextRPG
{
    class LockerManager
    {
        static List<Locker> lockers = new List<Locker>();
        static List<char> lockerSymbols = new List<char>();
        
        static LockerManager()
        {
            lockerSymbols.Add('}');
            lockerSymbols.Add(']');
            lockerSymbols.Add(')');
        }


        public static void LoadLockers(Map map)
        {
            foreach(char symbol in lockerSymbols)
            {
                Vector2[] lockerPosArr = map.FindCharPositions(symbol);
                for(int i=0; i<lockerPosArr.Length; i++)
                {
                    Locker locker = new Locker("Locker" + i, symbol, lockerPosArr[i]);
                    locker.OnDestroy += Locker_OnDestroy;
                    lockers.Add(locker);
                    MapController.Bind(locker, map);
                }
            }
        }

        private static void Locker_OnDestroy(object sender, OnDestroyEventArgs e)
        {
            lockers.Remove((Locker)e.destroyTarget);
        }

        public static void UnloadLockers()
        {
            foreach(Locker locker in lockers)
            {
                MapController.UnBind(locker);
            }
            lockers.Clear();
        }

        public static Locker GetLocker(Vector2 checkPos)
        {
            foreach (Locker locker in lockers)
            {
                if (locker.Position == checkPos)
                    return locker;
            }
            return null;
        }
    }
}
