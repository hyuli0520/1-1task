using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{
    Image healthBar;
    public float maxHealth;
    public static float health;

    public Text healthText;

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
        if (health > maxHealth)
        {
            health = 100;
        }
        healthBar.fillAmount = (health * 0.01f) / (maxHealth * 0.01f);

        healthText.text = health.ToString();
    }
}