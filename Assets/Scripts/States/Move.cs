using GGJ26.Input;
using UnityEngine;
using UnityEngine.Windows;


namespace GGJ26.StateMachine
{

    public class Move : BaseState
    {

        private InputCollector inputCollector;
        private InputHandler inputHandler;
        private Rigidbody2D rigidbody2D;

        public Move(IPlayableCharacter playableCharacter, StateMachineBehaviour stateMachineBehaviour)
        {
            StateMachineBehaviour = stateMachineBehaviour;
            PlayableCharacter = playableCharacter;
            inputCollector = PlayableCharacter.GetInputCollector();
            rigidbody2D = playableCharacter.GetRigidbody2D();
            inputHandler = PlayableCharacter.GetInputHandler();

        }
        public override void OnEnter()
        {
            base.OnEnter();
            inputHandler.SetRead(true);
        }

        public override void OnExit()
        {
            base.OnExit();
            inputHandler.SetRead(false);
        }

        public override void OnFrame()
        {
            base.OnFrame();

            if (inputCollector.lastFrameHasButton())
            {
                Motion motion = inputCollector.GetMotion(PlayableCharacter.IsFacingRight());
                StateMachineBehaviour.ChangeState(new Attack(motion, PlayableCharacter, StateMachineBehaviour));
            }
            else
            {
                InputData input = inputCollector.GetLastInputInBuffer();
                if (input != null)
                {
                    Debug.Log(input);
                    Vector2 move = NumpadToMove(input.Movement);
                    rigidbody2D.linearVelocity = move;
                }
            }

        }

        

        public Vector2 NumpadToMove(NumpadDirection direction)
        {
            switch (direction)
            {
                case NumpadDirection.Up: return new Vector2(0, 1);
                case NumpadDirection.Down: return new Vector2(0, -1);
                case NumpadDirection.Left: return new Vector2(-1, 0);
                case NumpadDirection.Right: return new Vector2(1, 0);

                case NumpadDirection.UpLeft: return new Vector2(-1, 1).normalized;
                case NumpadDirection.UpRight: return new Vector2(1, 1).normalized;
                case NumpadDirection.DownLeft: return new Vector2(-1, -1).normalized;
                case NumpadDirection.DownRight: return new Vector2(1, -1).normalized;

                case NumpadDirection.Neutral:
                default:
                    return Vector2.zero;
            }
        }
    }

}