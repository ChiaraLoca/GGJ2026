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
        if(collider == null) return;
        HurtboxCollider.offset = collider.offset;
        HurtboxCollider.size = (collider).size;
        HurtboxCollider.edgeRadius = collider.edgeRadius;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
