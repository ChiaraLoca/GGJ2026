using UnityEngine;
using GGJ26.Input;
using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using System.Linq;

public class InputCollector : MonoBehaviour
{
    InputHandler _inputHandler;
    private InputBuffer InputBuffer = new InputBuffer(60);
    private List<Motion> motions = new List<Motion>();

    public TextMeshProUGUI buffer;


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
        buffer.text = InputBuffer.print();


    }

    public Motion GetMotion(bool isFacingRight)
    {
        InputBuffer.Clear();
        return MotionCreator.get5A();

        /*foreach (Motion motion in motions)
        {
            if (InputBuffer.MatchesMotion(motion, isFacingRight))
            {
                return motion;
            }
        }*/
    }

    public bool lastFrameHasButton()
    {
        if (InputBuffer.getNewest()==null)
            return false;
        return InputBuffer.getNewest().Attack1;
    }

    internal InputData GetLastInputInBuffer()
    {
        return InputBuffer.getNewest();
    }
}
