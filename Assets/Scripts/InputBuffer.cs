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

    /// <summary>
    /// Verifica se la motion è presente nel buffer con leniency per fighting game.
    /// Considera:
    /// - Input consecutivi possono essere saltati (leniency frames)
    /// - Gli input devono essere in ordine dal più vecchio al più recente
    /// - Permette frame "neutri" tra un input e l'altro
    /// </summary>
    public bool Matches(Motion motion, bool isFacingRight, int leniencyFrames = 8)
    {
        if (count == 0) return false;
        
        InputData[] requiredInputs = isFacingRight ? motion.Inputs : motion.FlippedInputs;
        
        if (requiredInputs == null || requiredInputs.Length == 0) return false;
        
        // Cerca partendo dal frame più recente andando indietro
        int inputIndex = requiredInputs.Length - 1; // Partiamo dall'ultimo input richiesto
        int framesSinceLastMatch = 0;
        int totalFramesSearched = 0;
        int maxSearchFrames = 60; // Non cercare oltre 60 frame
        
        foreach (var frame in NewestFirst())
        {
            if (totalFramesSearched >= maxSearchFrames) break;
            totalFramesSearched++;
            
            InputData required = requiredInputs[inputIndex];
            
            // Verifica match con leniency
            if (MatchesWithLeniency(frame, required))
            {
                inputIndex--;
                framesSinceLastMatch = 0;
                
                // Se abbiamo matchato tutti gli input, successo!
                if (inputIndex < 0)
                {
                    return true;
                }
            }
            else
            {
                framesSinceLastMatch++;
                
                // Se troppi frame senza match, reset e riprova
                if (framesSinceLastMatch > leniencyFrames)
                {
                    // Ricomincia dalla fine della sequenza
                    inputIndex = requiredInputs.Length - 1;
                    framesSinceLastMatch = 0;
                    
                    // Controlla se questo frame matcha il primo input della sequenza
                    if (MatchesWithLeniency(frame, requiredInputs[inputIndex]))
                    {
                        inputIndex--;
                        if (inputIndex < 0) return true;
                    }
                }
            }
        }
        
        return false;
    }
    
    /// <summary>
    /// Match con leniency per le direzioni (permette diagonali come sostituti)
    /// </summary>
    private bool MatchesWithLeniency(InputData actual, InputData required)
    {
        // Per gli attacchi, deve essere esatto
        if (required.Attack1 && !actual.Attack1) return false;
        
        // Se non richiede movimento specifico (neutral), qualsiasi direzione va bene per quel check
        if (required.Movement == NumpadDirection.Neutral)
        {
            return !required.Attack1 || actual.Attack1;
        }
        
        // Match esatto della direzione
        if (actual.Movement == required.Movement) return true;
        
        // Leniency per le direzioni cardinali (accetta anche le diagonali adiacenti)
        return IsDirectionLenient(actual.Movement, required.Movement);
    }
    
    /// <summary>
    /// Permette leniency sulle direzioni:
    /// - Down accetta anche DownLeft e DownRight
    /// - Right accetta anche UpRight e DownRight
    /// - etc.
    /// </summary>
    private bool IsDirectionLenient(NumpadDirection actual, NumpadDirection required)
    {
        switch (required)
        {
            case NumpadDirection.Down:
                return actual == NumpadDirection.DownLeft || actual == NumpadDirection.DownRight;
            case NumpadDirection.Up:
                return actual == NumpadDirection.UpLeft || actual == NumpadDirection.UpRight;
            case NumpadDirection.Left:
                return actual == NumpadDirection.DownLeft || actual == NumpadDirection.UpLeft;
            case NumpadDirection.Right:
                return actual == NumpadDirection.DownRight || actual == NumpadDirection.UpRight;
            case NumpadDirection.DownRight:
                return actual == NumpadDirection.Down || actual == NumpadDirection.Right;
            case NumpadDirection.DownLeft:
                return actual == NumpadDirection.Down || actual == NumpadDirection.Left;
            case NumpadDirection.UpRight:
                return actual == NumpadDirection.Up || actual == NumpadDirection.Right;
            case NumpadDirection.UpLeft:
                return actual == NumpadDirection.Up || actual == NumpadDirection.Left;
            default:
                return false;
        }
    }
    
    /// <summary>
    /// Cerca la motion con priorità più alta tra quelle matchate
    /// </summary>
    public Motion FindBestMatch(Motion[] motions, bool isFacingRight, int leniencyFrames = 8)
    {
        Motion bestMatch = null;
        int highestPriority = int.MinValue;
        
        foreach (var motion in motions)
        {
            if (Matches(motion, isFacingRight, leniencyFrames))
            {
                if (motion.priority > highestPriority)
                {
                    highestPriority = motion.priority;
                    bestMatch = motion;
                }
            }
        }
        
        return bestMatch;
    }
}
