using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [Header("Audio Source")]
    public AudioSource musicSource;   // linkataan inspectorissa

    [Header("Music Clip")]
    public AudioClip backgroundMusic; // linkataan inspectorissa

    private void Awake()
    {
        // Singleton setup
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // musiikki jatkuu scenejen välillä
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic();
    }

    public void PlayMusic()
    {
        if (musicSource.clip != backgroundMusic)
        {
            musicSource.clip = backgroundMusic;
        }
        musicSource.loop = true;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void ChangeMusic(AudioClip newClip)
    {
        musicSource.Stop();
        musicSource.clip = newClip;
        musicSource.loop = true;
        musicSource.Play();
    }
}