using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    private float volume = 1;
    public AudioSource audioSource;

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
}