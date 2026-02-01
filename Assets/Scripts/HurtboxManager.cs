using GGJ26.StateMachine;
using System;
using UnityEngine;

public class HurtboxManager : MonoBehaviour
{
    public BoxCollider2D HurtboxCollider;
    public PlayerController PlayerController;

    internal void TakeDamage(int v)
    {
        throw new NotImplementedException();
    }

    public void SetupColliderFromDB(BoxCollider2D collider)
    {
        if (collider == null) return;

        if (MatchManager.Instance.IsFacingRight(PlayerController))
        {
            
            HurtboxCollider.offset = collider.offset;
            HurtboxCollider.size = (collider).size;
        }
        else
        {
            
            HurtboxCollider.offset = new Vector2(-collider.offset.x, collider.offset.y);
            HurtboxCollider.size = new Vector2(-collider.size.x, collider.size.y);
        }
        HurtboxCollider.edgeRadius = collider.edgeRadius;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
