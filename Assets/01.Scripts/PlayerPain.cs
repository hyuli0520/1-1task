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
    
    public GameManager manager;
    public Text painText;

    void Start()
    {
        maxPain = 100;
        pain = 10;
        if (manager.stage == 1)
        {
            pain = 10;
        }
        else if (manager.stage == 2)
        {
            pain = 30;
        }
    }

    void Awake()
    {
        painBar = GetComponent<Image>();
    }

    void Update()
    {
        painBar.fillAmount = (pain * 0.01f) / (maxPain * 0.01f);

        painText.text = pain.ToString();
    }
}
