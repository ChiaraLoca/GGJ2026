using GGJ26.Input;
using UnityEngine;


namespace GGJ26.StateMachine
{

    public class Down : IState
    {
        private IPlayableCharacter character;
        private StateMachineBehaviour sm;
       
        
        private int frame = 0;
        private Motion hitByMotion;
        private int finalDamage;
        public Down(IPlayableCharacter character, StateMachineBehaviour sm, Motion hitByMotion, int finalDamage)
        {
            this.character = character;
            this.sm = sm;
            this.hitByMotion = hitByMotion;
            this.finalDamage = finalDamage;
        }

        public void OnEnter()
        {
            Debug.Log($"Down Enter");
            character.GetPlayerSpriteUpdater().ChangeSprite("down", 0);
            character.TakeDamage(finalDamage);
            KnockBackHelper.ApplyKnockBack(character, hitByMotion);
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