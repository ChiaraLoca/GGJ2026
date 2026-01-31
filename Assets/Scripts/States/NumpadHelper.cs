using UnityEngine;



namespace GGJ26.StateMachine
{
    public class NumpadHelper
    {
        public static Vector2 NumpadToMove(NumpadDirection dir)
        {
            switch (dir)
            {
                case NumpadDirection.Up: return Vector2.up;
                case NumpadDirection.Down: return Vector2.down;
                case NumpadDirection.Left: return Vector2.left;
                case NumpadDirection.Right: return Vector2.right;
                case NumpadDirection.UpLeft: return new Vector2(-1, 1).normalized;
                case NumpadDirection.UpRight: return new Vector2(1, 1).normalized;
                case NumpadDirection.DownLeft: return new Vector2(-1, -1).normalized;
                case NumpadDirection.DownRight: return new Vector2(1, -1).normalized;
                case NumpadDirection.Neutral:
                default: return Vector2.zero;
            }
        }
    }
}