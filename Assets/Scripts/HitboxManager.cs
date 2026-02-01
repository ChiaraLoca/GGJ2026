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

    // Update is called once per frame
    void Update()
    {
        
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
