using GGJ26.Input;
using GGJ26.StateMachine;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using StateMachineBehaviour = GGJ26.StateMachine.StateMachineBehaviour;

public class PlayerController : MonoBehaviour,IPlayableCharacter
{
    private Rigidbody2D rb;
    private InputHandler inputHandler;
    private StateMachineBehaviour stateMachine;
    private InputCollector inputCollector;
    public bool isAttacking = false;

    public TextMeshProUGUI status;

    [Header("Player Settings")]
    [SerializeField] private int playerNumber = 1; // 1 o 2
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float baseMoveSpeed = 5f;
    [SerializeField] private float power = 5f;

    [Header("Visual")]
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("HP Settings")]
    [SerializeField] private float maxHP = 100f;
    [SerializeField] private float baseMaxHP = 100f;
    [SerializeField] private float transformationThreshold = 20f; // HP sotto il quale si trasforma

    [Header("Special Settings")]
    [SerializeField] private float maxSpecial = 100f;
    [SerializeField] private float specialGainPerSecond = 5f; // Guadagno passivo di special
    [SerializeField] private float specialGainOnHit = 10f; // Guadagno quando colpisci
    [SerializeField] private float specialGainOnDamage = 15f; // Guadagno quando vieni colpito

    [Header("Debug - Current Values")]
    [SerializeField] private float currentHP;
    [SerializeField] private float currentSpecial;

    private CharacterData currentCharacter;
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

        
        rb = GetComponent<Rigidbody2D>();

        inputCollector = GetComponent<InputCollector>();
        

        stateMachine = new StateMachineBehaviour();
        stateMachine.ChangeState(new Move(this, stateMachine));

    }

    private void Start()
    {
        AssignGamepad();
        currentHP = maxHP;
        currentSpecial = 0f; // Special parte da 0

        
        
    }

    private void FixedUpdate()
    {
        
        stateMachine.Tick();
        status.text = stateMachine.current.GetType().Name;
    }

    private void AssignGamepad()
    {
        var gamepads = Gamepad.all;
        if (playerNumber == 1 && gamepads.Count > 0)
            assignedGamepad = gamepads[0];
        else if (playerNumber == 2 && gamepads.Count > 1)
            assignedGamepad = gamepads[1];
    }
    public void InitializeHandler(InputHandler handler)
    {

        inputHandler = handler;
        inputCollector.SetInputHandler(inputHandler);
        MatchManager.Instance.RegisterPlayer(this);
    }


    

    public void Initialize(CharacterData character, int playerNum)
    {
        currentCharacter = character;
        playerNumber = playerNum;

        if (character != null)
        {
            // Applicare i modificatori da CharacterData
            maxHP = baseMaxHP + character.hp;
            moveSpeed = baseMoveSpeed + (0.2f * character.speed);
            power = 5f + character.power;

            if (spriteRenderer != null && character.characterImage != null)
            {
                spriteRenderer.sprite = character.characterImage;
            }
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
        //HandleInput();
        //Move();
        UpdateSpecial();
    }

    private void UpdateSpecial()
    {
        // Guadagno passivo di special nel tempo
        if (currentSpecial < maxSpecial)
        {
            currentSpecial += specialGainPerSecond * Time.deltaTime;
            currentSpecial = Mathf.Min(currentSpecial, maxSpecial);
        }
    }

    /*private void HandleInput()
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
    }*/

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
        // Invoca il callback OnHit quando viene colpito
        currentCharacter?.OnHit?.Invoke(currentCharacter);

        currentHP -= damage;
        currentHP = Mathf.Max(0, currentHP);

        // Guadagna special quando vieni colpito
        AddSpecial(specialGainOnDamage);

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

    // Chiamato quando colpisci un avversario
    public void OnHitEnemy()
    {
        // Invoca il callback OnAttack quando colpisce
        currentCharacter?.OnAttack?.Invoke(currentCharacter);

        AddSpecial(specialGainOnHit);
    }

    // Aggiunge special (con clamp)
    public void AddSpecial(float amount)
    {
        currentSpecial += amount;
        currentSpecial = Mathf.Clamp(currentSpecial, 0f, maxSpecial);
    }

    // Usa la special (ritorna true se aveva abbastanza)
    public bool UseSpecial(float amount)
    {
        if (currentSpecial >= amount)
        {
            currentSpecial -= amount;
            return true;
        }
        return false;
    }

    // Controlla se la special è piena
    public bool IsSpecialFull()
    {
        return currentSpecial >= maxSpecial;
    }

    private void OnDeath()
    {
        Debug.Log($"Player {playerNumber} è stato sconfitto!");
        // TODO: Implementare logica di fine round/partita
    }

    public float GetCurrentHP() => currentHP;
    public float GetMaxHP() => maxHP;
    public float GetCurrentSpecial() => currentSpecial;
    public float GetMaxSpecial() => maxSpecial;
    public float GetPower() => power;
    public bool IsTransformed() => isTransformed;
    public CharacterData GetCurrentCharacter() => currentCharacter;

    public InputCollector GetInputCollector()
    {
        return inputCollector;
    }

    public InputHandler GetInputHandler()
    {
        return inputHandler;
    }

    public Rigidbody2D GetRigidbody2D()
    {
        return rb;
    }

    public bool IsAttacking()
    {
        return isAttacking;
    }

    public void SetAttacking(bool attacking)
    {
        isAttacking = attacking;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public void SetInputHandler(InputHandler handler)
    {
        inputHandler = handler;
    }
}
