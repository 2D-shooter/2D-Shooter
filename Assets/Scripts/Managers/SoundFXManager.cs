using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    // Singleton
    public static SoundFXManager Instance;

    [Header("Audio Source")]
    public AudioSource sfxSource;       // linkataan inspectorissa

    [Header("Audio Clips")]
    public AudioClip shootClip;         // ampumisääni inspectorissa

    private void Awake()
    {
        // Singleton setup
        if (Instance == null)
        {
            Instance = this;
            // Optionaalisesti: säilytetään scene changeissa
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Soita ampumisääni
    public void PlayShoot()
    {
        // Pieni satunnaisuus pitchissä, jotta äänet eivät kuulosta kopioiduilta
        sfxSource.pitch = Random.Range(0.95f, 1.05f);

        // TÄRKEÄ → mahdollistaa useita päällekkäisiä laukauksia
        sfxSource.PlayOneShot(shootClip);
    }
}