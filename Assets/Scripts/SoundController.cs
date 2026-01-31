using UnityEngine;

public class SoundController : MonoBehaviour
{
    [Header("Audio Settings")]
    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioClip backgroundMusic;
    [SerializeField] private float baseVolume = 0.8f;
    [SerializeField] private float basePitch = 1f;

    private float currentPitch = 1f;

    private void Start()
    {
        // Se non è assegnato un AudioSource, creane uno
        if (musicAudioSource == null)
        {
            musicAudioSource = gameObject.AddComponent<AudioSource>();
        }

        musicAudioSource.volume = baseVolume;
        musicAudioSource.loop = true;
    }

    /// <summary>
    /// Inizia la riproduzione della musica di background
    /// </summary>
    public void PlayBackgroundMusic(AudioClip musicClip)
    {
        if (musicClip == null)
        {
            Debug.LogWarning("SoundController: AudioClip non assegnato!");
            return;
        }

        musicAudioSource.clip = musicClip;
        musicAudioSource.Play();
        currentPitch = basePitch;
        musicAudioSource.pitch = currentPitch;

        Debug.Log("SoundController: Musica di background avviata");
    }

    /// <summary>
    /// Inizia la riproduzione della musica assegnata in Inspector
    /// </summary>
    public void PlayBackgroundMusic()
    {
        PlayBackgroundMusic(backgroundMusic);
    }

    /// <summary>
    /// Modifica il pitch (velocità) della musica che sta playando
    /// pitch > 1 = più veloce e acuto
    /// pitch < 1 = più lento e grave
    /// </summary>
    public void StretchMusic(float pitchMultiplier)
    {
        if (musicAudioSource == null) return;

        currentPitch = Mathf.Clamp(basePitch * pitchMultiplier, 0.5f, 2f);
        musicAudioSource.pitch = currentPitch;

        Debug.Log($"SoundController: Pitch musica modificato a {currentPitch:F2}");
    }

    /// <summary>
    /// Reimposta il pitch della musica al valore base
    /// </summary>
    public void ResetMusicPitch()
    {
        currentPitch = basePitch;
        if (musicAudioSource != null)
            musicAudioSource.pitch = currentPitch;
    }

    /// <summary>
    /// Ferma la riproduzione della musica
    /// </summary>
    public void StopMusic()
    {
        if (musicAudioSource != null)
            musicAudioSource.Stop();
    }

    /// <summary>
    /// Pausa la musica
    /// </summary>
    public void PauseMusic()
    {
        if (musicAudioSource != null)
            musicAudioSource.Pause();
    }

    /// <summary>
    /// Riprende la musica
    /// </summary>
    public void ResumeMusic()
    {
        if (musicAudioSource != null)
            musicAudioSource.Play();
    }

    /// <summary>
    /// Modifica il volume della musica
    /// </summary>
    public void SetVolume(float volume)
    {
        if (musicAudioSource != null)
            musicAudioSource.volume = Mathf.Clamp01(volume);
    }

    public float GetCurrentPitch() => currentPitch;
    public bool IsPlaying() => musicAudioSource != null && musicAudioSource.isPlaying;
}

