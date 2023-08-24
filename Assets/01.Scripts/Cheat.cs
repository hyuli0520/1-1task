using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class Cheat : MonoBehaviour
{
    public string[] itemObjs;
    public float playerHealth;
    public float playerPain;

    public Player player;
    public SpriteRenderer playerSprite;
    public ObjectManager objectManager;
    public GameObject[] a;
    public GameObject changeHealth;
    public GameObject changePain;
    public InputField healthInput;
    public InputField painInput;

    void Awake()
    {
        itemObjs = new string[] { "ItemRedCell", "ItemWhiteCell" };
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
            Stage1();
        if (Input.GetKeyDown(KeyCode.Keypad1))
            Stage2();
        if (Input.GetKeyDown(KeyCode.Keypad2))
            PowerUp();
        if (Input.GetKeyDown(KeyCode.Keypad3))
            OnUnBeatalbe();
        if (Input.GetKeyDown(KeyCode.Keypad4))
            OffUnBeatalbe();
        if (Input.GetKeyDown(KeyCode.Keypad5))
            KillAllEnemy();
        if (Input.GetKeyDown(KeyCode.Keypad6))
            OnChangeHp();
        if (Input.GetKeyDown(KeyCode.Keypad7))
            OnChangePain();
        if (Input.GetKeyDown(KeyCode.Keypad8))
            SpawnBloodWhite();
        if (Input.GetKeyDown(KeyCode.Keypad9))
            SpawnBloodRed();
    }

    void Stage1()
    {
        GameManager.Instance.stage = 1;
        GameManager.Instance.StageStart1();
    }
    void Stage2()
    {
        GameManager.Instance.stage = 2;
        GameManager.Instance.StageStart2();
    }

    void PowerUp()
    {
        player.power++;
    }

    void OnUnBeatalbe()
    {
        player.isUnbeatable = true;
        playerSprite.color = new Color(1, 1, 1, 0.5f);
    }
    void OffUnBeatalbe()
    {
        player.isUnbeatable = false;
        playerSprite.color = new Color(1, 1, 1, 1);
    }

    void KillAllEnemy()
    {
        a = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < a.Length; i++)
        {
            a[i].SetActive(false);
        }
    }

    void OnChangeHp()
    {
        Time.timeScale = 0;
        changeHealth.SetActive(true);
    }
    void OnChangePain()
    {
        Time.timeScale = 0;
        changePain.SetActive(true);
    }

    public void ChangeHp()
    {
        Time.timeScale = 1;
        float.TryParse(healthInput.GetComponent<InputField>().text,out playerHealth);
        PlayerHp.health = playerHealth;
        changeHealth.SetActive(false);
    }
    public void ChangePain()
    {
        Time.timeScale = 1;
        float.TryParse(painInput.GetComponent<InputField>().text, out playerPain);
        PlayerPain.pain = playerPain;
        changePain.SetActive(false);
    }

    void SpawnBloodWhite()
    {
        int ranPoint = Random.Range(0, 9);
        GameObject item = objectManager.MakeObj(itemObjs[1]);
        item.transform.position = GameManager.Instance.spawnPoints[ranPoint].position;

        Rigidbody2D rigid = item.GetComponent<Rigidbody2D>();
        Item itemLogic = item.GetComponent<Item>();
        itemLogic.objectManager = objectManager;

        if (ranPoint == 5 || ranPoint == 6)
        {
            rigid.velocity = new Vector2(2 * (-1), -1);
        }
        else if (ranPoint == 7 || ranPoint == 8)
        {
            rigid.velocity = new Vector2(2, -1);
        }
        else
        {
            rigid.velocity = new Vector2(0, 3 * (-1));
        }
    }
    void SpawnBloodRed()
    {
        int ranPoint = Random.Range(0, 9);
        GameObject item = objectManager.MakeObj(itemObjs[0]);
        item.transform.position = GameManager.Instance.spawnPoints[ranPoint].position;

        Rigidbody2D rigid = item.GetComponent<Rigidbody2D>();
        Item itemLogic = item.GetComponent<Item>();
        itemLogic.objectManager = objectManager;

        if (ranPoint == 5 || ranPoint == 6)
        {
            rigid.velocity = new Vector2(2 * (-1), -1);
        }
        else if (ranPoint == 7 || ranPoint == 8)
        {
            rigid.velocity = new Vector2(2, -1);
        }
        else
        {
            rigid.velocity = new Vector2(0, 3 * (-1));
        }
    }
}