using UnityEngine;
namespace GGJ26.Input
{
    public class InputData
    {
        public Vector2 Movement { get; set; }
        public bool Attack1 { get; set; }

        public override string ToString()
        {
            return $"Movement: {Movement}, Attack1: {Attack1}";
        }
    }

    
}


