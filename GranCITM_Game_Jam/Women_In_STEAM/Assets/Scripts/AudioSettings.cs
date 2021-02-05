using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    public AudioMixer audio_mixer;
    public Slider music_slider;
    public Slider sfx_slider;

    void Start()
    {
        audio_mixer.GetFloat("music_volume", out float m);
        audio_mixer.GetFloat("sfx_volume", out float s);
        music_slider.value = m;
        sfx_slider.value = s;
    }
    public void SetMusicVolume(float m_volume)
    {
        audio_mixer.SetFloat("music_volume", m_volume);
    }
    public void SetSFXVolume(float sfx_volume)
    {
        audio_mixer.SetFloat("sfx_volume", sfx_volume);
    }

    public void SetFullscreen(bool is_fullscreen)
    {
        // Screen.fullScreen = is_fullscreen;
    }
}