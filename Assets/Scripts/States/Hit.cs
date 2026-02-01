using GGJ26.Input;
using UnityEngine;


namespace GGJ26.StateMachine
{
    public class BlockStun : IState
    {
        private IPlayableCharacter character;
        private StateMachineBehaviour sm;

        
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
            KnockBackHelper.ApplyKnockBack(character, hitByMotion);


        }
        public void OnFrame()
        {
            frame++;

            if (frame >= hitByMotion.blockStunFrames)
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
            frame = 0;
            character.TakeDamage(hitByMotion.damage);
            KnockBackHelper.ApplyKnockBack(character, hitByMotion);

        }
        public void OnFrame()
        {
            frame++;

            if (frame>=hitByMotion.hitStunFrames)
            {
                sm.ChangeState(new Neutral(character, sm));
            }
        }
        public void OnExit()
        {
            Debug.Log($"Crouch Hit");
        }
    }
}