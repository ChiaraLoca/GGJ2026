using GGJ26.Input;
using UnityEngine;

public class MotionCreator
{
    public static Motion get5A()
    {
        InputData[] inputs = { new InputData(NumpadDirection.Neutral, true) };
        InputData[] flippedInputs = { new InputData(NumpadDirection.Neutral, true) };
        return new Motion("punch",inputs, flippedInputs, 18, 10, 12, 0, 5, 10, 3, false, 1f,16, 5, 12);
    }

    public static Motion get6A() {
        InputData[] inputs = { new InputData(NumpadDirection.Right, true) };
        InputData[] flippedInputs = { new InputData(NumpadDirection.Left, true) };
        return new Motion("kick", inputs, flippedInputs, 36, 18, 20, 0, 15, 25, 3, true, 2f,32);
    }

    public static Motion get2A()
    {
        InputData[] inputs = { new InputData(NumpadDirection.Down, true) };
        InputData[] flippedInputs = { new InputData(NumpadDirection.Down, true) };
        return new Motion("lowHit",inputs, flippedInputs, 20, 12, 14, 0, 5, 12, 3, true, 1f,17, 5, 12);
    }
}
