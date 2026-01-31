using GGJ26.Input;
using System.Linq;
using UnityEngine;


namespace GGJ26.StateMachine
{
    public class Attack : BaseState {

        public Motion motion;
        public string nameMotion;

        public Attack(Motion motion, IPlayableCharacter playableCharacter, StateMachineBehaviour stateMachineBehaviour) {

            StateMachineBehaviour = stateMachineBehaviour;
            PlayableCharacter = playableCharacter;
            this.motion = motion;
            nameMotion = string.Join(",", motion.Inputs.Select(x => x.ToString()));


        }
        public override void OnFrame()
        {
            base.OnFrame();

            if (Frame < motion.startupEnd)
            {
                Debug.Log($"startup " + nameMotion);
            }
            else if (Frame < motion.activeEnd)
            {
                Debug.Log($"Active "+ nameMotion);
            }
            else if (Frame < motion.totalFrames)
            {
                Debug.Log($"Recovery " + nameMotion);
            }
            else
            {
                StateMachineBehaviour.ChangeState(new Move(PlayableCharacter, StateMachineBehaviour));
            }
                   
            

                
        }
    }
}