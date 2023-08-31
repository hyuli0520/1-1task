using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public string SceneName = "GameTitle";
    public string GameSceneName = "GameStage";

    public string[] itemObjs;
    public string[] enemyObjs;
    public Transform[] spawnPoints;

    public float maxSpawnDelay;
    public float curSpawnDelay;
    public float maxItemDelay;
    public float curItemDelay;
    public int stage;
    public int killEnemy;
    public int itemGet;

    public bool isBoss;

    public GameObject player;
    public SpriteRenderer playerSprite;
    public Text scoreText;
    public GameObject stage1Text;
    public GameObject stage2Text;
    public GameObject gameOverSet;
    public GameObject gameComplete;
    public GameObject menuSet;
    public ObjectManager objectManager;
    public BossHp bossHpBar;
    public GameObject GameRank;
    public Item item;

    public Animator Stage1Anim;
    public Animator Stage2Anim;

    void Start()
    {
        maxSpawnDelay = 5;
        maxItemDelay = 5;
        GameClear();
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
        if (curItemDelay > maxItemDelay)
        {
            SpawnItem();
            maxItemDelay = Random.Range(3f, 6f);
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

        if (GameRank.activeSelf == true)
        {
            Time.timeScale = 0f;
        }
    }

    public void StageStart1()
    {
        StartCoroutine(Stage1Start());
        GameClear();
    }
    public void StageStart2()
    {
        GameClear();
    }

    IEnumerator Stage1Start()
    {
        yield return new WaitForSeconds(2f);
        stage1Text.SetActive(true);
        Stage1Anim.SetTrigger("On");
        isBoss = false;
        killEnemy = 0;
        yield return new WaitForSeconds(1f);
        stage1Text.SetActive(false);
    }

    IEnumerator Stage2Start()
    {
        yield return new WaitForSeconds(2f);
        stage2Text.SetActive(true);
        Stage2Anim.SetTrigger("On");
        isBoss = false;
        killEnemy = 0;
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
        if (stage == 2)
        {
            Time.timeScale = 0;
            gameComplete.SetActive(true);
        }
        else
        {
            stage++;
            bossHpBar.gameObject.SetActive(false);
            Invoke("GameClear", 2f);
            StartCoroutine(Stage2Start());
        }
    }

    void GameClear()
    {
        if (stage == 1)
        {
            PlayerHp.health = 100;
            PlayerPain.pain = 10;
        }
        else if (stage == 2)
        {
            PlayerHp.health = 100;
            PlayerPain.pain = 30;
        }
    }

    void GameCompleteSet()
    {
        gameComplete.SetActive(false);
        GameRank.SetActive(true);
    }

    void StageClear()
    {
        Player playerLogic = player.GetComponent<Player>();

        // 체력 게이지 추가 점수 획득
        if (PlayerHp.health > 80)
            playerLogic.score += 2000;
        else
            playerLogic.score += 1000;

        // 고통 게이지 추가 점수 획득
        if (PlayerPain.pain > 80)
            playerLogic.score += 2000;
        else
            playerLogic.score += 1000;

        // 아이템 추가 점수 획득
        if (itemGet > 5 && itemGet <= 15)
            playerLogic.score += 500;
        else if (itemGet > 15)
            playerLogic.score += 1500;
        else
            playerLogic.score += 100;
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
        GameObject _item = objectManager.MakeObj(itemObjs[ItemNum]);
        _item.transform.position = spawnPoints[ranPoint].position;
        

        Rigidbody2D rigid = _item.GetComponent<Rigidbody2D>();
        Item itemLogic = _item.GetComponent<Item>();
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

    // 1.5초 무적
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

    public void GoRanking()
    {
        GameRank.SetActive(true);
    }

    public void ReStart()
    {
        SceneManager.LoadScene(GameSceneName);
        GameRank.SetActive(false);
        Time.timeScale = 1f;
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
