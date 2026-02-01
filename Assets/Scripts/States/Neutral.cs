using GGJ26.Input;
using UnityEngine;


namespace GGJ26.StateMachine
{
    public class Neutral : IState
    {
        private IPlayableCharacter character;
        private StateMachineBehaviour sm;
        private AttackInputHelper attackInputHandler;

        public Neutral(IPlayableCharacter character, StateMachineBehaviour sm)
        {
            this.character = character;
            this.sm = sm;
            attackInputHandler = new AttackInputHelper(character);
        }

        public void OnEnter()
        {
            Debug.Log($"Neutral Enter");
            character.GetPlayerSpriteUpdater().ChangeSprite("idle", 0);
        }
        public void OnFrame()
        {
            IState newState = attackInputHandler.CheckAttackInput(sm);
            

            if (newState != null)
            {
                sm.ChangeState(newState);
                return;
            }
        }
        public void OnExit()
        {
            Debug.Log($"Neutral Exit");
        }
    }
}