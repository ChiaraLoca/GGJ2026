using GGJ26.Input;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneController : MonoBehaviour
{
    [Header("Character Database")]
    [SerializeField] private CharacterDatabase characterDatabase;

    [Header("Player References")]
    [SerializeField] private PlayerController player1Controller;
    [SerializeField] private PlayerController player2Controller;

    [Header("UI")]
    [SerializeField] private CombatUI combatUI;

    [Header("Debug Settings")]
    [SerializeField] private bool useDebugCharacters = false;
    [SerializeField] private int debugPlayer1Index = 1;
    [SerializeField] private int debugPlayer2Index = 2;

    [Header("Music")]
    [SerializeField] private SoundController soundController;



    // Indice del personaggio trasformazione (quando HP basso)
    public const int TRANSFORMATION_CHARACTER_INDEX = 0;

    private int player1CharacterIndex;
    private int player2CharacterIndex;

    private void Start()
    {
        LoadSelectedCharacters();
        SetupPlayersInScene();
        InitializePlayers();
        // Avvia la musica di background
        if (soundController != null)
        {
            soundController.PlayBackgroundMusic();
        }
        
        // Trova il CombatUI se non assegnato
        if (combatUI == null)
        {
            combatUI = FindObjectOfType<CombatUI>();
            if (combatUI != null)
            {
                Debug.Log("[GameSceneController] CombatUI trovato automaticamente");
            }
        }
    }

    void SetupPlayersInScene()
    {
        // Trova gli InputHandler dei prefab creati in scena 1
        InputHandler[] handlers = GameObject.FindObjectsOfType<InputHandler>();

        // Assumi che handlers[0] = player 1, handlers[1] = player 2
        PlayerController[] controllers = GameObject.FindObjectsOfType<PlayerController>();

        controllers[0].InitializeHandler(handlers[0]);
        controllers[1].InitializeHandler(handlers[1]);
    }


    private void LoadSelectedCharacters()
    {
        if (useDebugCharacters)
        {
            // Modalità debug: usa i valori impostati nell'inspector
            player1CharacterIndex = debugPlayer1Index;
            player2CharacterIndex = debugPlayer2Index;
            Debug.Log($"[DEBUG] Usando personaggi di debug: P1={player1CharacterIndex}, P2={player2CharacterIndex}");
        }
        else
        {
            // Carica da PlayerPrefs (impostati dalla schermata di selezione)
            player1CharacterIndex = PlayerPrefs.GetInt("Player1CharacterIndex", 1);
            player2CharacterIndex = PlayerPrefs.GetInt("Player2CharacterIndex", 2);
            Debug.Log($"Personaggi caricati da PlayerPrefs: P1={player1CharacterIndex}, P2={player2CharacterIndex}");
        }

        player1Controller.SetCharacterIndex(player1CharacterIndex);
        player2Controller.SetCharacterIndex(player2CharacterIndex);

        player1Controller.GetPlayerSpriteUpdater().SetCharacterIndex(player1CharacterIndex);
        player2Controller.GetPlayerSpriteUpdater().SetCharacterIndex(player2CharacterIndex);

    }

    private void InitializePlayers()
    {
        if (characterDatabase == null)
        {
            Debug.LogError("CharacterDatabase non assegnato!");
            return;
        }

        var p1Character = characterDatabase.GetCharacter(player1CharacterIndex);
        var p2Character = characterDatabase.GetCharacter(player2CharacterIndex);

        if (p1Character != null && player1Controller != null)
        {
            player1Controller.Initialize(p1Character, 1);
        }

        if (p2Character != null && player2Controller != null)
        {
            player2Controller.Initialize(p2Character, 2);
        }

        // Inizializza la UI di combattimento
        InitializeCombatUI(p1Character, p2Character);
    }

    private void InitializeCombatUI(CharacterData p1Character, CharacterData p2Character)
    {
        if (combatUI == null)
        {
            Debug.LogWarning("[GameSceneController] CombatUI è null in InitializeCombatUI!");
            return;
        }

        // Imposta i ritratti
        combatUI.SetPlayer1Portrait(p1Character);
        combatUI.SetPlayer2Portrait(p2Character);

        // Usa Invoke per ritardare di un frame e assicurare che tutto sia pronto
        Invoke(nameof(DelayedUIInitialization), 0.05f);
    }

    private void DelayedUIInitialization()
    {
        // Imposta HP iniziale al massimo
        if (player1Controller != null && combatUI != null)
            combatUI.SetPlayer1HPInstant(player1Controller.GetCurrentHP(), player1Controller.GetMaxHP());
        if (player2Controller != null && combatUI != null)
            combatUI.SetPlayer2HPInstant(player2Controller.GetCurrentHP(), player2Controller.GetMaxHP());

        // Imposta Special iniziale a 0
        if (player1Controller != null && combatUI != null)
            combatUI.SetPlayer1SpecialInstant(player1Controller.GetCurrentSpecial(), player1Controller.GetMaxSpecial());
        if (player2Controller != null && combatUI != null)
            combatUI.SetPlayer2SpecialInstant(player2Controller.GetCurrentSpecial(), player2Controller.GetMaxSpecial());

        Debug.Log("[GameSceneController] CombatUI inizializzata correttamente");
    }

    private void Update()
    {
        // Aggiorna HP nella UI
        UpdateCombatUI();
        if(player1Controller.GetCurrentHP() <= 0 || player2Controller.GetCurrentHP() <= 0)
        {
            EndGame();
        }
    }

    private void UpdateCombatUI()
    {
        if (combatUI == null) return;
        Debug.Log(player1Controller.GetCurrentHP() + " " + player1Controller.GetMaxHP());
        Debug.Log(player2Controller.GetCurrentHP() + " " + player2Controller.GetMaxHP());
        // Aggiorna HP
        if (player1Controller != null)
            combatUI.SetPlayer1HP(player1Controller.GetCurrentHP(), player1Controller.GetMaxHP());
        if (player2Controller != null)
            combatUI.SetPlayer2HP(player2Controller.GetCurrentHP(), player2Controller.GetMaxHP());

        // Aggiorna Special
        if (player1Controller != null)
            combatUI.SetPlayer1Special(player1Controller.GetCurrentSpecial(), player1Controller.GetMaxSpecial());
        if (player2Controller != null)
            combatUI.SetPlayer2Special(player2Controller.GetCurrentSpecial(), player2Controller.GetMaxSpecial());
    }

    // Metodo per trasformare un player quando HP scende sotto soglia
    public void TransformPlayer(int playerNumber)
    {
        Debug.Log($"Trasformazione del giocatore {playerNumber} in corso...");
        var transformationCharacter = characterDatabase.GetCharacter(TRANSFORMATION_CHARACTER_INDEX);
        if (transformationCharacter == null)
        {
            Debug.LogWarning("Personaggio trasformazione (indice 0) non trovato!");
            return;
        }

        if (playerNumber == 1 && player1Controller != null)
        {
            player1Controller.TransformTo(transformationCharacter);
        }
        else if (playerNumber == 2 && player2Controller != null)
        {
            player2Controller.TransformTo(transformationCharacter);
        }
    }

    public CharacterData GetPlayer1Character()
    {
        return characterDatabase?.GetCharacter(player1CharacterIndex);
    }

    public CharacterData GetPlayer2Character()
    {
        return characterDatabase?.GetCharacter(player2CharacterIndex);
    }

    public void EndGame()
    {
        if (player1Controller.GetCurrentHP() <= 0)
        {
            SceneManager.LoadScene("Player2Win");
        }
        if (player2Controller.GetCurrentHP() <= 0)
        {
            SceneManager.LoadScene("Player1Win");
        }
    }
}
