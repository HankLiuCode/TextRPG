using System;
using System.Collections.Generic;
using System.Text;

namespace TextRPG
{
    public class OnMoveEventArgs : EventArgs
    {
        public GameEntity moveTarget;
        public Vector2 targetPosition;

        public OnMoveEventArgs(GameEntity moveTarget, Vector2 targetPosition)
        {
            this.moveTarget = moveTarget;
            this.targetPosition = targetPosition;
        }
    }
    public class OnDestroyEventArgs : EventArgs
    {
        public GameEntity destroyTarget;

        public OnDestroyEventArgs(GameEntity destroyTarget)
        {
            this.destroyTarget = destroyTarget;
        }
    }

    public class GameEntity
    {
        public event EventHandler<OnMoveEventArgs> OnMove;
        public event EventHandler<OnDestroyEventArgs> OnDestroy;
        public bool IsActive { get; set; }
        public string name;
        public char Symbol { get; private set; }
        private Vector2 _position;
        public Vector2 Position {
            get 
            {
                return _position;
            }
            set 
            {
                if (OnMove != null) OnMove.Invoke(this, new OnMoveEventArgs(this, value));
                _position = value;
            }
        }

        public GameEntity(string name, char symbol, Vector2 position)
        {
            this.name = name;
            Symbol = symbol;

            Position = position;
        }
        
        public void Destroy()
        {
            if (OnDestroy != null) OnDestroy.Invoke(this, new OnDestroyEventArgs(this));
        }

        public virtual void Update(int step)
        {

        }
        
     
    }
}
