using UnityEngine;

using UnityEngine.InputSystem;

namespace GGJ26.Input
{
    public class InputHandler : MonoBehaviour
    {
        public DpadRaw dpadRaw = new DpadRaw();
        public InputData current;

        private bool canRead = false;

        public void OnMove(InputAction.CallbackContext ctx)
        {
            if (!canRead)
                return;
            current.Movement = ctx.ReadValue<Vector2>();

        }

        public void OnAttack(InputAction.CallbackContext ctx)
        {
            if (!canRead)
                return;

            if (ctx.started)
                current.Attack1 = true;
            if (ctx.performed)
                current.Attack1 = false;
        }

        public void Update()
        {
            if (!canRead)
                return;

            Vector2 stickMove = current.Movement;

            Vector2 dpadMove = ((Vector2)dpadRaw.GetDPadRaw()).normalized;

            Vector2 combined = stickMove + dpadMove;

            if (combined.magnitude > 1f)
                combined.Normalize();

            current.Movement = combined;
        }

        public void SetRead(bool value)
        {
            canRead = value;
        }

    }

}




