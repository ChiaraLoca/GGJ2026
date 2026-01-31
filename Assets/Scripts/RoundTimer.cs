using UnityEngine;
using TMPro;

public class RoundTimer : MonoBehaviour
{
    [Header("Timer Settings")]
    [SerializeField] private float roundDuration = 60f;
    [SerializeField] private int totalExclamations = 19;
    [SerializeField] private float exclamationInterval = 6f; // Ogni 6 secondi rimuovi uno per lato

    [Header("UI Reference")]
    [SerializeField] private TextMeshProUGUI timerText;

    [Header("Player References")]
    [SerializeField] private PlayerController player1;
    [SerializeField] private PlayerController player2;

    [Header("Sound")]
    [SerializeField] private SoundController soundController;

    private float currentTime;
    private int currentExclamations;
    private float nextExclamationTime;
    private bool roundEnded = false;

    private void Start()
    {
        currentTime = roundDuration;
        currentExclamations = totalExclamations;
        nextExclamationTime = roundDuration - exclamationInterval;
        UpdateTimerDisplay();
    }

    private void Update()
    {
        if (roundEnded) return;

        currentTime -= Time.deltaTime;

        // Riduci i punti esclamativi ogni intervallo
        if (currentTime <= nextExclamationTime && currentExclamations > 1)
        {
            // Rimuovi uno da ogni lato (2 totali), ma mantieni almeno 1 centrale
            currentExclamations -= 2;
            if (currentExclamations < 1) currentExclamations = 1;
            
            if(currentExclamations == 15)
            {
                soundController.StretchMusic(soundController.GetCurrentPitch() * 1.2f); 
            }

            if (currentExclamations == 7)
            {
                soundController.StretchMusic(soundController.GetCurrentPitch() * 1.2f);
            }

            nextExclamationTime -= exclamationInterval;
            UpdateTimerDisplay();
        }

        // Fine del round
        if (currentTime <= 0)
        {
            currentTime = 0;
            currentExclamations = 1;
            UpdateTimerDisplay();
            EndRound();
        }
    }

    private void UpdateTimerDisplay()
    {
        if (timerText == null) return;

        // Crea la stringa di punti esclamativi
        timerText.text = new string('!', currentExclamations);
    }

    private void EndRound()
    {
        if (roundEnded) return;
        roundEnded = true;

        Debug.Log("=== ROUND TERMINATO - TEMPO SCADUTO ===");

        float p1HP = 0f;
        float p2HP = 0f;

        if (player1 != null)
        {
            p1HP = player1.GetCurrentHP();
            Debug.Log($"Player 1 HP: {p1HP}/{player1.GetMaxHP()}");
        }
        else
        {
            Debug.LogWarning("Player 1 non assegnato!");
        }

        if (player2 != null)
        {
            p2HP = player2.GetCurrentHP();
            Debug.Log($"Player 2 HP: {p2HP}/{player2.GetMaxHP()}");
        }
        else
        {
            Debug.LogWarning("Player 2 non assegnato!");
        }

        // Determina il vincitore
        if (p1HP > p2HP)
        {
            Debug.Log(">>> PLAYER 1 VINCE IL ROUND! <<<");
        }
        else if (p2HP > p1HP)
        {
            Debug.Log(">>> PLAYER 2 VINCE IL ROUND! <<<");
        }
        else
        {
            Debug.Log(">>> PAREGGIO! <<<");
        }
    }

    // Metodo pubblico per terminare il round anticipatamente (es. KO)
    public void ForceEndRound()
    {
        EndRound();
    }

    public float GetRemainingTime() => currentTime;
    public bool IsRoundEnded() => roundEnded;
}
