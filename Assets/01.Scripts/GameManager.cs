using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public string SceneName = "GameTitle";

    public string[] enemyObjs;
    public Transform[] spawnPoints;

    public float maxSpawnDelay;
    public float curSpawnDelay;
    public int stage;

    public GameObject player;
    public SpriteRenderer playerSprite;
    public Text scoreText;
    public GameObject gameOverSet;
    public GameObject menuSet;
    public ObjectManager objectManager;

    void Start()
    {
        maxSpawnDelay = 5;
    }

    void Awake()
    {
        stage = 1;
        enemyObjs = new string[] { "EnemyS", "EnemyM", "EnemyL" };
    }

    void Update()
    {
        curSpawnDelay += Time.deltaTime;

        if (curSpawnDelay > maxSpawnDelay)
        {
            SpawnEnemy();
            maxSpawnDelay = Random.Range(1.5f, 3f);
            curSpawnDelay = 0;
        }

        Player playerLogic = player.GetComponent<Player>();
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

    void SpawnEnemy()
    {
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

    // 1.5�� ����
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
