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
        private AttackInputHelper attackInputHandler;

        public Move(IPlayableCharacter character, StateMachineBehaviour sm)
        {
            this.character = character;
            this.sm = sm;
            this.rb = character.GetRigidbody2D();
            this.inputCollector = character.GetInputCollector();
            attackInputHandler = new AttackInputHelper(character);
        }

        public void OnEnter()
        {
            Debug.Log($"Move Enter:");
            character.GetPlayerSpriteUpdater().ChangeSprite("movement", 0);
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
                move = NumpadHelper.NumpadToMove(input.Movement) * character.GetMoveSpeed();

            rb.linearVelocity = move;

            // Animazione
            //character.SetAnimation(move == Vector2.zero ? "Idle" : "Walk");
        }

        public void OnExit()
        {
            Debug.Log($"Move Exit:");
            rb.linearVelocity = Vector2.zero;
        }

        
    }
}