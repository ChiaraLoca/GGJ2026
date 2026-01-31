using UnityEngine;

public class GameSceneController : MonoBehaviour
{
    [Header("Character Database")]
    [SerializeField] private CharacterDatabase characterDatabase;

    [Header("Player References")]
    [SerializeField] private PlayerController player1Controller;
    [SerializeField] private PlayerController player2Controller;

    [Header("Debug Settings")]
    [SerializeField] private bool useDebugCharacters = false;
    [SerializeField] private int debugPlayer1Index = 1;
    [SerializeField] private int debugPlayer2Index = 2;

    // Indice del personaggio trasformazione (quando HP basso)
    public const int TRANSFORMATION_CHARACTER_INDEX = 0;

    private int player1CharacterIndex;
    private int player2CharacterIndex;

    private void Start()
    {
        LoadSelectedCharacters();
        InitializePlayers();
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
    }

    // Metodo per trasformare un player quando HP scende sotto soglia
    public void TransformPlayer(int playerNumber)
    {
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
}
