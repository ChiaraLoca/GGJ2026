using UnityEngine;
using GGJ26.Input;
using NUnit.Framework;
using System.Collections.Generic;

public class InputCollector : MonoBehaviour
{
    InputHandler _inputHandler;
    private InputBuffer InputBuffer = new InputBuffer(60);
    private List<Motion> motions;

    private void setupMoves()
    {
        motions.Add(MotionCreator.get5A());
        motions.Sort((a, b) => a.priority.CompareTo(b.priority));
    }

    void Awake()
    {
        _inputHandler = GetComponent<InputHandler>();
        setupMoves();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        InputBuffer.Enqueue(_inputHandler.GetInputData());
    }

    public Motion GetMotion(bool isFacingRight)
    {
        foreach (Motion motion in motions)
        {
            if (InputBuffer.Matches(motion, isFacingRight))
            {
                return motion;
            }
        }

        return null;
    }

    public bool lastFrameHasButton()
    {
        return InputBuffer.getNewest().Attack1;
    }
    
}
