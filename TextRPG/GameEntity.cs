using System;
using System.Collections.Generic;
using System.Text;
using TextRPG.Common;

namespace TextRPG
{
    public class OnMoveEventArgs : EventArgs
    {
        public Vector2 fromVector;
        public Vector2 toVector;
        public char symbol;
        public string name;

        public OnMoveEventArgs(Vector2 fromVector, Vector2 toVector, char symbol, string name)
        {
            this.fromVector = fromVector;
            this.toVector = toVector;
            this.symbol = symbol;
            this.name = name;
        }
    }
    public class OnDestroyEventArgs : EventArgs
    {
        public Vector2 destroyTarget;
        public char symbol;
        public string name;

        public OnDestroyEventArgs(Vector2 destroyTarget, char symbol, string name)
        {
            this.destroyTarget = destroyTarget;
            this.symbol = symbol;
            this.name = name;
        }
    }

    public class GameEntity
    {
        public event EventHandler<OnMoveEventArgs> OnMove;
        public event EventHandler<OnDestroyEventArgs> OnDestroy;
        public bool IsActive { get; private set; }
        public string name;
        char symbol;
        public Vector2 Position { get; private set; }

        public GameEntity(string name, char symbol, Vector2 position)
        {
            this.name = name;
            this.symbol = symbol;
            Position = position;

            //has to call map.Bind(this) in order to work
            IsActive = true;
        }

        public void Destroy(GameEntity gameEntity)
        {
            IsActive = false;
            if (OnDestroy != null) OnDestroy.Invoke(this, new OnDestroyEventArgs(Position, symbol, name));
        }

        public void SetPosition(Vector2 newPosition)
        {
            if (OnMove != null) OnMove.Invoke(this, new OnMoveEventArgs(Position, newPosition, symbol, name));
            Position = newPosition;
        }
    }
}
