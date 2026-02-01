using GGJ26.Input;
using UnityEngine;


namespace GGJ26.StateMachine
{

    public class KnockBackHelper
    { 
        public static void ApplyKnockBack(IPlayableCharacter character,Motion hitByMotion)
        {

            
            float distance = hitByMotion.knockBack;
            
            Debug.Log($"Applying KnockBack of {distance}");
            Rigidbody2D rb = character.GetRigidbody2D();

            Vector2 dir;

            if (MatchManager.Instance.IsFacingRight(character))
                dir = Vector2.left;
            else
                dir = Vector2.right;

            Vector2 targetPosition = rb.position + dir * distance;

            rb.linearVelocity = Vector2.zero;
            rb.MovePosition(targetPosition);
        }
    }

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