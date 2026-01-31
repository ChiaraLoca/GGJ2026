using UnityEngine;
using UnityEngine.InputSystem;

namespace GGJ26.Input
{
    public class InputHandler : MonoBehaviour
    {
        public PlayerInput playerInput;
        private InputAction moveAction;
        private InputAction attackAction;

        public RawInputData current = new RawInputData();

        private void Start()
        {
            

            // QUESTE sono per-player
            moveAction = playerInput.actions["Move"];
            attackAction = playerInput.actions["Attack"];
        }

        private void FixedUpdate()
        {
            current.Movement = moveAction.ReadValue<Vector2>();
            current.Attack1 = attackAction.IsPressed();
        }

        public InputData GetInputData()
        {
            return new InputData(
                DirectionConverter.ToNumpad(current.Movement),
                current.Attack1
            );
        }
    }
}
