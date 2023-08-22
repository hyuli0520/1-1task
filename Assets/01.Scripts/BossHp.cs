using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHp : MonoBehaviour
{
    Image BosshealthBar;
    public float maxBossHealth;
    public static float curBossHealth;

    public Text BosshealthTxt;
    public Enemy enemy;

    void Awake()
    {
        BosshealthBar = GetComponent<Image>();
        
    }

    void Start()
    {
        maxBossHealth = enemy.health;
    }

    void Update()
    {
        curBossHealth = enemy.health;

        BosshealthBar.fillAmount = (curBossHealth * 0.01f) / (maxBossHealth * 0.01f);

        BosshealthTxt.text = curBossHealth.ToString();
    }
}
