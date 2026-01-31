
using UnityEngine;

using UnityEngine.InputSystem;

namespace GGJ26.Input
{

    public class InputHandler : MonoBehaviour
    {
        public InputActionAsset actionsAsset;
        private DpadRaw dpadRaw;
     

        private bool canRead = true;

        private void Awake()
        {
            
            dpadRaw = new DpadRaw(actionsAsset);
        }
        
        public RawInputData current = new RawInputData();

        public InputData GetInputData()
        {
            return new InputData(DirectionConverter.ToNumpad(current.Movement), current.Attack1);
        }

        

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
            if (ctx.canceled)
                current.Attack1 = false;
        }

        public void Update()
        {
            if (!canRead)
                return;

            Vector2 updatedByOnMove = current.Movement;
            Vector2 dpadMove = ((Vector2)dpadRaw.GetDPadRaw()).normalized;
            Vector2 combined = updatedByOnMove + dpadMove;

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




