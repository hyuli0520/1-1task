using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string enemyName;
    public int enemyScore;
    public float enemySpeed;
    public int health;
    public float enemyDmg;
    public Sprite[] sprites;

    public float maxShotDelay;
    public float curShotDelay;

    public GameObject bulletObjA;
    public GameObject bulletObjB;
    public GameObject player;
    public ObjectManager objectManager;

    SpriteRenderer spriteRenderer;
    Rigidbody2D rigid;
    Animator anim;

    public int patternIndex;
    public int curPatternCount;
    public int[] maxPatternCount;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();

        if (enemyName == "B")
        {
            anim = GetComponent<Animator>();
        }
    }

    void OnEnable()
    {
        if (GameManager.Instance.stage == 1)
        {
            switch (enemyName)
            {
                case "B":
                    health = 1000;
                    Stop();
                    break;
                case "L":
                    health = 10;
                    break;
                case "M":
                    health = 5;
                    break;
                case "S":
                    health = 2;
                    break;
            }
        }
        else if (GameManager.Instance.stage == 2)
        {
            switch (enemyName)
            {
                case "B":
                    health = 1500;
                    Stop();
                    break;
                case "L":
                    health = 40;
                    break;
                case "M":
                    health = 20;
                    break;
                case "S":
                    health = 8;
                    break;
            }
        }
    }

    void Stop()
    {
        if (!gameObject.activeSelf)
            return;

        StartCoroutine(StopBoss());
    }

    IEnumerator StopBoss()
    {
        yield return new WaitForSeconds(2f);
        rigid.velocity = new Vector2(0, 0);
        StartCoroutine(Think(2f));
    }

    IEnumerator Think(float _time)
    {
        yield return new WaitForSeconds(_time);
        patternIndex = patternIndex == 3 ? 0 : patternIndex + 1;
        curPatternCount = 0;

        switch (patternIndex)
        {
            case 0:
                FireForward();
                break;
            case 1:
                FireShot();
                break;
            case 2:
                FireArc();
                break;
            case 3:
                FireAround();
                break;
        }
    }

    void FireForward()
    {
        GameObject bulletR = objectManager.MakeObj("BulletBossA");
        bulletR.transform.position = transform.position + Vector3.right * 0.3f;
        GameObject bulletRR = objectManager.MakeObj("BulletBossA");
        bulletRR.transform.position = transform.position + Vector3.right * 0.45f;
        GameObject bulletL = objectManager.MakeObj("BulletBossA");
        bulletL.transform.position = transform.position + Vector3.left * 0.3f;
        GameObject bulletLL = objectManager.MakeObj("BulletBossA");
        bulletLL.transform.position = transform.position + Vector3.left * 0.45f;

        Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidRR = bulletRR.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidLL = bulletLL.GetComponent<Rigidbody2D>();

        rigidR.AddForce(Vector2.down * 8, ForceMode2D.Impulse);
        rigidRR.AddForce(Vector2.down * 8, ForceMode2D.Impulse);
        rigidL.AddForce(Vector2.down * 8, ForceMode2D.Impulse);
        rigidLL.AddForce(Vector2.down * 8, ForceMode2D.Impulse);

        curPatternCount++;

        if (GameManager.Instance.stage == 1)
        {
            if (curPatternCount < maxPatternCount[patternIndex])
                Invoke("FireForward", 2);
            else
                StartCoroutine(Think(3f));
        }
        else
        {
            if (curPatternCount < maxPatternCount[patternIndex])
                Invoke("FireForward", 1);
            else
                StartCoroutine(Think(2f));
        }
    }
    void FireShot()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject bullet = objectManager.MakeObj("BulletEnemyB");
            bullet.transform.position = transform.position;

            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector2 dirVec = player.transform.position - transform.position;
            Vector2 ranVec = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(0f, 2f));
            dirVec += ranVec;
            rigid.AddForce(dirVec.normalized * 5, ForceMode2D.Impulse);
        }
        curPatternCount++;

        if (GameManager.Instance.stage == 1)
        {
            if (curPatternCount < maxPatternCount[patternIndex])
                Invoke("FireShot", 3f);
            else
                StartCoroutine(Think(3f));
        }
        else
        {
            if (curPatternCount < maxPatternCount[patternIndex])
                Invoke("FireShot", 2f);
            else
                StartCoroutine(Think(3f));
        }
    }
    void FireArc()
    {
        GameObject bullet = objectManager.MakeObj("BulletEnemyA");
        bullet.transform.position = transform.position;
        bullet.transform.rotation = Quaternion.identity;

        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        Vector2 dirVec = new Vector2(Mathf.Sin(Mathf.PI * 10 * curPatternCount / maxPatternCount[patternIndex]), -1);
        rigid.AddForce(dirVec.normalized * 5, ForceMode2D.Impulse);

        curPatternCount++;

        if (curPatternCount < maxPatternCount[patternIndex])
        {
            Invoke("FireArc", 0.15f);
        }
        else
            StartCoroutine(Think(3f));
    }
    void FireAround()
    {
        if (GameManager.Instance.stage == 1)
        {
            int roundNumA = 30;
            int roundNumB = 20;
            int roundNum = curPatternCount % 2 == 0 ? roundNumA : roundNumB;

            for (int index = 0; index < roundNum; index++)
            {
                GameObject bullet = objectManager.MakeObj("BulletBossB");
                bullet.transform.position = transform.position;
                bullet.transform.rotation = Quaternion.identity;

                Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
                Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 2 * index / roundNum),
                                             Mathf.Sin(Mathf.PI * 2 * index / roundNum));
                rigid.AddForce(dirVec.normalized * 2, ForceMode2D.Impulse);

                Vector3 rotVec = Vector3.forward * 360 * index / roundNum + Vector3.forward * 90;
                bullet.transform.Rotate(rotVec);
            }
        }
        else if (GameManager.Instance.stage == 2)
        {
            int roundNumA = 35;
            int roundNumB = 30;
            int roundNum = curPatternCount % 2 == 0 ? roundNumA : roundNumB;

            for (int index = 0; index < roundNum; index++)
            {
                GameObject bullet = objectManager.MakeObj("BulletBossB");
                bullet.transform.position = transform.position;
                bullet.transform.rotation = Quaternion.identity;

                Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
                Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 2 * index / roundNum),
                                             Mathf.Sin(Mathf.PI * 2 * index / roundNum));
                rigid.AddForce(dirVec.normalized * 2, ForceMode2D.Impulse);

                Vector3 rotVec = Vector3.forward * 360 * index / roundNum + Vector3.forward * 90;
                bullet.transform.Rotate(rotVec);
            }
        }

        curPatternCount++;

        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireAround", 0.7f);
        else
            StartCoroutine(Think(3f));
    }

    private void Update()
    {
        if (enemyName == "B")
            return;

        Reload();
        Fire();
    }

    private void Fire()
    {
        if (curShotDelay < maxShotDelay)
            return;

        if (enemyName == "S")
        {

        }
        else if (enemyName == "L")
        {
            GameObject bulletR = objectManager.MakeObj("BulletEnemyB");
            bulletR.transform.position = transform.position + Vector3.right * 0.3f;
            GameObject bulletL = objectManager.MakeObj("BulletEnemyB");
            bulletL.transform.position = transform.position + Vector3.left * 0.3f;

            Rigidbody2D bulletrigidR = bulletR.GetComponent<Rigidbody2D>();
            Rigidbody2D bulletrigidL = bulletL.GetComponent<Rigidbody2D>();
            Vector3 dirVecR = player.transform.position - transform.position + Vector3.right * 0.3f;
            Vector3 dirVecL = player.transform.position - transform.position + Vector3.left * 0.3f;
            bulletrigidR.AddForce(dirVecR.normalized * 4, ForceMode2D.Impulse);
            bulletrigidL.AddForce(dirVecL.normalized * 4, ForceMode2D.Impulse);
        }

        curShotDelay = 0;
    }

    private void Reload()
    {
        curShotDelay += Time.deltaTime;
    }

    void OnHit(int dmg)
    {
        health -= dmg;
        if (enemyName == "B")
        {
            anim.SetTrigger("OnHit");
        }
        else
        {
            spriteRenderer.sprite = sprites[1];
            Invoke("ReturnSprite", 0.1f);
        }

        if (health <= 0)
        {
            Player playerLogic = player.GetComponent<Player>();
            playerLogic.score += enemyScore;
            gameObject.SetActive(false);
            transform.rotation = Quaternion.identity;
            if (enemyName == "B")
            {
                GameManager.Instance.DieBoss();
            }
        }
    }

    void ReturnSprite()
    {
        spriteRenderer.sprite = sprites[0];
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BorderBullet" && enemyName != "B")
        {
            PlayerPain.pain += enemyDmg / 2;
            gameObject.SetActive(false);
            transform.rotation = Quaternion.identity;
        }
        else if (collision.gameObject.tag == "PlayerBullet")
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            OnHit(bullet.dmg);
            bullet.gameObject.SetActive(false);
        }

        if (collision.gameObject.tag == "Player")
        {
            Player playerLogic = collision.gameObject.GetComponent<Player>();
            if (playerLogic != null && !playerLogic.isUnbeatable)
                PlayerHp.health -= enemyDmg / 2;
        }
    }
}
