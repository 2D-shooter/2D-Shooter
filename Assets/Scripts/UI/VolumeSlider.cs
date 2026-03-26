using UnityEngine;

public class VolumeSlider : MonoBehaviour
{
    public enum VolumeType { Master, Music, SFX }

    public VolumeType volumeType;
    public SoundMixerManager mixer;

    public void OnValueChanged(float value)
    {
        switch (volumeType)
        {
            case VolumeType.Master:
                mixer.SetMasterVolume(value);
                break;

            case VolumeType.Music:
                mixer.SetMusicVolume(value);
                break;

            case VolumeType.SFX:
                mixer.SetSoundFXVolume(value);
                break;
        }
    }
}