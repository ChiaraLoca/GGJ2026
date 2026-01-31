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

    //public TextMeshProUGUI buffer;


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
        //  buffer.text = InputBuffer.print();


    }

    public Motion GetMotion(bool isFacingRight)
    {
        Motion res = InputBuffer.FindBestMatch(motions.ToArray(), isFacingRight);
        if (res != null)
        {
            // Consuma solo i frame utilizzati da questo motion
            InputBuffer.ConsumeMotion(res, isFacingRight);
        }
        return res;
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
