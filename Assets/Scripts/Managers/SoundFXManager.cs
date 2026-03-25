using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    // Singleton
    public static SoundFXManager Instance;

    [Header("Audio Source")]
    public AudioSource sfxSource;

    [Header("Audio Clips")]
    public AudioClip shootClip;
    public AudioClip emptyGunClip;
    public AudioClip reloadClip;

    [Header("Melee Clips")]
public AudioClip fistSwingClip;
public AudioClip enemyHitFistClip;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Ampumisääni
    public void PlayShoot()
    {
        sfxSource.pitch = Random.Range(0.95f, 1.05f);
        sfxSource.PlayOneShot(shootClip);
    }

    // Tyhjä ase (klik)
    public void PlayEmptyGun()
    {
        sfxSource.pitch = 1f; // ei pitch randomia jos haluat realistisemman klikin
        sfxSource.PlayOneShot(emptyGunClip);
    }

    // Latausääni
    public void PlayReload()
    {
        sfxSource.pitch = 1f;
        sfxSource.PlayOneShot(reloadClip);
    }

    public void PlayFistSwing()
    {
        sfxSource.pitch = Random.Range(0.95f, 1.05f);
        sfxSource.PlayOneShot(fistSwingClip);
    }


    public void PlayFistHit()
    {
        sfxSource.pitch = Random.Range(0.95f, 1.05f);
        sfxSource.PlayOneShot(enemyHitFistClip);
    }
}