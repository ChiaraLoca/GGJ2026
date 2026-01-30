using UnityEngine;

using UnityEngine.InputSystem;
namespace GGJ26.Input
{
    public class DpadRaw
{
    public InputAction dUp;
    public InputAction dDown;
    public InputAction dLeft;
    public InputAction dRight;

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