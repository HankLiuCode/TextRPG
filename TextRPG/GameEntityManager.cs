using System;
using System.Collections.Generic;
using System.Text;

namespace TextRPG
{
    class GameEntityManager
    {
        Dictionary<char, Func<Vector2, GameEntity>> charGameEntityMapper = new Dictionary<char, Func<Vector2, GameEntity>>();
        public List<GameEntity> gameEntities = new List<GameEntity>();
        public event EventHandler OnLoad;
        public event EventHandler OnUnload;

        public GameEntityManager()
        {
            charGameEntityMapper.Add('m', (Vector2 pos) => new Monster("Monster", 'm', pos, new Stats(1, 4, 1, 1)));
            charGameEntityMapper.Add('#', (Vector2 pos) => new Obstacle("Obstacle", '#', pos));
            charGameEntityMapper.Add('!', (Vector2 pos) => new ItemEntity("HealthPotion",'!',Item.HealthPotion,pos));
            charGameEntityMapper.Add('i', (Vector2 pos) => new ItemEntity("StrengthPotion", 'i', Item.StrengthPotion, pos));
            charGameEntityMapper.Add('O', (Vector2 pos) => new ItemEntity("Bomb", 'O', Item.Bomb, pos));

            charGameEntityMapper.Add('{', (Vector2 pos) => new ItemEntity("KEY_curly", '{', Item.KEY_curly, pos));
            charGameEntityMapper.Add('[', (Vector2 pos) => new ItemEntity("KEY_square", '[', Item.KEY_square, pos));
            charGameEntityMapper.Add('(', (Vector2 pos) => new ItemEntity("KEY_round", '(', Item.KEY_round, pos));

            charGameEntityMapper.Add('}', (Vector2 pos) => new Locker("LOCKER_curly", '}', pos));
            charGameEntityMapper.Add(']', (Vector2 pos) => new Locker("LOCKER_square", ']', pos));
            charGameEntityMapper.Add(')', (Vector2 pos) => new Locker("LOCKER_round", ')', pos));
        }

        public void UnloadGameEntities()
        {
            foreach (GameEntity ge in gameEntities)
            {
                MapController.UnBind(ge);
            }
            MapController.UnBindAll();
            gameEntities.Clear();
            OnUnload?.Invoke(this, EventArgs.Empty);
        }

        public void LoadGameEntities(Map map)
        {
            for(int i=0; i<map.Width; i++)
            {
                for(int j=0; j<map.Height; j++)
                {
                    char c = map[i, j];
                    if (charGameEntityMapper.ContainsKey(c))
                    {
                        GameEntity ge = charGameEntityMapper[c](new Vector2(i, j));
                        ge.OnDestroy += Ge_OnDestroy;
                        gameEntities.Add(ge);
                        MapController.Bind(ge, map);
                    }
                }
            }
            OnLoad?.Invoke(this, EventArgs.Empty);
        }

        private void Ge_OnDestroy(object sender, OnDestroyEventArgs e)
        {
            gameEntities.Remove(e.destroyTarget);
        }

        public GameEntity GetGameEntity(Vector2 checkPos)
        {
            foreach(GameEntity ge in gameEntities)
            {
                if(ge.Position == checkPos)
                {
                    return ge;
                }
            }
            return null;
        }

        public void Remove(GameEntity ge)
        {
            gameEntities.Remove(ge);
            ge.Destroy();
        }

        // TODO:
        // GameEntity, Vector2 -> void
        // instantiate gameEntity in map at runtime

        public List<T> Find<T>() where T : GameEntity
        {
            List<T> targetEntites = new List<T>();
            foreach(GameEntity ge in gameEntities)
            {
                if(typeof(T) == ge.GetType())
                {

                    targetEntites.Add((T)ge);
                }
            }
            return targetEntites;
        }
    }
}
