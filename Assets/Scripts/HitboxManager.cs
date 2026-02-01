using UnityEngine;
using GGJ26.StateMachine;

public class HitboxManager : MonoBehaviour
{
    public BoxCollider2D HitboxCollider;
    public PlayerController PlayerController;
    private bool HitCompleted = false;
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
        HitCompleted = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(HitCompleted) return;
        if (other.gameObject.TryGetComponent<HurtboxManager>(out HurtboxManager hurtboxManager) && !hurtboxManager.PlayerController.Equals(PlayerController))
        {
            Motion attack = PlayerController.stateMachine.GetMotion();
            if (attack != null)
            {
                // Calcola il danno finale senza modificare l'oggetto Motion originale
                float powerMultiplier = (attack.damage / 100f) * PlayerController.GetPower();
                int finalDamage = attack.damage + (int)powerMultiplier;
                Debug.Log($"[HIT] {PlayerController.name} -> {hurtboxManager.PlayerController.name}");
                Debug.Log($"[HIT] Attack: {attack.name} | Base Damage: {attack.damage} | Power: {PlayerController.GetPower()} | Bonus: +{(int)powerMultiplier} | TOTAL: {finalDamage}");
                hurtboxManager.PlayerController.stateMachine.GotHit(attack, hurtboxManager.PlayerController, finalDamage);
                HitCompleted = true;
            }
        }
    }
}
