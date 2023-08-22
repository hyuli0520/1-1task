using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectManager : MonoBehaviour
{
    public GameObject enemyBPrefab;
    public GameObject enemyLPrefab;
    public GameObject enemyMPrefab;
    public GameObject enemySPrefab;
    public GameObject itemPowerPrefab;
    public GameObject itemUnbeatPrefab;
    public GameObject itemHealingPrefab;
    public GameObject itemCoinPrefab;
    public GameObject itemPainLessPrefab;
    public GameObject itemShootSpeedPrefab;
    public GameObject itemRedCellPrefab;
    public GameObject itemWhiteCellPrefab;
    public GameObject bulletPlayerAPrefab;
    public GameObject bulletPlayerBPrefab;
    public GameObject bulletEnemyAPrefab;
    public GameObject bulletEnemyBPrefab;
    public GameObject bulletBossAPrefab;
    public GameObject bulletBossBPrefab;
 
    GameObject[] enemyB;
    GameObject[] enemyL;
    GameObject[] enemyM;
    GameObject[] enemyS;

    GameObject[] itemPower;
    GameObject[] itemUnbeat;
    GameObject[] itemHealing;
    GameObject[] itemCoin;
    GameObject[] itemPainLess;
    GameObject[] itemShootSpeed;

    GameObject[] itemRedCell;
    GameObject[] itemWhiteCell;

    GameObject[] bulletPlayerA;
    GameObject[] bulletPlayerB;
    GameObject[] bulletEnemyA;
    GameObject[] bulletEnemyB;
    GameObject[] bulletBossA;
    GameObject[] bulletBossB;

    void Awake()
    {
        enemyB = new GameObject[10];
        enemyL = new GameObject[10];
        enemyM = new GameObject[10];
        enemyS = new GameObject[20];

        itemPower = new GameObject[20];
        itemUnbeat = new GameObject[20];
        itemHealing = new GameObject[20];
        itemCoin = new GameObject[20];
        itemPainLess = new GameObject[20];
        itemShootSpeed = new GameObject[20];

        itemRedCell = new GameObject[20];
        itemWhiteCell = new GameObject[20];

        bulletPlayerA = new GameObject[100];
        bulletPlayerB = new GameObject[100];
        bulletEnemyA = new GameObject[100];
        bulletEnemyB = new GameObject[100];

        bulletBossA = new GameObject[50];
        bulletBossB = new GameObject[1000];

        Generate();
    }

    void Generate()
    {
        // #1. Enemy
        for (int index = 0; index < enemyB.Length; index++)
        {
            enemyB[index] = Instantiate(enemyBPrefab);
            enemyB[index].SetActive(false);
        }
        for (int index = 0; index< enemyL.Length; index++)
        {
            enemyL[index] = Instantiate(enemyLPrefab);
            enemyL[index].SetActive(false);
        }
        for (int index = 0; index < enemyM.Length; index++)
        {
            enemyM[index] = Instantiate(enemyMPrefab);
            enemyM[index].SetActive(false);
        }
        for (int index = 0; index < enemyS.Length; index++)
        {
            enemyS[index] = Instantiate(enemySPrefab);
            enemyS[index].SetActive(false);
        }

        // #2. Item
        for (int index = 0; index < itemPower.Length; index++)
        {
            itemPower[index] = Instantiate(itemPowerPrefab);
            itemPower[index].SetActive(false);
        }
        for (int index = 0; index < itemUnbeat.Length; index++)
        {
            itemUnbeat[index] = Instantiate(itemUnbeatPrefab);
            itemUnbeat[index].SetActive(false);
        }
        for (int index = 0; index < itemHealing.Length; index++)
        {
            itemHealing[index] = Instantiate(itemHealingPrefab);
            itemHealing[index].SetActive(false);
        }
        for (int index = 0; index < itemCoin.Length; index++)
        {
            itemCoin[index] = Instantiate(itemCoinPrefab);
            itemCoin[index].SetActive(false);
        }
        for (int index = 0; index < itemPainLess.Length; index++)
        {
            itemPainLess[index] = Instantiate(itemPainLessPrefab);
            itemPainLess[index].SetActive(false);
        }
        for (int index = 0; index < itemShootSpeed.Length; index++)
        {
            itemShootSpeed[index] = Instantiate(itemShootSpeedPrefab);
            itemShootSpeed[index].SetActive(false);
        }

        // #3. Blood Cell
        for (int index = 0; index < itemRedCell.Length; index++)
        {
            itemRedCell[index] = Instantiate(itemRedCellPrefab);
            itemRedCell[index].SetActive(false);
        }
        for (int index = 0; index < itemWhiteCell.Length; index++)
        {
            itemWhiteCell[index] = Instantiate(itemWhiteCellPrefab);
            itemWhiteCell[index].SetActive(false);
        }

        // #4. Bullet
        for (int index = 0; index < bulletPlayerA.Length; index++)
        {
            bulletPlayerA[index] = Instantiate(bulletPlayerAPrefab);
            bulletPlayerA[index].SetActive(false);
        }
        for (int index = 0; index < bulletPlayerB.Length; index++)
        {
            bulletPlayerB[index] = Instantiate(bulletPlayerBPrefab);
            bulletPlayerB[index].SetActive(false);
        }
        for (int index = 0; index < bulletEnemyA.Length; index++)
        {
            bulletEnemyA[index] = Instantiate(bulletEnemyAPrefab);
            bulletEnemyA[index].SetActive(false);
        }
        for (int index = 0; index < bulletEnemyB.Length; index++)
        {
            bulletEnemyB[index] = Instantiate(bulletEnemyBPrefab);
            bulletEnemyB[index].SetActive(false);
        }
        for (int index = 0; index < bulletBossA.Length; index++)
        {
            bulletBossA[index] = Instantiate(bulletBossAPrefab);
            bulletBossA[index].SetActive(false);
        }
        for (int index = 0; index < bulletBossB.Length; index++)
        {
            bulletBossB[index] = Instantiate(bulletBossBPrefab);
            bulletBossB[index].SetActive(false);
        }
    }

    public GameObject MakeObj(string type)
    {
        GameObject[] targetPool = null;

        switch (type)
        {
            case "EnemyB":
                targetPool = enemyB;
                break;
            case "EnemyL":
                targetPool = enemyL;
                break;
            case "EnemyM":
                targetPool = enemyM;
                break;
            case "EnemyS":
                targetPool = enemyS;
                break;

            case "ItemPower":
                targetPool = itemPower;
                break;
            case "ItemUnbeatable":
                targetPool = itemUnbeat;
                break;
            case "ItemHealing":
                targetPool = itemHealing;
                break;
            case "ItemCoin":
                targetPool = itemCoin;
                break;
            case "ItemPainLess":
                targetPool = itemPainLess;
                break;
            case "ItemShootSpeed":
                targetPool = itemShootSpeed;
                break;

            case "ItemRedCell":
                targetPool = itemRedCell;
                break;
            case "ItemWhiteCell":
                targetPool = itemWhiteCell;
                break;

            case "BulletPlayerA":
                targetPool = bulletPlayerA;
                break;
            case "BulletPlayerB":
                targetPool = bulletPlayerB;
                break;
            case "BulletEnemyA":
                targetPool = bulletEnemyA;
                break;
            case "BulletEnemyB":
                targetPool = bulletEnemyB;
                break;
            case "BulletBossA":
                targetPool = bulletBossA;
                break;
            case "BulletBossB":
                targetPool = bulletBossB;
                break;
            default:
                Debug.Log("아무 것도 없음");
                break;
        }

        for (int index = 0; index < targetPool.Length; index++)
        {
            if (!targetPool[index].activeSelf)
            {
                targetPool[index].SetActive(true);
                return targetPool[index];
            }
        }
        return null;
    }
}