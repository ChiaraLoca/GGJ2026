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
    public float knockBack { get; set; }
    public int priority { get; set; }
    public string name { get; set; }
    public int recoveryFrameSwitch { get; set; }
    
    public int specialRequiredPower { get; set; } = 0;
    
    // Numero di sprite per ogni fase (per supportare animazioni multi-frame)
    public int startupSpriteCount { get; set; } = 1;
    public int activeSpriteCount { get; set; } = 1;
    public int recoverySpriteCount { get; set; } = 1;
    
    // Proiettile
    public bool spawnsProjectile { get; set; } = false;
    public int projectileSpawnFrame { get; set; } = 0; // Frame in cui spawnare il proiettile
    public int projectileDamage { get; set; } = 20;

    public bool isGrab { get; set; } = false;
    public bool isLauncher { get; set; } = false;

    // Costruttore originale (retrocompatibile)
    public Motion(string name,InputData[] inputs, InputData[] flippedInputs, int totalFrames, int startupEnd, int activeEnd, int priority, int damage, int hitStunFrames, int blockStunFrames, bool knockDown, float knockBack,int recoveryFrameSwitch, int specialRequiredPower, int cancelWindowStart = -1, int cancelWindowEnd = -1) 
        : this(name, inputs, flippedInputs, totalFrames, startupEnd, activeEnd, priority, damage, hitStunFrames, blockStunFrames, knockDown, knockBack, recoveryFrameSwitch, specialRequiredPower, 1, 1, 1, cancelWindowStart, cancelWindowEnd) { }

    // Nuovo costruttore con supporto multi-frame
    public Motion(string name, InputData[] inputs, InputData[] flippedInputs, int totalFrames, int startupEnd, int activeEnd, int priority, int damage, int hitStunFrames, int blockStunFrames, bool knockDown, float knockBack, int recoveryFrameSwitch, int specialRequiredPower, int startupSpriteCount, int activeSpriteCount, int recoverySpriteCount, int cancelWindowStart = -1, int cancelWindowEnd = -1) {         
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
        this.knockBack = knockBack;
        this.cancelWindowStart = cancelWindowStart;
        this.cancelWindowEnd = cancelWindowEnd;
        this.recoveryFrameSwitch = recoveryFrameSwitch;
        this.specialRequiredPower = specialRequiredPower;
        this.startupSpriteCount = startupSpriteCount;
        this.activeSpriteCount = activeSpriteCount;
        this.recoverySpriteCount = recoverySpriteCount;
    }

    public bool isCancellable(int frame) {
        if (cancelWindowStart == -1 || cancelWindowEnd == -1) return false;
        return frame >= cancelWindowStart && frame <= cancelWindowEnd;
    }
}
