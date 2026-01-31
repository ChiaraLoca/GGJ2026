using UnityEngine;
using UnityEngine.InputSystem;

namespace GGJ26.Input
{
    public class DpadRaw 
    {
        private InputActionAsset actionsAsset;

        private InputAction dUp;
        private InputAction dDown;
        private InputAction dLeft;
        private InputAction dRight;

        public DpadRaw(InputActionAsset actionsAsset)
        {


            this.actionsAsset = actionsAsset;

            // Trova le action
            dUp = actionsAsset.FindAction("DpUp");
            dDown = actionsAsset.FindAction("DpDown");
            dLeft = actionsAsset.FindAction("DpLeft");
            dRight = actionsAsset.FindAction("DpRight");

            // Abilita le action
            dUp.Enable();
            dDown.Enable();
            dLeft.Enable();
            dRight.Enable();
        }

        // Chiamare manualmente quando non serve più
        public void Dispose()
        {
            dUp.Disable();
            dDown.Disable();
            dLeft.Disable();
            dRight.Disable();
        }

        public Vector2Int GetDPadRaw()
        {
            int x = 0;
            int y = 0;

            if (dRight.IsPressed()) x++;
            if (dLeft.IsPressed()) x--;
            if (dUp.IsPressed()) y++;
            if (dDown.IsPressed()) y--;

            return new Vector2Int(x, y);
        }
    }
}