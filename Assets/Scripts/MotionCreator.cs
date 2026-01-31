using GGJ26.Input;
using UnityEngine;

public class MotionCreator
{
    public static Motion get5A()
    {
        InputData[] inputs = { new InputData(NumpadDirection.Neutral, true) };
        InputData[] flippedInputs = { new InputData(NumpadDirection.Neutral, true) };
        return new Motion("punch",inputs, flippedInputs, 60, 20, 40, 0, 5, 12);
    }

    public static Motion get6A() {
        InputData[] inputs = { new InputData(NumpadDirection.Right, true) };
        InputData[] flippedInputs = { new InputData(NumpadDirection.Left, true) };
        return new Motion("kick", inputs, flippedInputs, 100, 50, 60, 0);
    }

    public static Motion get2A()
    {
        InputData[] inputs = { new InputData(NumpadDirection.Down, true) };
        InputData[] flippedInputs = { new InputData(NumpadDirection.Down, true) };
        return new Motion("lowHit",inputs, flippedInputs, 60, 20, 40, 0, 5, 12);
    }
}
