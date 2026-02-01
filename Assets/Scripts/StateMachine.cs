using GGJ26.Input;
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

        public void GotHit(Motion hitByMotion, IPlayableCharacter character)
        {
            if (current is Block block)
            {
                ChangeState(new BlockStun(character, this, hitByMotion));
            }
            else 
            {
                if(hitByMotion.knockDown)
                    ChangeState(new Down(character, this, hitByMotion));
                else
                    ChangeState(new Hit(character, this, hitByMotion));
            }
        }

        
    }

}