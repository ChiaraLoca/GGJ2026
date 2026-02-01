using GGJ26.Input;
using UnityEngine;

public class MotionCreator
{
    public static Motion get5A()
    {
        InputData[] inputs = { new InputData(NumpadDirection.Neutral, true) };
        InputData[] flippedInputs = { new InputData(NumpadDirection.Neutral, true) };
        return new Motion("punch",inputs, flippedInputs, 18, 10, 12, 0, 5, 10, 3, false, 1f,16,0, 5, 12);
    }

    public static Motion get6A() {
        InputData[] inputs = { new InputData(NumpadDirection.Right, true) };
        InputData[] flippedInputs = { new InputData(NumpadDirection.Left, true) };
        return new Motion("kick", inputs, flippedInputs, 36, 18, 20, 0, 15, 25, 3, true, 2f,32, 0);
    }

    public static Motion get2A()
    {
        InputData[] inputs = { new InputData(NumpadDirection.Down, true) };
        InputData[] flippedInputs = { new InputData(NumpadDirection.Down, true) };
        return new Motion("lowHit",inputs, flippedInputs, 20, 12, 14, 0, 5, 5, 3, false, 1f,17, 0, 5, 12);
    }


    public static Motion getSpecialMove()
    {
        InputData[] inputs = {
            new InputData(NumpadDirection.Down, false),
            new InputData(NumpadDirection.DownRight, false),
            new InputData(NumpadDirection.Right, true),
        };
        InputData[] flippedInputs = {
            new InputData(NumpadDirection.Down, false),
            new InputData(NumpadDirection.DownLeft, false),
            new InputData(NumpadDirection.Left, true),
        };
        // Esempio: 2 sprite startup, 3 sprite active, 2 sprite recovery = 7 sprite totali
        // Parametri: name, inputs, flippedInputs, totalFrames, startupEnd, activeEnd, priority, 
        //            damage, hitStunFrames, blockStunFrames, knockDown, knockBack, recoveryFrameSwitch, 
        //            specialRequiredPower, startupSpriteCount, activeSpriteCount, recoverySpriteCount, cancelWindowStart, cancelWindowEnd
        return new Motion("special", inputs, flippedInputs, 
            totalFrames: 60, 
            startupEnd: 15, 
            activeEnd: 36, 
            priority: 2, 
            damage: 40, 
            hitStunFrames: 10, 
            blockStunFrames: 5, 
            knockDown: true, 
            knockBack: 2f, 
            recoveryFrameSwitch: 50, 
            specialRequiredPower: 100,
            startupSpriteCount: 2,   // sprite 0, 1
            activeSpriteCount: 4,    // sprite 2, 3, 4, 5
            recoverySpriteCount: 2,  // sprite 6, 7
            cancelWindowStart: 5, 
            cancelWindowEnd: 12);
    }
}
