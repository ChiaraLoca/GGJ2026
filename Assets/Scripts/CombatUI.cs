using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CombatUI : MonoBehaviour
{
    [Header("Player 1 UI")]
    [SerializeField] private Image player1Portrait;
    [SerializeField] private Image player1SpecialSprite;
    [SerializeField] private Image player1HPBar;
    [SerializeField] private Image player1HPBarBackground;
    [SerializeField] private Image player1SpecialBar;
    [SerializeField] private Image player1SpecialBarBackground;
    [SerializeField] private TextMeshProUGUI player1NameText;

    [Header("Player 2 UI")]
    [SerializeField] private Image player2Portrait;
    [SerializeField] private Image player2SpecialSprite;
    [SerializeField] private Image player2HPBar;
    [SerializeField] private Image player2HPBarBackground;
    [SerializeField] private Image player2SpecialBar;
    [SerializeField] private Image player2SpecialBarBackground;
    [SerializeField] private TextMeshProUGUI player2NameText;

    [Header("Bar Colors")]
    [SerializeField] private Color hpFullColor = new Color(0.2f, 0.8f, 0.2f);
    [SerializeField] private Color hpMidColor = new Color(0.9f, 0.7f, 0.1f);
    [SerializeField] private Color hpLowColor = new Color(0.9f, 0.2f, 0.2f);
    [SerializeField] private Color specialColor = new Color(0.3f, 0.5f, 1f);

    [Header("Animation")]
    [SerializeField] private float barAnimationSpeed = 5f;

    // Valori target per animazione smooth
    private float player1HPTarget = 1f;
    private float player2HPTarget = 1f;
    private float player1SpecialTarget = 0f;
    private float player2SpecialTarget = 0f;

    // Valori attuali
    private float player1HPCurrent = 1f;
    private float player2HPCurrent = 1f;
    private float player1SpecialCurrent = 0f;
    private float player2SpecialCurrent = 0f;

    private void Update()
    {
        // Anima le barre smoothly
        player1HPCurrent = Mathf.Lerp(player1HPCurrent, player1HPTarget, barAnimationSpeed * Time.deltaTime);
        player2HPCurrent = Mathf.Lerp(player2HPCurrent, player2HPTarget, barAnimationSpeed * Time.deltaTime);
        player1SpecialCurrent = Mathf.Lerp(player1SpecialCurrent, player1SpecialTarget, barAnimationSpeed * Time.deltaTime);
        player2SpecialCurrent = Mathf.Lerp(player2SpecialCurrent, player2SpecialTarget, barAnimationSpeed * Time.deltaTime);

        UpdateBars();
    }

    private void UpdateBars()
    {
        // HP Bars
        if (player1HPBar != null)
        {
            player1HPBar.fillAmount = player1HPCurrent;
            player1HPBar.color = GetHPColor(player1HPCurrent);
        }
        if (player2HPBar != null)
        {
            player2HPBar.fillAmount = player2HPCurrent;
            player2HPBar.color = GetHPColor(player2HPCurrent);
        }

        // Special Bars
        if (player1SpecialBar != null)
        {
            player1SpecialBar.fillAmount = player1SpecialCurrent;
            player1SpecialBar.color = specialColor;
        }
        if (player2SpecialBar != null)
        {
            player2SpecialBar.fillAmount = player2SpecialCurrent;
            player2SpecialBar.color = specialColor;
        }
    }

    private Color GetHPColor(float hpPercent)
    {
        if (hpPercent > 0.5f)
            return Color.Lerp(hpMidColor, hpFullColor, (hpPercent - 0.5f) * 2f);
        else
            return Color.Lerp(hpLowColor, hpMidColor, hpPercent * 2f);
    }

    // === Metodi pubblici per aggiornare la UI ===

    public void SetPlayer1Portrait(CharacterData character)
    {
        if (character == null) return;

        if (player1Portrait != null && character.characterPortrait != null)
        {
            player1Portrait.sprite = character.characterPortrait;
        }
        if (player1SpecialSprite != null && character.characterSpecialSprite != null)
        {
            player1SpecialSprite.sprite = character.characterSpecialSprite;
            player1SpecialSprite.gameObject.SetActive(true);
        }
        else if (player1SpecialSprite != null)
        {
            player1SpecialSprite.gameObject.SetActive(false);
        }
        if (player1NameText != null)
        {
            player1NameText.text = character.characterName;
        }
    }

    public void SetPlayer2Portrait(CharacterData character)
    {
        if (character == null) return;

        if (player2Portrait != null && character.characterPortrait != null)
        {
            player2Portrait.sprite = character.characterPortrait;
        }
        if (player2SpecialSprite != null && character.characterSpecialSprite != null)
        {
            player2SpecialSprite.sprite = character.characterSpecialSprite;
            player2SpecialSprite.gameObject.SetActive(true);
        }
        else if (player2SpecialSprite != null)
        {
            player2SpecialSprite.gameObject.SetActive(false);
        }
        if (player2NameText != null)
        {
            player2NameText.text = character.characterName;
        }
    }

    public void SetPlayer1HP(float currentHP, float maxHP)
    {
        player1HPTarget = Mathf.Clamp01(currentHP / maxHP);
    }

    public void SetPlayer2HP(float currentHP, float maxHP)
    {
        player2HPTarget = Mathf.Clamp01(currentHP / maxHP);
    }

    public void SetPlayer1Special(float currentSpecial, float maxSpecial)
    {
        player1SpecialTarget = Mathf.Clamp01(currentSpecial / maxSpecial);
    }

    public void SetPlayer2Special(float currentSpecial, float maxSpecial)
    {
        player2SpecialTarget = Mathf.Clamp01(currentSpecial / maxSpecial);
    }

    // Metodo per settare HP istantaneamente (senza animazione)
    public void SetPlayer1HPInstant(float currentHP, float maxHP)
    {
        player1HPTarget = Mathf.Clamp01(currentHP / maxHP);
        player1HPCurrent = player1HPTarget;
        UpdateBars();
    }

    public void SetPlayer2HPInstant(float currentHP, float maxHP)
    {
        player2HPTarget = Mathf.Clamp01(currentHP / maxHP);
        player2HPCurrent = player2HPTarget;
        UpdateBars();
    }

    // Reset per inizio round
    public void ResetBars()
    {
        player1HPTarget = 1f;
        player2HPTarget = 1f;
        player1SpecialTarget = 0f;
        player2SpecialTarget = 0f;
        player1HPCurrent = 1f;
        player2HPCurrent = 1f;
        player1SpecialCurrent = 0f;
        player2SpecialCurrent = 0f;
        UpdateBars();
    }
}
