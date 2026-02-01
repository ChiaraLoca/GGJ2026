using GGJ26.Input;
using GGJ26.StateMachine;
using UnityEngine;

using StateMachineBehaviour = GGJ26.StateMachine.StateMachineBehaviour;

public class Jump : IState
{
    private IPlayableCharacter character;
    private StateMachineBehaviour sm;
    private Rigidbody2D rb;

    private int frame;
    private int phase;

    // Frame data
    private int ascendFrames = 12;
    private int apexFrames = 4;
    private int fallMinFrames = 6;
    private int landingFrames = 5;

    private int landingStartFrame;

    public Jump(IPlayableCharacter character, StateMachineBehaviour sm)
    {
        this.character = character;
        this.sm = sm;
        rb = character.GetRigidbody2D();
    }

    public void OnEnter()
    {
        frame = 0;
        phase = -1;
        landingStartFrame = -1;
        Debug.Log("Jump Enter");

        

        character.GetPlayerSpriteUpdater().ChangeSprite("jump", 0);
    }

    public void OnFrame()
    {
        frame++;

        int newPhase;

        if (frame <= ascendFrames)
            newPhase = 0; // Ascend
        else if (frame <= ascendFrames + apexFrames)
            newPhase = 1; // Apex
        else if (frame <= ascendFrames + apexFrames + fallMinFrames)
            newPhase = 2; // Fall
        else
            newPhase = 3; // Landing

        if (newPhase != phase)
        {
            switch (newPhase)
            {
                case 0: Debug.Log("Jump Ascend"); break;
                case 1: Debug.Log("Jump Apex"); break;
                case 2: Debug.Log("Jump Fall"); break;
                case 3:
                    Debug.Log("Jump Landing");
                    landingStartFrame = frame;
                   
                    break;
            }
            phase = newPhase;
        }

        // Apex: blocca Y
        if (phase == 1)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
        }

        // Fall: gravità extra
        if (phase == 2)
        {
            character.GetPlayerSpriteUpdater().ChangeSprite("jump", 1);
            rb.linearVelocity += new Vector2(0, Physics2D.gravity.y * Time.deltaTime * 2);
        }

        // Landing recovery
        if (phase == 3 && frame >= landingStartFrame + landingFrames)
        {
            rb.linearVelocity = Vector2.zero;
           
     // Snap to ground
            sm.ChangeState(new Move(character, sm));
        }
    }

    public void OnExit()
    {
        Debug.Log("Jump Exit");
        rb.transform.position = new Vector2(rb.transform.position.x, character.GetStartingYPosition());

    }
}
