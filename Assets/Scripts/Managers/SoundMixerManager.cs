using UnityEngine;
using UnityEngine.Audio;

public class SoundMixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    public void SetMasterVolume(float level)
    {
        level = Mathf.Clamp(level, 0.0001f, 1f);
        audioMixer.SetFloat("mastervolume", Mathf.Log10(level) *20f);
    }

    public void SetSoundFXVolume(float level)
    {
        level = Mathf.Clamp(level, 0.0001f, 1f);
        Debug.Log("LEVEL: " + level);
        audioMixer.SetFloat("soundfxvolume", Mathf.Log10(level) *20f);

        SoundFXManager.Instance.PlayShoot();
    }

    public void SetMusicVolume(float level)
    {
        level = Mathf.Clamp(level, 0.0001f, 1f);
        audioMixer.SetFloat("musicvolume", Mathf.Log10(level) *20f);
    }
}
