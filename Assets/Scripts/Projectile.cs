using UnityEngine;
using GGJ26.Input;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Projectile : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float speed = 20f;
    [SerializeField] private int damage = 20;
    [SerializeField] private float lifetime = 3f;
    [SerializeField] private bool destroyOnHit = true;
    
    [Header("Hit Properties")]
    [SerializeField] private int hitStunFrames = 15;
    [SerializeField] private int blockStunFrames = 5;
    [SerializeField] private float knockBack = 1.5f;
    [SerializeField] private bool causesKnockdown = false;

    private Vector2 direction;
    private PlayerController ownerPlayer;
    private float spawnTime;
    private Motion projectileHitMotion;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        // Configura il Rigidbody2D per non essere influenzato dalla fisica
        rb.gravityScale = 0f;
        rb.bodyType = RigidbodyType2D.Kinematic;
        
        // Assicurati che il collider sia trigger
        var collider = GetComponent<BoxCollider2D>();
        if (collider != null)
        {
            collider.isTrigger = true;
        }
    }

    /// <summary>
    /// Inizializza il proiettile con direzione e owner
    /// </summary>
    public void Initialize(Vector2 moveDirection, PlayerController owner, int customDamage = -1)
    {
        direction = moveDirection.normalized;
        ownerPlayer = owner;
        spawnTime = Time.time;
        
        if (customDamage > 0)
            damage = customDamage;

        // Flip dello sprite se va a sinistra
        if (direction.x < 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = -Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
        
        // Crea una Motion per gestire l'hit del proiettile
        CreateProjectileMotion();
    }
    
    private void CreateProjectileMotion()
    {
        // Crea una motion fittizia per il sistema di hit
        InputData[] inputs = { new InputData(NumpadDirection.Neutral, false) };
        projectileHitMotion = new Motion(
            name: "projectileHit",
            inputs: inputs,
            flippedInputs: inputs,
            totalFrames: 1,
            startupEnd: 0,
            activeEnd: 1,
            priority: 0,
            damage: damage,
            hitStunFrames: hitStunFrames,
            blockStunFrames: blockStunFrames,
            knockDown: causesKnockdown,
            knockBack: knockBack,
            recoveryFrameSwitch: 0,
            specialRequiredPower: 0
        );
    }

    private void Update()
    {
        // Movimento
        transform.Translate(direction * speed * Time.deltaTime, Space.World);

        // Auto-distruzione dopo lifetime
        if (Time.time - spawnTime > lifetime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Controlla se ha colpito una hurtbox
        if (other.TryGetComponent<HurtboxManager>(out HurtboxManager hurtbox))
        {
            // Non colpire il proprio owner
            if (hurtbox.PlayerController == ownerPlayer)
                return;

            // Applica danno usando il sistema GotHit (come le altre mosse)
            Debug.Log($"[Projectile] Colpito {hurtbox.PlayerController.name} per {damage} danni!");
            hurtbox.PlayerController.stateMachine.GotHit(projectileHitMotion, hurtbox.PlayerController, damage);

            if (destroyOnHit)
            {
                Destroy(gameObject);
            }
        }
    }

    // Opzionale: distruggi se esce dallo schermo
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
