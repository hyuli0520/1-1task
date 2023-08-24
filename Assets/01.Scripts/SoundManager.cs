using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    private float volume = 0.5f;
    public AudioSource audioSource;
    public AudioSource SfxSource;

    public override void Awake()
    {
        base.Awake();
        if (transform.parent != null && transform.root != null)
        {
            DontDestroyOnLoad(this.transform.root.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
    public float Volume
    {
        get
        {
            return volume;
        }
        set
        {
            volume = value;
            audioSource.volume = value;
        }
    }
    public float SfxVolume
    {
        get
        {
            return volume;
        }
        set
        {
            volume = value;
            SfxSource.volume = value;
        }
    }
    public void OnSfx()
    {
        SfxSource.Play();
    }
}