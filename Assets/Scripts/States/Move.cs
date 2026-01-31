using GGJ26.Input;
using UnityEngine;
using UnityEngine.Windows;


namespace GGJ26.StateMachine
{
    public class Move : IState
    {
        private IPlayableCharacter character;
        private StateMachineBehaviour sm;
        private Rigidbody2D rb;
        private InputCollector inputCollector;

        public Move(IPlayableCharacter character, StateMachineBehaviour sm)
        {
            this.character = character;
            this.sm = sm;
            this.rb = character.GetRigidbody2D();
            this.inputCollector = character.GetInputCollector();
        }

        public void OnEnter()
        {
            Debug.Log($"Move Enter:");
            //character.SetAnimation("Idle");
        }

        public void OnFrame()
        {
            // Controllo motion da attacco
            Motion motion = inputCollector.GetMotion(character.IsFacingRight());
            if (motion != null && !(sm.current is Attack) && !character.IsAttacking())
            {
                sm.ChangeState(new Attack(motion, character, sm));
                return;
            }

            // Movimento base
            InputData input = inputCollector.GetLastInputInBuffer();
            Vector2 move = Vector2.zero;

            if (input != null)
                move = NumpadToMove(input.Movement) * 1;

            rb.linearVelocity = move;

            // Animazione
            //character.SetAnimation(move == Vector2.zero ? "Idle" : "Walk");
        }

        public void OnExit()
        {
            Debug.Log($"Move Exit:");
            rb.linearVelocity = Vector2.zero;
        }

        private Vector2 NumpadToMove(NumpadDirection dir)
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