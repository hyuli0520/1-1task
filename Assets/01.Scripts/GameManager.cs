using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public string SceneName = "GameTitle";

    public string[] itemObjs;
    public string[] enemyObjs;
    public Transform[] spawnPoints;

    public float maxSpawnDelay;
    public float curSpawnDelay;
    public float maxItemDelay;
    public float curItemDelay;
    public int stage;
    public int killEnemy;

    public bool isBoss;

    public GameObject player;
    public SpriteRenderer playerSprite;
    public Text scoreText;
    public GameObject stage1Text;
    public GameObject stage2Text;
    public GameObject gameOverSet;
    public GameObject menuSet;
    public ObjectManager objectManager;
    public BossHp bossHpBar;

    public Animator Stage1Anim;
    public Animator Stage2Anim;

    void Start()
    {
        maxSpawnDelay = 5;
        maxItemDelay = 5;
        StartCoroutine(Stage1Start());
    }

    public override void Awake()
    {
        base.Awake();
        stage = 1;
        enemyObjs = new string[] { "EnemyS", "EnemyM", "EnemyL", "EnemyB" };
        itemObjs = new string[] { "ItemRedCell", "ItemWhiteCell" };
    }

    void Update()
    {
        Player playerLogic = player.GetComponent<Player>();
        curSpawnDelay += Time.deltaTime;
        curItemDelay += Time.deltaTime;

        if (curSpawnDelay > maxSpawnDelay)
        {
            if (killEnemy == 100 && !isBoss)
            {
                SpawnBoss();
                isBoss = true;
            }
            if (!isBoss)
            {
                SpawnEnemy();
                maxSpawnDelay = Random.Range(1.5f, 3f);
                curSpawnDelay = 0;
            }
        }
        if(curItemDelay > maxItemDelay)
        {
            SpawnItem();
            maxItemDelay = Random.Range(5f, 10f);
            curItemDelay = 0;
        }

        scoreText.text = string.Format("{0:n0}", playerLogic.score);

        if (Input.GetButtonDown("Cancel"))
        {
            if (menuSet.activeSelf)
            {
                menuSet.SetActive(false);
                Time.timeScale = 1f;
            }
            else
            {
                menuSet.SetActive(true);
                Time.timeScale = 0f;
            }
        }

        if (PlayerHp.health <= 0 || PlayerPain.pain >= 100)
        {
            GameOver();
        }
    }

    IEnumerator Stage1Start()
    {
        isBoss = false;
        yield return new WaitForSeconds(2f);
        stage1Text.SetActive(true);
        Stage1Anim.SetTrigger("On");
        yield return new WaitForSeconds(1f);
        stage1Text.SetActive(false);
    }

    IEnumerator Stage2Start()
    {
        isBoss = false;
        yield return new WaitForSeconds(2f);
        stage2Text.SetActive(true);
        Stage2Anim.SetTrigger("On");
        yield return new WaitForSeconds(1f);
        stage2Text.SetActive(false);
    }

    void SpawnBoss()
    {
        bossHpBar.gameObject.SetActive(true);
        int bossPoint = 4;
        GameObject boss = objectManager.MakeObj(enemyObjs[3]);
        bossHpBar.enemy = boss.GetComponent<Enemy>();
        boss.transform.position = spawnPoints[bossPoint].position;

        Rigidbody2D rd = boss.GetComponent<Rigidbody2D>();
        Enemy bossLogic = boss.GetComponent<Enemy>();
        bossLogic.player = player;
        bossLogic.objectManager = objectManager;
        rd.velocity = new Vector2(0, bossLogic.enemySpeed * (-1));
    }

    public void DieBoss()
    {
        if (stage > 2)
            return;
        else
        {
            stage++;
            killEnemy = 0;
            bossHpBar.gameObject.SetActive(false);
            StartCoroutine(Stage2Start());
        }
    }

    void SpawnEnemy()
    {
        killEnemy++;
        int ranEnemy = Random.Range(0, 3);
        int ranPoint = Random.Range(0, 9);
        GameObject enemy = objectManager.MakeObj(enemyObjs[ranEnemy]);
        enemy.transform.position = spawnPoints[ranPoint].position;

        Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
        Enemy enemyLogic = enemy.GetComponent<Enemy>();
        enemyLogic.player = player;
        enemyLogic.objectManager = objectManager;

        if (ranPoint == 5 || ranPoint == 6)
        {
            enemy.transform.Rotate(Vector3.back * 90);
            rigid.velocity = new Vector2(enemyLogic.enemySpeed * (-1), -1);
        }
        else if (ranPoint == 7 || ranPoint == 8)
        {
            enemy.transform.Rotate(Vector3.forward * 90);
            rigid.velocity = new Vector2(enemyLogic.enemySpeed, -1);
        }
        else
        {
            rigid.velocity = new Vector2(0, enemyLogic.enemySpeed * (-1));
        }

    }

    void SpawnItem()
    {
        int ItemNum = Random.Range(0, 2);
        int ranPoint = Random.Range(0, 9);
        GameObject item = objectManager.MakeObj(itemObjs[ItemNum]);
        item.transform.position = spawnPoints[ranPoint].position;

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

    // 1.5ÃÊ ¹«Àû
    public void RespawnPlayer()
    {
        StartCoroutine(RespawnPlayerExt());
    }

    IEnumerator RespawnPlayerExt()
    {
        playerSprite.color = new Color(1, 1, 1, 0.5f);
        yield return new WaitForSeconds(1.5f);
        playerSprite.color = new Color(1, 1, 1, 1);

        Player playerLogic = player.GetComponent<Player>();
        playerLogic.isUnbeatable = false;
    }

    public void Unbeatable()
    {
        StartCoroutine(UnbeatableExt());
    }

    IEnumerator UnbeatableExt()
    {
        playerSprite.color = new Color(1, 1, 1, 0.5f);
        yield return new WaitForSeconds(2.5f);
        playerSprite.color = new Color(1, 1, 1, 1);
        yield return new WaitForSeconds(0.5f);
        Player playerLogic = player.GetComponent<Player>();
        playerLogic.isUnbeatable = false;
    }

    public void GameOver()
    {
        gameOverSet.SetActive(true);
        player.SetActive(false);
    }

    public void GameOut()
    {
        SceneManager.LoadScene(SceneName);
    }

    public void GameExit()
    {
        Application.Quit();
    }

}
