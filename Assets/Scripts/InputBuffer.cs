using GGJ26.Input;
using System;
using System.Collections.Generic;
using UnityEngine;
public class InputBuffer
{
    private readonly InputData[] buffer;
    private int head; // oldest
    private int tail; // next write
    private int count;

    public int Count => count;
    public int Capacity => buffer.Length;

    public InputBuffer(int capacity)
    {
        buffer = new InputData[capacity];
        head = 0;
        tail = 0;
        count = 0;
    }

    /// <summary>
    /// Enqueue newest input frame.
    /// Automatically overwrites the oldest when full.
    /// </summary>
    public void Enqueue(InputData frame)
    {
        buffer[tail] = frame;
        tail = (tail + 1) % buffer.Length;

        if (count == buffer.Length)
        {
            // overwrite oldest
            head = (head + 1) % buffer.Length;
        }
        else
        {
            count++;
        }
    }

    /// <summary>
    /// Access element by age (0 = oldest, Count-1 = newest)
    /// </summary>
    public InputData Get(int index)
    {
        if (index < 0 || index >= count)
            throw new ArgumentOutOfRangeException();

        return buffer[(head + index) % buffer.Length];
    }

    /// <summary>
    /// Iterate from newest to oldest (very useful for command detection)
    /// </summary>
    public IEnumerable<InputData> NewestFirst()
    {
        for (int i = 0; i < count; i++)
        {
            int idx = (tail - 1 - i + buffer.Length) % buffer.Length;
            yield return buffer[idx];
        }
    }

    public InputData getNewest()
    {
        return buffer[(tail - 1 + buffer.Length) % buffer.Length];
    }

    public void Clear()
    {
        head = tail = count = 0;
    }

    public bool Matches(Motion motion, bool isFacingRight)
    {
        InputData[] inputData = isFacingRight ? motion.Inputs : motion.FlippedInputs;
        
        foreach (var input in inputData)
        {
            bool completeMatch = false;
            bool partialMatch = false;
            int bufferIndex = 0;
            foreach (var frame in NewestFirst())
            {
                bufferIndex++;
                if (frame.matches(input))
                {
                    partialMatch = true;
                    continue;
                }
                
            }
        }
    }
}
