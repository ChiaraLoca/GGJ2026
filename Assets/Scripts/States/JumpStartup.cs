using GGJ26.Input;
using Unity.VisualScripting;
using UnityEngine;


namespace GGJ26.StateMachine
{
    public class JumpStartup : IState
    {
        private IPlayableCharacter character;
        private StateMachineBehaviour sm;
        private AttackInputHelper attackInputHandler;
        private InputCollector inputCollector;
        private Rigidbody2D rb;

        public JumpStartup(IPlayableCharacter character, StateMachineBehaviour sm)
        {
            this.character = character;
            this.sm = sm;
            attackInputHandler = new AttackInputHelper(character);
            inputCollector = character.GetInputCollector();
            rb = character.GetRigidbody2D();
        }

        public void OnEnter()
        {
            Debug.Log($"JumpStartup Enter");
            //Star JumpStartup animation
        }
        public void OnFrame()
        {
            IState newState = attackInputHandler.CheckAttackInput(sm);
            if (newState != null)
            {
                sm.ChangeState(newState);
                return;
            }

            // Movimento base
            InputData input = inputCollector.GetLastInputInBuffer();

            Vector2 move = Vector2.zero;

            if (input != null)
                move = NumpadHelper.NumpadToMove(input.Movement) * 1;

            rb.linearVelocity = move;
            sm.ChangeState(new Jump(character,sm));



        }
        public void OnExit()
        {
            Debug.Log($"JumpStartup Exit");
        }
    }
}