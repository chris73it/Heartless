using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Volume_Settings : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Slider SFXSlider;
    [SerializeField] private Slider musicSlider;


    private float muteVolume = 0.0001f; // Adjust this value as needed for near-mute behavior.

    private void Start()
    {
        // Set the default volume here.
        float defaultVolume = 0.5f; // You can set any value between 0.0f and 1.0f as the default.
        volumeSlider.value = defaultVolume; // Set the slider value to the default.
        SFXSlider.value = defaultVolume; // Set the slider value to the default.
        musicSlider.value = defaultVolume; // Set the slider value to the default.



        SetVolume(); // Call the SetVolume method to apply the default volume.
        SetSFXVolume();
        SetMusicVolume();
    }

    public void SetVolume()
    {
        float volume = volumeSlider.value;

        if (volume <= 0)
        {
            // If the volume is at or below 0, set it to muteVolume (almost mute).
            myMixer.SetFloat("Volume", Mathf.Log10(muteVolume) * 20);
        }
        else
        {
            // Otherwise, set the volume normally.
            myMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
        }
    }
    public void SetSFXVolume()
    {
        float volume = SFXSlider.value;

        if (volume <= 0)
        {
            // If the volume is at or below 0, set it to muteVolume (almost mute).
            myMixer.SetFloat("SFX", Mathf.Log10(muteVolume) * 20);
        }
        else
        {
            // Otherwise, set the volume normally.
            myMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        }
    }
    public void SetMusicVolume()
    {
        float volume = musicSlider.value;

        if (volume <= 0)
        {
            // If the volume is at or below 0, set it to muteVolume (almost mute).
            myMixer.SetFloat("Music", Mathf.Log10(muteVolume) * 20);
        }
        else
        {
            // Otherwise, set the volume normally.
            myMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
        }
    }
}


