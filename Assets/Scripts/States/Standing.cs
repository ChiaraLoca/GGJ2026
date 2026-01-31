using GGJ26.Input;
using UnityEngine;


namespace GGJ26.StateMachine
{
    public class Standing : IState
    {
        private IPlayableCharacter character;
        private StateMachineBehaviour sm;
        
        private int maxFrame = 30;
        private int frame = 0;

        public Standing(IPlayableCharacter character, StateMachineBehaviour sm)
        {
            this.character = character;
            this.sm = sm;
            
        }

        public void OnEnter()
        {
            Debug.Log($"Standing Enter");
            character.GetPlayerSpriteUpdater().ChangeSprite("standing", 0);
        }
        public void OnFrame()
        {
            frame++;

            if (frame >= maxFrame)
            {
                sm.ChangeState(new Move(character, sm));
            }
        }
        public void OnExit()
        {
            Debug.Log($"Standing Hit");
        }
    }
}