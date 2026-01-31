using GGJ26.Input;
using UnityEngine;


namespace GGJ26.StateMachine
{
    
    

    public class Idle : BaseState
    {

        private InputHandler inputHandler;

        public Idle(IPlayableCharacter playableCharacter, StateMachineBehaviour stateMachineBehaviour)
        {
            StateMachineBehaviour = stateMachineBehaviour;
            PlayableCharacter = playableCharacter;
            inputHandler = PlayableCharacter.GetInputHandler();
        }

        public override void OnFrame()
        {
            base.OnFrame();

            if (inputHandler.current.Attack1)
            {

            }
            else//move
            {
                StateMachineBehaviour.ChangeState(new Move(PlayableCharacter, StateMachineBehaviour));
            }


        }
    }

    public class Attack : BaseState {

        public string motion;

        public Attack(string motion,IPlayableCharacter playableCharacter, StateMachineBehaviour stateMachineBehaviour) {

            StateMachineBehaviour = stateMachineBehaviour;
            PlayableCharacter = playableCharacter;
            this.motion = motion;
            
        }
        public override void OnFrame()
        {
            base.OnFrame();

            if (Frame < 10)
            {
                //startup
                Debug.Log($"startup " + motion);
            }
            else if (Frame < 30)
            {
                //active
                Debug.Log($"Active "+motion);
            }
            else if (Frame < 100)
            {
                //Recovery
                Debug.Log($"Recovery " + motion);
            }
            else
            {
                //exit
                StateMachineBehaviour.ChangeState(new Idle(PlayableCharacter, StateMachineBehaviour));
            }

            

                
        }


    }

    public class Move : BaseState
    {

        private InputHandler inputHandler;
        private Rigidbody2D rigidbody2D;

        public Move(IPlayableCharacter playableCharacter, StateMachineBehaviour stateMachineBehaviour)
        {
            StateMachineBehaviour = stateMachineBehaviour;
            PlayableCharacter = playableCharacter;
            inputHandler = PlayableCharacter.GetInputHandler();
            rigidbody2D = playableCharacter.GetRigidbody2D();

        }

        public override void OnFrame()
        {
            base.OnFrame();
            MovePlayer(inputHandler.current);
            StateMachineBehaviour.ChangeState(new Idle(PlayableCharacter, StateMachineBehaviour));

        }

        void MovePlayer(RawInputData input)
        {
            
                Vector2 velocity = input.Movement;
                rigidbody2D.linearVelocity = velocity;
            
 
        }
    }

}