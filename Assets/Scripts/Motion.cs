using GGJ26.Input;
using UnityEngine;

public class Motion
{
    public InputData[] Inputs;
    public InputData[] FlippedInputs;
    public int totalFrames;
    public int startupEnd;
    public int activeEnd;
    public int cancelWindowStart;
    public int cancelWindowEnd;
    public int damage { get; set; }
    public int hitStunFrames { get; set; }
    public int blockStunFrames { get; set; }
    public bool knockDown { get; set; }
    public int priority { get; set; }
    public string name { get; set; }


    public Motion(string name,InputData[] inputs, InputData[] flippedInputs, int totalFrames, int startupEnd, int activeEnd, int priority, int damage, int hitStunFrames, int blockStunFrames, bool knockDown, int cancelWindowStart = -1, int cancelWindowEnd = -1) {         
        this.name = name;
        this.Inputs = inputs;
        this.FlippedInputs = flippedInputs;
        this.totalFrames = totalFrames;
        this.startupEnd = startupEnd;
        this.activeEnd = activeEnd;
        this.priority = priority; 
        this.damage = damage;
        this.hitStunFrames = hitStunFrames; 
        this.blockStunFrames = blockStunFrames; 
        this.knockDown = knockDown;
        this.cancelWindowStart = cancelWindowStart;
        this.cancelWindowEnd = cancelWindowEnd;
    }

    public bool isCancellable(int frame) {
        if (cancelWindowStart == -1 || cancelWindowEnd == -1) return false;
        return frame >= cancelWindowStart && frame <= cancelWindowEnd;
    }
}
