using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelectController : MonoBehaviour
{
    [SerializeField] private CharacterData[] characters = new CharacterData[3];

    // Player 1 (sinistra)
    [SerializeField] private Image player1CharacterImage;
    [SerializeField] private Text player1CharacterName;
    [SerializeField] private Image player1Arrow;

    // Player 2 (destra)
    [SerializeField] private Image player2CharacterImage;
    [SerializeField] private Text player2CharacterName;
    [SerializeField] private Image player2Arrow;

    // Input
    [SerializeField] private float inputCooldown = 0.2f;

    private int player1SelectedIndex = 0;
    private int player2SelectedIndex = 1;

    private float player1InputTimer = 0f;
    private float player2InputTimer = 0f;

    private int player1Character = -1;
    private int player2Character = -1;

    private void Start()
    {
        UpdateCharacterDisplay();
    }

    private void Update()
    {
        HandlePlayer1Input();
        HandlePlayer2Input();
    }

    private void HandlePlayer1Input()
    {
        player1InputTimer -= Time.deltaTime;

        if (Input.GetKey(KeyCode.A) && player1InputTimer <= 0)
        {
            player1SelectedIndex--;
            if (player1SelectedIndex < 0)
                player1SelectedIndex = characters.Length - 1;

            player1InputTimer = inputCooldown;
            UpdateCharacterDisplay();
        }

        if (Input.GetKey(KeyCode.D) && player1InputTimer <= 0)
        {
            player1SelectedIndex++;
            if (player1SelectedIndex >= characters.Length)
                player1SelectedIndex = 0;

            player1InputTimer = inputCooldown;
            UpdateCharacterDisplay();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            player1Character = player1SelectedIndex;
            player1Arrow.enabled = false;
        }
    }

    private void HandlePlayer2Input()
    {
        player2InputTimer -= Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftArrow) && player2InputTimer <= 0)
        {
            player2SelectedIndex--;
            if (player2SelectedIndex < 0)
                player2SelectedIndex = characters.Length - 1;

            player2InputTimer = inputCooldown;
            UpdateCharacterDisplay();
        }

        if (Input.GetKey(KeyCode.LeftArrow) && player2InputTimer <= 0)
        {
            player2SelectedIndex++;
            if (player2SelectedIndex >= characters.Length)
                player2SelectedIndex = 0;

            player2InputTimer = inputCooldown;
            UpdateCharacterDisplay();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            player2Character = player2SelectedIndex;
            player2Arrow.enabled = false;
        }
    }

    private void UpdateCharacterDisplay()
    {
        player1CharacterImage.sprite = characters[player1SelectedIndex].characterImage;
        player1CharacterName.text = characters[player1SelectedIndex].characterName;

        player2CharacterImage.sprite = characters[player2SelectedIndex].characterImage;
        player2CharacterName.text = characters[player2SelectedIndex].characterName;
    }

    public void ConfirmSelection()
    {
        if (player1Character >= 0 && player2Character >= 0)
        {
            PlayerPrefs.SetInt("Player1Character", player1Character);
            PlayerPrefs.SetInt("Player2Character", player2Character);
            SceneManager.LoadScene("GameScene");
        }
    }
}