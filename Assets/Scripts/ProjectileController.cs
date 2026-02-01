using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] public int speedBase = 5;
    [SerializeField] public Vector2 direction = Vector2.right;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //si sposta nella direzione di velocita
        transform.Translate(direction.normalized * speedBase * Time.fixedDeltaTime);


    }


}
