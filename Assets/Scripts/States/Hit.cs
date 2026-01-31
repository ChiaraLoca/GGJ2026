using GGJ26.Input;
using UnityEngine;


namespace GGJ26.StateMachine
{
    public class Hit : IState
    {
        private IPlayableCharacter character;
        private StateMachineBehaviour sm;
       
        private int maxFrame = 30;
        private int frame = 0;

        public Hit(IPlayableCharacter character, StateMachineBehaviour sm)
        {
            this.character = character;
            this.sm = sm;
            
        }

        public void OnEnter()
        {
            Debug.Log($"Hit Enter");
            character.GetPlayerSpriteUpdater().ChangeSprite("hit", 0);
        }
        public void OnFrame()
        {
            frame++;

            if (frame>=maxFrame)
            {
                sm.ChangeState(new Standing(character, sm));
            }
        }
        public void OnExit()
        {
            Debug.Log($"Crouch Hit");
        }
    }
}