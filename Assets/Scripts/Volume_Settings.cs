using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Volume_Settings : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Slider SFXSlider;
    [SerializeField] private Slider musicSlider;

    private float muteVolume = 0.0001f;

    private string volumePrefKey = "VolumeSetting";
    private string sfxPrefKey = "SFXSetting";
    private string musicPrefKey = "MusicSetting";

    private void Start()
    {
        LoadVolumeSettings();
    }

    private void LoadVolumeSettings()
    {
        float defaultVolume = PlayerPrefs.GetFloat(volumePrefKey, 0.5f);
        float defaultSFXVolume = PlayerPrefs.GetFloat(sfxPrefKey, 0.5f);
        float defaultMusicVolume = PlayerPrefs.GetFloat(musicPrefKey, 0.5f);

        volumeSlider.value = defaultVolume;
        SFXSlider.value = defaultSFXVolume;
        musicSlider.value = defaultMusicVolume;

        SetVolume();
        SetSFXVolume();
        SetMusicVolume();
    }

    public void SetVolume()
    {
        float volume = volumeSlider.value;
        PlayerPrefs.SetFloat(volumePrefKey, volume);
        ApplyVolume(volume, "Volume");
    }

    public void SetSFXVolume()
    {
        float volume = SFXSlider.value;
        PlayerPrefs.SetFloat(sfxPrefKey, volume);
        ApplyVolume(volume, "SFX");
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        PlayerPrefs.SetFloat(musicPrefKey, volume);
        ApplyVolume(volume, "Music");
    }

    private void ApplyVolume(float volume, string mixerParam)
    {
        if (volume <= 0)
        {
            myMixer.SetFloat(mixerParam, Mathf.Log10(muteVolume) * 20);
        }
        else
        {
            myMixer.SetFloat(mixerParam, Mathf.Log10(volume) * 20);
        }
    }
}


