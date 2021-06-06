using System;
using System.Collections.Generic;
using System.Text;

namespace TextRPG {
    public struct Vector2
    {
        public readonly int x;
        public readonly int y;

        public static Vector2 operator -(Vector2 a) => new Vector2(-a.x, -a.y);
        public static Vector2 operator -(Vector2 a, Vector2 b) => new Vector2(a.x - b.x, a.y - b.y);
        public static Vector2 operator +(Vector2 a, Vector2 b) => new Vector2(a.x + b.x, a.y + b.y);
        public static Vector2 operator *(Vector2 a, int b) => new Vector2(a.x * b, a.y * b);
        public static Vector2 operator /(Vector2 a, int b) => new Vector2(a.y/b, a.y/b);

        public static bool operator ==(Vector2 a, Vector2 b) => (a.x == b.x) && (a.y == b.y);

        public static bool operator !=(Vector2 a, Vector2 b) => (a.x != b.x) || (a.y != b.y);

        public static Vector2 Up { get { return new Vector2(0, -1); } }
        public static Vector2 Down{ get {return new Vector2(0, 1); } }
        public static Vector2 Left { get { return new Vector2(-1, 0); } }
        public static Vector2 Right { get { return new Vector2(1, 0); } }

        public static Vector2 One { get { return new Vector2(1, 1); } }

        public static Vector2 Zero { get { return new Vector2(0, 0); } }

        public static Vector2 None { get { return new Vector2(-int.MaxValue, -int.MaxValue); } }

        public Vector2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("Vector2({0}, {1})", x, y);
        }
    }
}
