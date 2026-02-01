using GGJ26.StateMachine;
using TMPro;
using UnityEngine;

using UnityEngine.InputSystem;
using StateMachineBehaviour = GGJ26.StateMachine.StateMachineBehaviour;
namespace GGJ26.Input
{
    public interface IPlayableCharacter 
    {
        
        public InputCollector GetInputCollector();
        public InputHandler GetInputHandler();
        public Rigidbody2D GetRigidbody2D();
        public bool IsAttacking();
        void SetAttacking(bool attacking);
        public Transform GetTransform();
        void SetInputHandler(InputHandler handler);
        float GetStartingYPosition();
        public float GetMoveSpeed();
        public float GetJumpForce();
        public PlayerSpriteUpdater GetPlayerSpriteUpdater();
        public void TakeDamage(int damage);
    }


    [RequireComponent(typeof(InputHandler))]
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(InputCollector))]
    public class MockPlayableCharacter : MonoBehaviour
    {
        private Rigidbody2D rb;
        private InputHandler inputHandler;
        private StateMachineBehaviour stateMachine;
        private InputCollector inputCollector;
        public bool isAttacking = false;

        public TextMeshProUGUI status;

        /*void Awake()
        {
            MatchManager.Instance.RegisterPlayer(this);
            rb = GetComponent<Rigidbody2D>();
            inputHandler = GetComponent<InputHandler>();
            inputCollector = GetComponent<InputCollector>();
            stateMachine = new StateMachineBehaviour();
            stateMachine.ChangeState(new Move(this, stateMachine));
        }*/

        void FixedUpdate()
        {

            stateMachine.Tick();
            status.text = stateMachine.current.GetType().Name;


        }

        public bool IsAttacking()
        {
            return isAttacking;
        }
        public void SetAttacking(bool attacking)
        {
            isAttacking = attacking;
        }

        

        public InputCollector GetInputCollector()
        {
            return inputCollector;
        }
        public InputHandler GetInputHandler()
        {
            return inputHandler;
        }
        public Rigidbody2D GetRigidbody2D()
        {
            return rb;
        }

        public Transform GetTransform()
        {
            return gameObject.transform;
        }

        public void SetInputHandler(InputHandler handler)
        {
            inputHandler = handler;
        }
    }

}




