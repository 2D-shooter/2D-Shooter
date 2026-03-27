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

[Header("Player Sounds")]
public AudioClip[] playerGruntClips;
public AudioClip[] playerDeathClips;

public float playerGruntCooldown = 0.3f;
private float lastPlayerGruntTime;

[Header("Enemy Sounds")]
public AudioClip[] gruntClips;
public AudioClip[] deathClips;

[Header("Enemy Settings")]
public float gruntCooldown = 0.3f;
private float lastGruntTime;
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

    public void PlayPlayerGrunt()
    {
        if (Time.time < lastPlayerGruntTime + playerGruntCooldown) return;

        if (playerGruntClips.Length == 0) return;

        lastPlayerGruntTime = Time.time;

        AudioClip clip = playerGruntClips[Random.Range(0, playerGruntClips.Length)];
        sfxSource.pitch = Random.Range(0.95f, 1.05f);
        sfxSource.PlayOneShot(clip);
    }

    public void PlayPlayerDeath()
    {
        if (playerDeathClips.Length == 0) return;

        AudioClip clip = playerDeathClips[Random.Range(0, playerDeathClips.Length)];
        sfxSource.pitch = Random.Range(0.9f, 1.1f);
        sfxSource.PlayOneShot(clip);
    }

    public void PlayEnemyGrunt(Vector3 position)
    {
        if (Time.time < lastGruntTime + gruntCooldown) return;
        if (gruntClips.Length == 0) return;

        lastGruntTime = Time.time;

        AudioClip clip = gruntClips[Random.Range(0, gruntClips.Length)];
        AudioSource.PlayClipAtPoint(clip, position, 1f);
    }

    public void PlayEnemyDeath(Vector3 position)
    {
        if (deathClips.Length == 0) return;

        AudioClip clip = deathClips[Random.Range(0, deathClips.Length)];
        AudioSource.PlayClipAtPoint(clip, position, 1f);
    }
}