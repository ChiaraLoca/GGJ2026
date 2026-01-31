using UnityEngine;
namespace GGJ26.Input
{
    public class InputData
    {
        public NumpadDirection Movement { get; set; }
        public bool Attack1 { get; set; }

        public InputData(NumpadDirection movement,  bool attack1)
        {
            Movement = movement;
            Attack1 = attack1;
        }

    

        public override string ToString()
        {
            return $"Movement: {Movement}, Attack1: {Attack1}";
        }

        public bool matches(InputData other)
        {
            return Movement == other.Movement && Attack1 == other.Attack1;
        }
    }

    
}


