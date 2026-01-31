using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private int playerNumber = 1; // 1 o 2
    [SerializeField] private float moveSpeed = 5f;


    [Header("Visual")]
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("HP Settings")]
    [SerializeField] private float maxHP = 100f;
    [SerializeField] private float transformationThreshold = 20f; // HP sotto il quale si trasforma

    private CharacterData currentCharacter;
    [SerializeField] private float currentHP;
    private bool isTransformed = false;
    private float moveInput = 0f;

    // Riferimento al GameSceneController per la trasformazione
    private GameSceneController gameSceneController;

    // Riferimenti ai gamepad
    private Gamepad assignedGamepad;

    private void Awake()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        gameSceneController = FindFirstObjectByType<GameSceneController>();
    }

    private void Start()
    {
        AssignGamepad();
        currentHP = maxHP;
    }

    private void AssignGamepad()
    {
        var gamepads = Gamepad.all;
        if (playerNumber == 1 && gamepads.Count > 0)
            assignedGamepad = gamepads[0];
        else if (playerNumber == 2 && gamepads.Count > 1)
            assignedGamepad = gamepads[1];
    }

    public void Initialize(CharacterData character, int playerNum)
    {
        currentCharacter = character;
        playerNumber = playerNum;

        if (character != null && spriteRenderer != null && character.characterImage != null)
        {
            spriteRenderer.sprite = character.characterImage;
        }

        // Flip dello sprite per player 2 (guarda a sinistra)
        if (playerNumber == 2 && spriteRenderer != null)
        {
            spriteRenderer.flipX = true;
        }

        Debug.Log($"Player {playerNumber} inizializzato con: {character?.characterName}");
    }

    public void TransformTo(CharacterData transformationCharacter)
    {
        if (isTransformed) return;

        currentCharacter = transformationCharacter;
        isTransformed = true;

        if (spriteRenderer != null && transformationCharacter.characterImage != null)
        {
            spriteRenderer.sprite = transformationCharacter.characterImage;
        }

        Debug.Log($"Player {playerNumber} si è trasformato in: {transformationCharacter.characterName}");
    }

    private void Update()
    {
        HandleInput();
        Move();
    }

    private void HandleInput()
    {
        var keyboard = Keyboard.current;
        moveInput = 0f;

        if (playerNumber == 1)
        {
            // Player 1: A/D
            if (keyboard != null)
            {
                if (keyboard.aKey.isPressed) moveInput = -1f;
                else if (keyboard.dKey.isPressed) moveInput = 1f;
            }

            // Gamepad Player 1
            if (moveInput == 0f && assignedGamepad != null)
            {
                moveInput = assignedGamepad.leftStick.x.ReadValue();
                if (Mathf.Abs(moveInput) < 0.2f)
                {
                    if (assignedGamepad.dpad.left.isPressed) moveInput = -1f;
                    else if (assignedGamepad.dpad.right.isPressed) moveInput = 1f;
                }
            }
        }
        else if (playerNumber == 2)
        {
            // Player 2: Frecce
            if (keyboard != null)
            {
                if (keyboard.leftArrowKey.isPressed) moveInput = -1f;
                else if (keyboard.rightArrowKey.isPressed) moveInput = 1f;
            }

            // Gamepad Player 2
            if (moveInput == 0f && assignedGamepad != null)
            {
                moveInput = assignedGamepad.leftStick.x.ReadValue();
                if (Mathf.Abs(moveInput) < 0.2f)
                {
                    if (assignedGamepad.dpad.left.isPressed) moveInput = -1f;
                    else if (assignedGamepad.dpad.right.isPressed) moveInput = 1f;
                }
            }
        }
    }

    private void Move()
    {
        if (Mathf.Abs(moveInput) > 0.1f)
        {
            float newX = transform.position.x + moveInput * moveSpeed * Time.deltaTime;
            // Il CameraController gestisce il clamping ai bordi dello schermo
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        }
    }

    // Metodo per ricevere danno (da usare poi nel combat system)
    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        currentHP = Mathf.Max(0, currentHP);

        // Controlla se deve trasformarsi
        if (!isTransformed && currentHP <= transformationThreshold && gameSceneController != null)
        {
            gameSceneController.TransformPlayer(playerNumber);
        }

        if (currentHP <= 0)
        {
            OnDeath();
        }
    }

    private void OnDeath()
    {
        Debug.Log($"Player {playerNumber} è stato sconfitto!");
        // TODO: Implementare logica di fine round/partita
    }

    public float GetCurrentHP() => currentHP;
    public float GetMaxHP() => maxHP;
    public bool IsTransformed() => isTransformed;
    public CharacterData GetCurrentCharacter() => currentCharacter;
}
