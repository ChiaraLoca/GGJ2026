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

    public void ClearCollider()
    {
        //clear collider
        HitboxCollider.offset = Vector2.zero;
        HitboxCollider.size = Vector2.zero;

    }

    public void SetupColliderFromDB(BoxCollider2D collider)
    {
        if (collider == null) return;

        if (MatchManager.Instance.IsFacingRight(PlayerController))
        {
            HitboxCollider.offset = collider.offset;
        }
        else
        {
            // Solo l'offset viene negato per il flip, la size deve rimanere positiva
            HitboxCollider.offset = new Vector2(-collider.offset.x, collider.offset.y);
        }
        // La size è sempre positiva
        HitboxCollider.size = collider.size;
        HitboxCollider.edgeRadius = collider.edgeRadius;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.TryGetComponent<HurtboxManager>(out HurtboxManager hurtboxManager) && !hurtboxManager.PlayerController.Equals(PlayerController))
        {
            Motion attack = PlayerController.stateMachine.GetMotion();
            if (attack != null)
            {
                // Calcola il danno finale senza modificare l'oggetto Motion originale
                int finalDamage = attack.damage + (int)((attack.damage / 100f) * PlayerController.GetPower());
                Debug.Log($"{PlayerController.name} hit {hurtboxManager.PlayerController.name} with {attack.name} for {finalDamage} damage!");
                hurtboxManager.PlayerController.stateMachine.GotHit(attack, hurtboxManager.PlayerController, finalDamage);
            }
        }
    }
}
