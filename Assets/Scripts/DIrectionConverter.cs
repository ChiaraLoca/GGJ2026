using UnityEngine;

public static class DirectionConverter
{
    private const float DeadZone = 0.2f;

    public static NumpadDirection ToNumpad(Vector2 input)
    {
        // Neutral
        if (input.magnitude < DeadZone)
            return NumpadDirection.Neutral;

        float angle = Mathf.Atan2(input.y, input.x) * Mathf.Rad2Deg;
        if (angle < 0) angle += 360f;

        // 8-way split (45° each)
        if (angle >= 337.5f || angle < 22.5f) return NumpadDirection.Right;      // 6
        if (angle < 67.5f) return NumpadDirection.UpRight;   // 9
        if (angle < 112.5f) return NumpadDirection.Up;        // 8
        if (angle < 157.5f) return NumpadDirection.UpLeft;    // 7
        if (angle < 202.5f) return NumpadDirection.Left;      // 4
        if (angle < 247.5f) return NumpadDirection.DownLeft;  // 1
        if (angle < 292.5f) return NumpadDirection.Down;      // 2
        return NumpadDirection.DownRight;                      // 3
    }
}
