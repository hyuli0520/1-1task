using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
    public Slider musicSlider;
    public Slider sfxSlider;

    public void Start()
    {
        musicSlider.value = SoundManager.Instance.Volume;
        sfxSlider.value = SoundManager.Instance.Volume;
    }

    public void SetMusicVolume(float volume)
    {
        SoundManager.Instance.Volume = volume;
    }

    public void SetSfxVolume(float volume)
    {
        SoundManager.Instance.SfxVolume = volume;
    }
}
