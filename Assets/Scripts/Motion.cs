using GGJ26.Input;
using UnityEngine;

public class Motion
{
    public InputData[] Inputs { get; set; }
    public InputData[] FlippedInputs { get; set; }
    int totalFrames;
    int startupEnd;
    int activeEnd;
    int cancelWindowStart;
    int cancelWindowEnd;
    public int priority { get; set; }


    public Motion(InputData[] inputs, InputData[] flippedInputs, int totalFrames, int startupEnd, int activeEnd, int priority, int cancelWindowStart = -1, int cancelWindowEnd = -1) {         
        this.Inputs = inputs;
        this.FlippedInputs = flippedInputs;
        this.totalFrames = totalFrames;
        this.startupEnd = startupEnd;
        this.activeEnd = activeEnd;
        this.priority = priority; 
        this.cancelWindowStart = cancelWindowStart;
        this.cancelWindowEnd = cancelWindowEnd;
    }

    public bool isCancellable(int frame) {
        if (cancelWindowStart == -1 || cancelWindowEnd == -1) return false;
        return frame >= cancelWindowStart && frame <= cancelWindowEnd;
    }
}
