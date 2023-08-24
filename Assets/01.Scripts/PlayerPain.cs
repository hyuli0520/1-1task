using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPain : MonoBehaviour
{
    Image painBar;
    public float maxPain;
    public static float pain;

    public Text painText;

    void Start()
    {
        maxPain = 100;
        pain = 10;
    }

    void Awake()
    {
        painBar = GetComponent<Image>();
    }

    void Update()
    {
        if (pain < 0)
            pain = 0;
        if (pain > maxPain)
            pain = 100;
        
        painBar.fillAmount = (pain * 0.01f) / (maxPain * 0.01f);

        painText.text = pain.ToString();
    }
}
