
using UnityEngine;

using UnityEngine.InputSystem;

namespace GGJ26.Input
{

    public class InputHandler : MonoBehaviour
    {
        public InputActionAsset actionsAsset;
        private DpadRaw dpadRaw;
        public RawInputData current = new RawInputData();

        private void Awake()
        {
            Application.targetFrameRate = 60;
            dpadRaw = new DpadRaw(actionsAsset);
            
        }
        
        

        public InputData GetInputData()
        {
            return new InputData(DirectionConverter.ToNumpad(current.Movement), current.Attack1);
        }

        

        public void OnMove(InputAction.CallbackContext ctx)
        {
            
            current.Movement = ctx.ReadValue<Vector2>();


        }

        public void OnAttack(InputAction.CallbackContext ctx)
        {
           
            if (ctx.started)
                current.Attack1 = true;
            if (ctx.canceled)
                current.Attack1 = false;
        }

        public void Update()
        {
            
            Vector2 updatedByOnMove = current.Movement;
            Vector2 dpadMove = ((Vector2)dpadRaw.GetDPadRaw()).normalized;
            Vector2 combined = updatedByOnMove + dpadMove;

            if (combined.magnitude > 1f)
                combined.Normalize();

            current.Movement = combined;

        }

       

    }

}




