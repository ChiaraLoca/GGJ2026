using System.Diagnostics;

namespace GGJ26.StateMachine
{
    public class StateMachineBehaviour
    { 
        public IState current;

        public void ChangeState(IState newState)
        {
            current?.OnExit();
            current = newState;
            current.OnEnter();  
        }

        public void Tick()
        {
            
            current.OnFrame();
        }

        public Motion GetMotion() 
        {
            if (current is Attack attack)
            {
                return attack.motion;
            }

            return null;
        }

        
    }

}