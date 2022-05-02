using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class SoundOption : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider bGMSlider;
    public Slider sESlider;
    public Slider masterSlider;

    private void Start()
    {

        audioMixer.GetFloat("BGM_Volume", out float bgmVolume);
        bGMSlider.value = bgmVolume;
        audioMixer.GetFloat("SE_Volume", out float seVolume);
        sESlider.value = seVolume;
        audioMixer.GetFloat("Master_Volume", out float masterVolume);
        masterSlider.value = masterVolume;
    }

    public void SetBGM(float volume)
    {
        audioMixer.SetFloat("BGM_Volume", volume);
    }

    public void SetSE(float volume)
    {
        audioMixer.SetFloat("SE_Volume", volume);
    }

    public void SetMaster(float volume)
    {
        audioMixer.SetFloat("Master_Volume", volume);
    }
}
