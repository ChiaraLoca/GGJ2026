using UnityEngine;

/// &lt;summary&gt;
/// Gestisce le animazioni del player. I metodi pubblici possono essere chiamati
/// da altre classi (es. input handler, combat system) per triggerare le animazioni.
/// &lt;/summary&gt;
public class PlayerAnimator : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("Animation State")]
    [SerializeField] private bool isGrounded = true;
    [SerializeField] private bool isBlocking = false;
    [SerializeField] private bool isCrouching = false;
    [SerializeField] private bool isAttacking = false;
    [SerializeField] private bool isHit = false;
    [SerializeField] private bool isDown = false;

    // Nomi dei parametri dell Animator (da configurare nell Animator Controller)
    private static readonly int ANIM_IDLE = Animator.StringToHash("Idle");
    private static readonly int ANIM_BLOCK = Animator.StringToHash("Block");
    private static readonly int ANIM_CROUCH = Animator.StringToHash("Crouch");
    private static readonly int ANIM_JUMP = Animator.StringToHash("Jump");
    private static readonly int ANIM_HIT = Animator.StringToHash("Hit");
    private static readonly int ANIM_DOWN = Animator.StringToHash("Down");
    private static readonly int ANIM_GETUP = Animator.StringToHash("GetUp");
    private static readonly int ANIM_PUNCH = Animator.StringToHash("Punch");
    private static readonly int ANIM_KICK = Animator.StringToHash("Kick");
    private static readonly int ANIM_LOW_HIT = Animator.StringToHash("LowHit");
    private static readonly int ANIM_SPECIAL = Animator.StringToHash("Special");
    private static readonly int ANIM_IS_GROUNDED = Animator.StringToHash("IsGrounded");
    private static readonly int ANIM_IS_BLOCKING = Animator.StringToHash("IsBlocking");
    private static readonly int ANIM_IS_CROUCHING = Animator.StringToHash("IsCrouching");

    private void Awake()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // ==================== Movement Animations ====================

    /// &lt;summary&gt;
    /// Animazione di riposo/attesa
    /// &lt;/summary&gt;
    public void PlayIdle()
    {
        if (CanPlayAnimation())
        {
            ResetStates();
            if (animator != null) animator.SetTrigger(ANIM_IDLE);
        }
    }

    /// &lt;summary&gt;
    /// Animazione di blocco/parata
    /// &lt;/summary&gt;
    public void PlayBlock(bool blocking)
    {
        isBlocking = blocking;
        if (animator != null) animator.SetBool(ANIM_IS_BLOCKING, blocking);
        
        if (blocking)
        {
            if (animator != null) animator.SetTrigger(ANIM_BLOCK);
        }
    }

    /// &lt;summary&gt;
    /// Animazione abbassato/accovacciato
    /// &lt;/summary&gt;
    public void PlayCrouch(bool crouching)
    {
        isCrouching = crouching;
        if (animator != null) animator.SetBool(ANIM_IS_CROUCHING, crouching);
        
        if (crouching)
        {
            if (animator != null) animator.SetTrigger(ANIM_CROUCH);
        }
    }

    /// &lt;summary&gt;
    /// Animazione di salto
    /// &lt;/summary&gt;
    public void PlayJump()
    {
        if (isGrounded && CanPlayAnimation())
        {
            isGrounded = false;
            if (animator != null) animator.SetBool(ANIM_IS_GROUNDED, false);
            if (animator != null) animator.SetTrigger(ANIM_JUMP);
        }
    }

    /// &lt;summary&gt;
    /// Chiamato quando il player atterra
    /// &lt;/summary&gt;
    public void OnLand()
    {
        isGrounded = true;
        if (animator != null) animator.SetBool(ANIM_IS_GROUNDED, true);
    }

    // ==================== Damage Animations ====================

    /// &lt;summary&gt;
    /// Animazione quando viene colpito
    /// &lt;/summary&gt;
    public void PlayHit()
    {
        isHit = true;
        isAttacking = false;
        if (animator != null) animator.SetTrigger(ANIM_HIT);
    }

    /// &lt;summary&gt;
    /// Animazione quando cade a terra (knockdown)
    /// &lt;/summary&gt;
    public void PlayDown()
    {
        isDown = true;
        isAttacking = false;
        if (animator != null) animator.SetTrigger(ANIM_DOWN);
    }

    /// &lt;summary&gt;
    /// Animazione di rialzata da terra
    /// &lt;/summary&gt;
    public void PlayGetUp()
    {
        if (isDown)
        {
            isDown = false;
            if (animator != null) animator.SetTrigger(ANIM_GETUP);
        }
    }

    // ==================== Attack Animations ====================

    /// &lt;summary&gt;
    /// Animazione del pugno
    /// &lt;/summary&gt;
    public void PlayPunch()
    {
        if (CanAttack())
        {
            isAttacking = true;
            if (animator != null) animator.SetTrigger(ANIM_PUNCH);
        }
    }

    /// &lt;summary&gt;
    /// Animazione del calcio
    /// &lt;/summary&gt;
    public void PlayKick()
    {
        if (CanAttack())
        {
            isAttacking = true;
            if (animator != null) animator.SetTrigger(ANIM_KICK);
        }
    }

    /// &lt;summary&gt;
    /// Animazione del colpo basso (da accovacciato)
    /// &lt;/summary&gt;
    public void PlayLowHit()
    {
        if (CanAttack())
        {
            isAttacking = true;
            if (animator != null) animator.SetTrigger(ANIM_LOW_HIT);
        }
    }

    /// &lt;summary&gt;
    /// Animazione della mossa speciale
    /// &lt;/summary&gt;
    public void PlaySpecial()
    {
        if (CanAttack())
        {
            isAttacking = true;
            if (animator != null) animator.SetTrigger(ANIM_SPECIAL);
        }
    }

    // ==================== Animation Events (chiamati dall Animator) ====================

    /// &lt;summary&gt;
    /// Chiamato dall Animator quando un attacco finisce
    /// &lt;/summary&gt;
    public void OnAttackEnd()
    {
        isAttacking = false;
    }

    /// &lt;summary&gt;
    /// Chiamato dall Animator quando l animazione di hit finisce
    /// &lt;/summary&gt;
    public void OnHitEnd()
    {
        isHit = false;
    }

    /// &lt;summary&gt;
    /// Chiamato dall Animator quando la rialzata finisce
    /// &lt;/summary&gt;
    public void OnGetUpEnd()
    {
        isDown = false;
    }

    // ==================== State Checks ====================

    /// &lt;summary&gt;
    /// Controlla se il player puo attaccare
    /// &lt;/summary&gt;
    public bool CanAttack()
    {
        return !isAttacking && !isHit && !isDown && !isBlocking;
    }

    /// &lt;summary&gt;
    /// Controlla se puo eseguire un animazione generica
    /// &lt;/summary&gt;
    public bool CanPlayAnimation()
    {
        return !isHit && !isDown;
    }

    private void ResetStates()
    {
        isBlocking = false;
        isCrouching = false;
    }

    // ==================== Getters ====================

    public bool IsGrounded { get { return isGrounded; } }
    public bool IsBlocking { get { return isBlocking; } }
    public bool IsCrouching { get { return isCrouching; } }
    public bool IsAttacking { get { return isAttacking; } }
    public bool IsHit { get { return isHit; } }
    public bool IsDown { get { return isDown; } }

    // ==================== Utility ====================

    /// &lt;summary&gt;
    /// Imposta il flip dello sprite (per direzione)
    /// &lt;/summary&gt;
    public void SetFacing(bool faceLeft)
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = faceLeft;
        }
    }

    /// &lt;summary&gt;
    /// Forza un reset di tutti gli stati (es. inizio round)
    /// &lt;/summary&gt;
    public void ResetAllStates()
    {
        isGrounded = true;
        isBlocking = false;
        isCrouching = false;
        isAttacking = false;
        isHit = false;
        isDown = false;

        if (animator != null) animator.SetBool(ANIM_IS_GROUNDED, true);
        if (animator != null) animator.SetBool(ANIM_IS_BLOCKING, false);
        if (animator != null) animator.SetBool(ANIM_IS_CROUCHING, false);
    }
}
