using UnityEngine;
using GGJ26.StateMachine;

public class HitboxManager : MonoBehaviour
{
    public BoxCollider2D HitboxCollider;
    public PlayerController PlayerController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    public void SetupColliderFromDB(BoxCollider2D collider)
    {
        if (collider == null) return;

        if (MatchManager.Instance.IsFacingRight(PlayerController))
        {
            
            HitboxCollider.offset = collider.offset;
            HitboxCollider.size = (collider).size; 
        }
        else
        {
            
            HitboxCollider.offset = new Vector2(-collider.offset.x, collider.offset.y);
            HitboxCollider.size = new Vector2(-collider.size.x, collider.size.y);
        }
        HitboxCollider.edgeRadius = collider.edgeRadius;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.TryGetComponent<HurtboxManager>(out HurtboxManager hurtboxManager) && !hurtboxManager.PlayerController.Equals(PlayerController))
        {
            Motion attack = PlayerController.stateMachine.GetMotion();
            if (attack != null)
            {
                Debug.Log($"{PlayerController.name} hit {hurtboxManager.PlayerController.name} with {attack.name} for {attack.damage} damage!");
                hurtboxManager.PlayerController.stateMachine.GotHit(attack, hurtboxManager.PlayerController);
            }
        }
    }
}
