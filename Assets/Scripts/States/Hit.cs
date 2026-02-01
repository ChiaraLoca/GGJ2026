using GGJ26.Input;
using UnityEngine;


namespace GGJ26.StateMachine
{
    public class BlockStun : IState
    {
        private IPlayableCharacter character;
        private StateMachineBehaviour sm;

        private int maxFrame = 30;
        private int frame = 0;
        private Motion hitByMotion;
        public BlockStun(IPlayableCharacter character, StateMachineBehaviour sm, Motion hitByMotion)
        {
            this.character = character;
            this.sm = sm;
            this.hitByMotion = hitByMotion;

        }

        public void OnEnter()
        {
            Debug.Log($"BlockStun Enter");
            character.GetPlayerSpriteUpdater().ChangeSprite("block", 0);


        }
        public void OnFrame()
        {
            frame++;

            if (frame >= maxFrame)
            {
                sm.ChangeState(new Standing(character, sm));
            }
        }
        public void OnExit()
        {
            Debug.Log($"BlockStun Hit");
        }
    }

    public class Hit : IState
    {
        private IPlayableCharacter character;
        private StateMachineBehaviour sm;
       
        private int maxFrame = 30;
        private int frame = 0;
        private Motion hitByMotion;
        public Hit(IPlayableCharacter character, StateMachineBehaviour sm, Motion hitByMotion)
        {
            this.character = character;
            this.sm = sm;
            this.hitByMotion = hitByMotion;

        }

        public void OnEnter()
        {
            Debug.Log($"Hit Enter");
            character.GetPlayerSpriteUpdater().ChangeSprite("hit", 0);

            character.TakeDamage(hitByMotion.damage);

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