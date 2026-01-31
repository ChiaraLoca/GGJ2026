using GGJ26.Input;
using UnityEngine;


namespace GGJ26.StateMachine
{
    public class LowBlock : IState
    {
        private IPlayableCharacter character;
        private StateMachineBehaviour sm;
        private AttackInputHelper attackInputHandler;

        public LowBlock(IPlayableCharacter character, StateMachineBehaviour sm)
        {
            this.character = character;
            this.sm = sm;
            attackInputHandler = new AttackInputHelper(character);
        }

        public void OnEnter()
        {
            Debug.Log($"LowBlock Enter");
            
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
            Debug.Log($"LowBlock Exit");
            character.GetPlayerSpriteUpdater().ChangeSprite("idle", 0);
        }
    }


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

    public class Crouch : IState
    {
        private IPlayableCharacter character;
        private StateMachineBehaviour sm;
        private AttackInputHelper attackInputHandler;

        public Crouch(IPlayableCharacter character, StateMachineBehaviour sm)
        {
            this.character = character;
            this.sm = sm;
            attackInputHandler = new AttackInputHelper(character);
        }

        public void OnEnter()
        {
            Debug.Log($"Crouch Enter");
            character.GetPlayerSpriteUpdater().ChangeSprite("crouch", 0);
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
            Debug.Log($"Crouch Exit");
        }
    }
}