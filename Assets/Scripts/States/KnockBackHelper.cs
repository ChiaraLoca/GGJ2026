using GGJ26.Input;
using UnityEngine;


namespace GGJ26.StateMachine
{
    
    public static class GrappleHelper
    {
        /// <summary>
        /// Applica un knockback/launch al personaggio senza usare fisica,
        /// solleva il personaggio leggermente e lo riporta a terra alla fine.
        /// </summary>
        /// <param name="character">Personaggio da lanciare</param>
        /// <param name="distance">Distanza orizzontale del lancio</param>
        /// <param name="height">Altezza massima del lancio</param>
        /// <param name="frames">Durata del lancio in frame</param>
        public static void ApplyGrapLaunch(IPlayableCharacter character, float distance, float height, int frames)
        {
            Vector2 startPos = character.GetTransform().position;

            // Determina direzione opposta
            int dir = MatchManager.Instance.IsFacingRight(character) ? -1 : 1;

            Vector2 targetPos = startPos + new Vector2(dir * distance, 0);

            // Lancia una coroutine che muove il personaggio frame by frame
            character.StartCoroutine(LaunchCoroutine(character, startPos, targetPos, height, frames));
        }

        private static System.Collections.IEnumerator LaunchCoroutine(IPlayableCharacter character,
            Vector2 startPos, Vector2 targetPos, float height, int totalFrames)
        {
            for (int frame = 0; frame <= totalFrames; frame++)
            {
                float t = (float)frame / totalFrames; // 0 -> 1

                // X lineare
                float xPos = Mathf.Lerp(startPos.x, targetPos.x, t);

                // Y parabola: 0 -> height -> 0
                float yOffset = 4 * height * t * (1 - t);

                character.GetTransform().position = (new Vector2(xPos, startPos.y + yOffset));

                yield return null; // aspetta il prossimo frame
            }

            // Snap finale a terra
            character.GetTransform().position=(targetPos);
        }
    }


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
}