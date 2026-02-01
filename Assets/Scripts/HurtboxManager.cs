using GGJ26.StateMachine;
using System;
using UnityEngine;

public class HurtboxManager : MonoBehaviour
{
    public BoxCollider2D HurtboxCollider;
    public PlayerController PlayerController;

    public void ClearCollider()
    {
        //clear collider
        HurtboxCollider.offset = Vector2.zero;
        HurtboxCollider.size = Vector2.zero;
        
    }

    public void SetupColliderFromDB(BoxCollider2D collider)
    {
        if (collider == null) return;

        if (MatchManager.Instance.IsFacingRight(PlayerController))
        {
            HurtboxCollider.offset = collider.offset;
        }
        else
        {
            // Solo l'offset viene negato per il flip, la size deve rimanere positiva
            HurtboxCollider.offset = new Vector2(-collider.offset.x, collider.offset.y);
        }
        // La size è sempre positiva
        HurtboxCollider.size = collider.size;
        HurtboxCollider.edgeRadius = collider.edgeRadius;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
