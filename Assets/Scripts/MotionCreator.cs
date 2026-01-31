using GGJ26.Input;
using UnityEngine;

public class MotionCreator
{
    public static Motion get5A()
    {
        InputData[] inputs = { new InputData(NumpadDirection.Neutral, true) };
        InputData[] flippedInputs = { new InputData(NumpadDirection.Neutral, true) };
        return new Motion(inputs, flippedInputs, 60, 20, 40, 0, 5, 12);
    }
}
