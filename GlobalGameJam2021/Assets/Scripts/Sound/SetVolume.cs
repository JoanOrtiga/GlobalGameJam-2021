using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class SetVolume : MonoBehaviour
{
    [SerializeField] private AudioMixer musicMixer, sfxMixer;
    [SerializeField] private bool music;

    public void setVolume()
    {
        if (music)
        {
            SetMusicLevel(gameObject.GetComponent<Slider>().value);
        }
        else
        {
            SetSfxLevel(gameObject.GetComponent<Slider>().value);
        }
    }
    public void SetMusicLevel(float sliderValue)
    {
        if (sliderValue == 0) 
        {
            musicMixer.SetFloat("MusicVol", -80);
        }
        else
        {
            musicMixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
        }
    }
    public void SetSfxLevel(float sliderValue)
    {
        if (sliderValue == 0)
        {
            sfxMixer.SetFloat("SFXVol", -80);
        }
        else
        {
            sfxMixer.SetFloat("SFXVol", Mathf.Log10(sliderValue) * 20);
        }

    }
}
