using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] enemyObjs;
    public Transform[] spawnPoints;

    public float maxSpawnDelay;
    public float curSpawnDelay;

    public GameObject player;

    void Update()
    {
        curSpawnDelay += Time.deltaTime;

        if (curSpawnDelay > maxSpawnDelay)
        {
            SpawnEnemy();
            maxSpawnDelay = Random.Range(1f, 3f);
            curSpawnDelay = 0;
        }
    }

    void SpawnEnemy()
    {
        int ranEnemy = Random.Range(0, 3);
        int ranPoint = Random.Range(0, 9);

        GameObject enemy = Instantiate(enemyObjs[ranEnemy],
                                       spawnPoints[ranPoint].position,
                                       spawnPoints[ranPoint].rotation);
        Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
        Enemy enemyLogic = enemy.GetComponent<Enemy>();
        enemyLogic.player = player;

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

    public void RespawnPlayer()
    {
        Invoke("RespawnPlayerExt", 2f);
    }

    void RespawnPlayerExt()
    {
        player.transform.position = Vector3.down * 3.5f;
        player.SetActive(true);
    }

}
