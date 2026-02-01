using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
   public BoxCollider2D collider2d;

    public void Active(bool val)
    { 
        collider2d.enabled = val;
    }

}
