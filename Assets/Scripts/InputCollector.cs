using UnityEngine;
using GGJ26.Input;
public class InputCollector : MonoBehaviour
{
    InputHandler _inputHandler;

    void Awake()
    {
        _inputHandler = GetComponent<InputHandler>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
}
