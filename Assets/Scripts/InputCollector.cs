using UnityEngine;
using GGJ26.Input;

using System.Collections.Generic;


public class InputCollector : MonoBehaviour
{
    InputHandler _inputHandler;
    private InputBuffer InputBuffer = new InputBuffer(60);
    private List<Motion> motions = new List<Motion>();

    //public TextMeshProUGUI buffer;


    private void setupMoves()
    {
        motions.Add(MotionCreator.get6A());
        motions.Add(MotionCreator.get2A());
        motions.Add(MotionCreator.get5A());
        motions.Add(MotionCreator.getSpecialMove());
        motions.Sort((a, b) => a.priority.CompareTo(b.priority));
    }

    void Awake()
    {
       
        setupMoves();
    }

    public void SetInputHandler(InputHandler inputHandler)
    {
        _inputHandler = inputHandler;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        InputData input = _inputHandler.GetInputData();

        

        InputBuffer.Enqueue(input);

       // Debug.Log(InputBuffer.print());
        //  buffer.text = InputBuffer.print();


    }

    public Motion GetMotion(bool isFacingRight, int playerSpecialPower)
    {
        Motion res = InputBuffer.FindBestMatch(motions.ToArray(), isFacingRight, playerSpecialPower);
        if (res != null)
        {
            // Consuma solo i frame utilizzati da questo motion
            InputBuffer.ConsumeMotion(res, isFacingRight);
        }
        return res;
    }

    private bool lastAttackState = false;

    public bool AttackPressedThisFrame()
    {
        InputData newest = InputBuffer.getNewest();
        if (newest == null)
            return false;

        // Edge detection: true solo se il tasto passa da false → true
        bool pressedThisFrame = newest.Attack1 && !lastAttackState;
        lastAttackState = newest.Attack1;
        return pressedThisFrame;
    }

    internal InputData GetLastInputInBuffer()
    {
        return InputBuffer.getNewest();
    }

    internal object Print()
    {
        return InputBuffer.print();

    }
}
