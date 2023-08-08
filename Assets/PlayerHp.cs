using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{
    Image healthBar;
    public float maxHealth;
    public static float health;

    void Awake()
    {
        healthBar = GetComponent<Image>();
    }

    void Start()
    {
        maxHealth = 100;
        health = maxHealth;
    }

    void Update()
    {
        healthBar.fillAmount = (health * 0.01f) / (maxHealth * 0.01f);
    }
}
