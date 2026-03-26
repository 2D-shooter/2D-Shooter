using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [Header("Audio Source")]
    public AudioSource musicSource;

    [Header("Music Clips")]
    public AudioClip backgroundMusic;
    public AudioClip bossMusic;

    private Coroutine musicRoutine;
    private bool isBossMusicPlaying = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        PlayNormalMusic();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ResetToNormal();
    }

    // LISÄTTY startTime
    public void PlayMusic(AudioClip clip, float startTime = 0f)
    {
        if (musicRoutine != null)
            StopCoroutine(musicRoutine);

        musicRoutine = StartCoroutine(SwapMusic(clip, startTime));
    }

    public void PlayBossMusic()
    {
        isBossMusicPlaying = true;
        PlayMusic(bossMusic, 24f); // alkaa 24 sekunnista
    }

    public void PlayNormalMusic()
    {
        isBossMusicPlaying = false;
        PlayMusic(backgroundMusic);
    }

    public void StopMusic()
    {
        if (musicRoutine != null)
            StopCoroutine(musicRoutine);

        musicSource.Stop();
    }

    public void ResetToNormal()
    {
        isBossMusicPlaying = false;
        PlayNormalMusic();
    }

    // LISÄTTY startTime coroutineen
    private IEnumerator SwapMusic(AudioClip newClip, float startTime)
    {
        float startVolume = musicSource.volume;

        // fade out
        float t = 0f;
        while (t < 0.5f)
        {
            t += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(startVolume, 0f, t / 0.5f);
            yield return null;
        }

        // vaihda musiikki
        musicSource.clip = newClip;
        musicSource.loop = true;

        musicSource.time = startTime;

        musicSource.Play();

        // fade in
        t = 0f;
        while (t < 0.5f)
        {
            t += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(0f, startVolume, t / 0.5f);
            yield return null;
        }
    }

    private IEnumerator FadeVolume(float start, float end, float duration)
    {
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(start, end, timer / duration);
            yield return null;
        }

        musicSource.volume = end;
    }
}