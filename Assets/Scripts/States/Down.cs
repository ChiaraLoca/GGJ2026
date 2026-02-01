using GGJ26.Input;
using UnityEngine;


namespace GGJ26.StateMachine
{
    public class Down : IState
    {
        private IPlayableCharacter character;
        private StateMachineBehaviour sm;
       
        private int maxFrame = 30;
        private int frame = 0;
        private Motion hitByMotion;
        public Down(IPlayableCharacter character, StateMachineBehaviour sm,Motion hitByMotion)
        {
            this.character = character;
            this.sm = sm;

            this.hitByMotion = hitByMotion;
        }

        public void OnEnter()
        {
            Debug.Log($"Down Enter");
            character.GetPlayerSpriteUpdater().ChangeSprite("down", 0);
        }
        public void OnFrame()
        {
            frame++;

            if (frame >= hitByMotion.hitStunFrames)
            {
                sm.ChangeState(new Standing(character, sm));
            }
        }
        public void OnExit()
        {
            Debug.Log($"Down Hit");
        }
    }
}