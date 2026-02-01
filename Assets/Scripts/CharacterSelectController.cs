using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;

public class CharacterSelectController : MonoBehaviour
{
    [Header("Character Database")]
    [SerializeField] private CharacterDatabase characterDatabase;

    [Header("Player 1 UI (Sinistra)")]
    [SerializeField] private Image player1CharacterImage;
    [SerializeField] private TextMeshProUGUI player1CharacterName;
    [SerializeField] private Button player1LeftArrow;
    [SerializeField] private Button player1RightArrow;
    [SerializeField] private GameObject player1ReadyIndicator;

    [Header("Player 2 UI (Destra)")]
    [SerializeField] private Image player2CharacterImage;
    [SerializeField] private TextMeshProUGUI player2CharacterName;
    [SerializeField] private Button player2LeftArrow;
    [SerializeField] private Button player2RightArrow;
    [SerializeField] private GameObject player2ReadyIndicator;

    [Header("Input Settings")]
    [SerializeField] private float inputCooldown = 0.25f;

    [Header("Scene Settings")]
    [SerializeField] private string gameSceneName = "GameScene";

    // L'indice 0 è riservato al personaggio "trasformazione" (non selezionabile)
    private const int FIRST_SELECTABLE_INDEX = 1;

    private int player1SelectedIndex = 1;
    private int player2SelectedIndex = 2;

    private float player1InputTimer = 0f;
    private float player2InputTimer = 0f;

    private bool player1Ready = false;
    private bool player2Ready = false;

    // Riferimenti ai gamepad
    private Gamepad player1Gamepad;
    private Gamepad player2Gamepad;

    private int CharacterCount => characterDatabase != null ? characterDatabase.CharacterCount : 0;

    private void Start()
    {
        // Setup bottoni frecce
        SetupArrowButtons();
        
        // Assegna i gamepad se disponibili
        AssignGamepads();
        
        // Nascondi indicatori ready
        if (player1ReadyIndicator != null) player1ReadyIndicator.SetActive(false);
        if (player2ReadyIndicator != null) player2ReadyIndicator.SetActive(false);

        // Inizializza display
        UpdateCharacterDisplay();
    }

    private void AssignGamepads()
    {
        var gamepads = Gamepad.all;
        if (gamepads.Count > 0)
            player1Gamepad = gamepads[0];
        if (gamepads.Count > 1)
            player2Gamepad = gamepads[1];
    }

    private void SetupArrowButtons()
    {
        // Player 1 frecce
        if (player1LeftArrow != null)
            player1LeftArrow.onClick.AddListener(Player1PreviousCharacter);
        if (player1RightArrow != null)
            player1RightArrow.onClick.AddListener(Player1NextCharacter);

        // Player 2 frecce
        if (player2LeftArrow != null)
            player2LeftArrow.onClick.AddListener(Player2PreviousCharacter);
        if (player2RightArrow != null)
            player2RightArrow.onClick.AddListener(Player2NextCharacter);
    }

    private void Update()
    {
        HandlePlayer1Input();
        HandlePlayer2Input();

        // Controlla se entrambi i giocatori hanno confermato
        if (player1Ready && player2Ready)
        {
            StartGame();
        }
    }

    private void HandlePlayer1Input()
    {
        player1InputTimer -= Time.deltaTime;

        var keyboard = Keyboard.current;

        if (!player1Ready)
        {
            float horizontal = 0f;

            // Tastiera: A/D
            if (keyboard != null)
            {
                if (keyboard.aKey.isPressed) horizontal = -1f;
                else if (keyboard.dKey.isPressed) horizontal = 1f;
            }

            // Gamepad Player 1: D-Pad o stick sinistro
            if (horizontal == 0f && player1Gamepad != null)
            {
                horizontal = player1Gamepad.leftStick.x.ReadValue();
                if (Mathf.Abs(horizontal) < 0.5f)
                {
                    if (player1Gamepad.dpad.left.isPressed) horizontal = -1f;
                    else if (player1Gamepad.dpad.right.isPressed) horizontal = 1f;
                }
            }

            if (Mathf.Abs(horizontal) > 0.5f && player1InputTimer <= 0)
            {
                ChangeSelection(ref player1SelectedIndex, horizontal > 0 ? 1 : -1);
                player1InputTimer = inputCooldown;
                UpdateCharacterDisplay();
            }

            // Conferma: Spazio o Gamepad South Button (A/X)
            bool confirmPressed = (keyboard != null && keyboard.spaceKey.wasPressedThisFrame) ||
                                  (player1Gamepad != null && player1Gamepad.buttonSouth.wasPressedThisFrame);
            if (confirmPressed)
            {
                ConfirmPlayer1Selection();
            }
        }
        else
        {
            // Annulla selezione: Backspace o Gamepad East Button (B/Circle)
            bool cancelPressed = (keyboard != null && keyboard.backspaceKey.wasPressedThisFrame) ||
                                 (player1Gamepad != null && player1Gamepad.buttonEast.wasPressedThisFrame);
            if (cancelPressed)
            {
                CancelPlayer1Selection();
            }
        }
    }

    private void HandlePlayer2Input()
    {
        player2InputTimer -= Time.deltaTime;

        var keyboard = Keyboard.current;

        if (!player2Ready)
        {
            float horizontal = 0f;

            // Tastiera: Frecce sinistra/destra
            if (keyboard != null)
            {
                if (keyboard.leftArrowKey.isPressed) horizontal = -1f;
                else if (keyboard.rightArrowKey.isPressed) horizontal = 1f;
            }

            // Gamepad Player 2: D-Pad o stick sinistro
            if (horizontal == 0f && player2Gamepad != null)
            {
                horizontal = player2Gamepad.leftStick.x.ReadValue();
                if (Mathf.Abs(horizontal) < 0.5f)
                {
                    if (player2Gamepad.dpad.left.isPressed) horizontal = -1f;
                    else if (player2Gamepad.dpad.right.isPressed) horizontal = 1f;
                }
            }

            if (Mathf.Abs(horizontal) > 0.5f && player2InputTimer <= 0)
            {
                ChangeSelection(ref player2SelectedIndex, horizontal > 0 ? 1 : -1);
                player2InputTimer = inputCooldown;
                UpdateCharacterDisplay();
            }

            // Conferma: Enter o Gamepad South Button (A/X)
            bool confirmPressed = (keyboard != null && keyboard.enterKey.wasPressedThisFrame) ||
                                  (player2Gamepad != null && player2Gamepad.buttonSouth.wasPressedThisFrame);
            if (confirmPressed)
            {
                ConfirmPlayer2Selection();
            }
        }
        else
        {
            // Annulla selezione: Delete o Gamepad East Button (B/Circle)
            bool cancelPressed = (keyboard != null && keyboard.deleteKey.wasPressedThisFrame) ||
                                 (player2Gamepad != null && player2Gamepad.buttonEast.wasPressedThisFrame);
            if (cancelPressed)
            {
                CancelPlayer2Selection();
            }
        }
    }

    private void ChangeSelection(ref int index, int direction)
    {
        index += direction;

        // Salta l'indice 0 (personaggio trasformazione non selezionabile)
        if (index < FIRST_SELECTABLE_INDEX)
            index = CharacterCount - 1;
        else if (index >= CharacterCount)
            index = FIRST_SELECTABLE_INDEX;
    }

    private void ConfirmPlayer1Selection()
    {
        player1Ready = true;
        SetArrowsInteractable(player1LeftArrow, player1RightArrow, false);
        if (player1ReadyIndicator != null) player1ReadyIndicator.SetActive(true);
        Debug.Log($"Player 1 ha selezionato: {characterDatabase.GetCharacter(player1SelectedIndex)?.characterName}");
    }

    private void CancelPlayer1Selection()
    {
        player1Ready = false;
        SetArrowsInteractable(player1LeftArrow, player1RightArrow, true);
        if (player1ReadyIndicator != null) player1ReadyIndicator.SetActive(false);
    }

    private void ConfirmPlayer2Selection()
    {
        player2Ready = true;
        SetArrowsInteractable(player2LeftArrow, player2RightArrow, false);
        if (player2ReadyIndicator != null) player2ReadyIndicator.SetActive(true);
        Debug.Log($"Player 2 ha selezionato: {characterDatabase.GetCharacter(player2SelectedIndex)?.characterName}");
    }

    private void CancelPlayer2Selection()
    {
        player2Ready = false;
        SetArrowsInteractable(player2LeftArrow, player2RightArrow, true);
        if (player2ReadyIndicator != null) player2ReadyIndicator.SetActive(false);
    }

    private void SetArrowsInteractable(Button leftArrow, Button rightArrow, bool interactable)
    {
        if (leftArrow != null) leftArrow.interactable = interactable;
        if (rightArrow != null) rightArrow.interactable = interactable;
    }

    private void UpdateCharacterDisplay()
    {
        if (characterDatabase == null || characterDatabase.CharacterCount == 0)
        {
            Debug.LogWarning("CharacterDatabase non assegnato o vuoto!");
            return;
        }

        // Limita gli indici al range valido (escludendo indice 0)
        player1SelectedIndex = Mathf.Clamp(player1SelectedIndex, FIRST_SELECTABLE_INDEX, CharacterCount - 1);
        player2SelectedIndex = Mathf.Clamp(player2SelectedIndex, FIRST_SELECTABLE_INDEX, CharacterCount - 1);

        var player1Character = characterDatabase.GetCharacter(player1SelectedIndex);
        var player2Character = characterDatabase.GetCharacter(player2SelectedIndex);

        if (player1Character != null)
        {
            if (player1CharacterImage != null && player1Character.characterImage != null)
                player1CharacterImage.sprite = player1Character.characterImage;
            if (player1CharacterName != null)
                player1CharacterName.text = player1Character.characterName;
        }

        if (player2Character != null)
        {
            if (player2CharacterImage != null && player2Character.characterImage != null)
                player2CharacterImage.sprite = player2Character.characterImage;
            if (player2CharacterName != null)
                player2CharacterName.text = player2Character.characterName;
        }
    }

    // Metodi pubblici per i bottoni UI (frecce)
    public void Player1NextCharacter()
    {
        if (player1Ready) return;
        ChangeSelection(ref player1SelectedIndex, 1);
        UpdateCharacterDisplay();
    }

    public void Player1PreviousCharacter()
    {
        if (player1Ready) return;
        ChangeSelection(ref player1SelectedIndex, -1);
        UpdateCharacterDisplay();
    }

    public void Player2NextCharacter()
    {
        if (player2Ready) return;
        ChangeSelection(ref player2SelectedIndex, 1);
        UpdateCharacterDisplay();
    }

    public void Player2PreviousCharacter()
    {
        if (player2Ready) return;
        ChangeSelection(ref player2SelectedIndex, -1);
        UpdateCharacterDisplay();
    }

    public void OnPlayer1ConfirmButton()
    {
        if (!player1Ready)
            ConfirmPlayer1Selection();
        else
            CancelPlayer1Selection();
    }

    public void OnPlayer2ConfirmButton()
    {
        if (!player2Ready)
            ConfirmPlayer2Selection();
        else
            CancelPlayer2Selection();
    }

    private void StartGame()
    {
        var p1Char = characterDatabase.GetCharacter(player1SelectedIndex);
        var p2Char = characterDatabase.GetCharacter(player2SelectedIndex);

        if (p1Char == null || p2Char == null)
        {
            Debug.LogError("Errore: personaggio non trovato!");
            return;
        }

        Debug.Log($"[CharacterSelect] Salvando selezione - P1 Index: {player1SelectedIndex} ({p1Char.characterName}), P2 Index: {player2SelectedIndex} ({p2Char.characterName})");

        // Salva le selezioni
        PlayerPrefs.SetInt("Player1CharacterIndex", player1SelectedIndex);
        PlayerPrefs.SetInt("Player2CharacterIndex", player2SelectedIndex);
        PlayerPrefs.SetString("Player1CharacterName", p1Char.characterName);
        PlayerPrefs.SetString("Player2CharacterName", p2Char.characterName);
        PlayerPrefs.Save();

        // Verifica che siano stati salvati correttamente
        int savedP1 = PlayerPrefs.GetInt("Player1CharacterIndex", -1);
        int savedP2 = PlayerPrefs.GetInt("Player2CharacterIndex", -1);
        Debug.Log($"[CharacterSelect] Verifica salvataggio - P1: {savedP1}, P2: {savedP2}");

        Debug.Log($"Avvio partita: {p1Char.characterName} vs {p2Char.characterName}");
        
        // Carica la scena di gioco
        if (!string.IsNullOrEmpty(gameSceneName))
            SceneManager.LoadScene(gameSceneName);
    }

    // Metodo per resettare la selezione (utile se si torna indietro)
    public void ResetSelection()
    {
        player1Ready = false;
        player2Ready = false;
        player1SelectedIndex = 0;
        player2SelectedIndex = CharacterCount > 1 ? 1 : 0;

        SetArrowsInteractable(player1LeftArrow, player1RightArrow, true);
        SetArrowsInteractable(player2LeftArrow, player2RightArrow, true);
        
        if (player1ReadyIndicator != null) player1ReadyIndicator.SetActive(false);
        if (player2ReadyIndicator != null) player2ReadyIndicator.SetActive(false);
        
        UpdateCharacterDisplay();
    }
}