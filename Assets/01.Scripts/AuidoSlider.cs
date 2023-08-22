using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AuidoSlider : MonoBehaviour
{
    public Slider musicSlider;

    public void Start()
    {
        musicSlider.value = SoundManager.Instance.Volume;
    }

    public void SetMusicVolume(float volume)
    {
        SoundManager.Instance.Volume = volume;
    }
}
