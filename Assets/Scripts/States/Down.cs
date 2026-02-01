using GGJ26.Input;
using UnityEngine;


namespace GGJ26.StateMachine
{

    public class KnockBackHelper
    { 
        public static void ApplyKnockBack(IPlayableCharacter character, Motion hitByMotion)
        {
            Rigidbody2D rb = character.GetRigidbody2D();
            float knockBackForce = hitByMotion.knockBack;

            Vector2 knockBackDirection;

            if(MatchManager.Instance.IsFacingRight(character))
                knockBackDirection = new Vector2(-1, 0).normalized;
            else
                knockBackDirection = new Vector2(1, 0).normalized;

            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y); // Reset horizontal velocity
            rb.AddForce(new Vector2(knockBackDirection.x * knockBackForce, knockBackDirection.y), ForceMode2D.Impulse);
        }
    }

    public class Down : IState
    {
        private IPlayableCharacter character;
        private StateMachineBehaviour sm;
       
        
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
            character.TakeDamage(hitByMotion.damage);
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