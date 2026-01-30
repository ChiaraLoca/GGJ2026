using UnityEngine;

using UnityEngine.InputSystem;

namespace GGJ26.Input
{
    [RequireComponent(typeof(InputHandler))]
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(Rigidbody2D))]

    public class MockPlayableCharacter : MonoBehaviour
    {
        private Rigidbody2D rb;
        private InputHandler inputHandler;

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            inputHandler = GetComponent<InputHandler>();
        }

        void FixedUpdate()
        {
            Move(inputHandler.current);
        }

        void Move(InputData input)
        {
            Vector2 velocity = input.Movement * 5;
            rb.linearVelocity = velocity;

            if (input.Attack1)
            {
                Debug.Log("ATTACK!");
            }
        }
    }

}




