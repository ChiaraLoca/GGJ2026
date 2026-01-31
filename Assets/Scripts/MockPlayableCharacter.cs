using GGJ26.StateMachine;
using UnityEngine;

using UnityEngine.InputSystem;
using StateMachineBehaviour = GGJ26.StateMachine.StateMachineBehaviour;
namespace GGJ26.Input
{
    public interface IPlayableCharacter 
    {
        public bool IsFacingRight();
        public InputHandler GetInputHandler();
        public Rigidbody2D GetRigidbody2D();
    }


    [RequireComponent(typeof(InputHandler))]
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(Rigidbody2D))]

    public class MockPlayableCharacter : MonoBehaviour, IPlayableCharacter
    {
        private Rigidbody2D rb;
        private InputHandler inputHandler;
        private StateMachineBehaviour stateMachine;

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            inputHandler = GetComponent<InputHandler>();
            stateMachine = new StateMachineBehaviour();
            stateMachine.ChangeState(new Idle(this, stateMachine));
        }

        void FixedUpdate()
        {
            stateMachine.Tick();
            
        }



        public bool IsFacingRight()
        {
            return true;
        }

        public InputHandler GetInputHandler()
        { 
            return inputHandler;
        }
        public Rigidbody2D GetRigidbody2D()
        { 
            return rb;
        }
    }

}




