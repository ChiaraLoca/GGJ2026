using GGJ26.Input;
using UnityEngine;


namespace GGJ26.StateMachine
{
    public class Block : IState
    {
        private IPlayableCharacter character;
        private StateMachineBehaviour sm;
        private AttackInputHelper attackInputHandler;

        public Block(IPlayableCharacter character, StateMachineBehaviour sm)
        {
            this.character = character;
            this.sm = sm;
            attackInputHandler = new AttackInputHelper(character);
        }

        public void OnEnter()
        {
            Debug.Log($"Block Enter");
            character.GetPlayerSpriteUpdater().ChangeSprite("block", 0);
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
            Debug.Log($"Block Exit");
        }
    }
}