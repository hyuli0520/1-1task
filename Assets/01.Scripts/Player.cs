using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    float h, v;
    public int score;
    public float speed;
    public int maxPower;
    public int power;
    public float maxShotDelay;
    public float curShotDelay;
    public float timer;

    public GameObject bulletObjA;
    public GameObject bulletObjB;

    public GameManager gameManager;
    public ObjectManager objectManager;
    public bool isUnbeatable;

    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer sprite;

    private void Start()
    {
        speed = 250f;
        maxShotDelay = 0.7f;
        maxPower = 5;
        power = 1;
    }



    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer < 3f)
        {
            transform.position = Vector3.Lerp(new Vector3(0, -5, 0), new Vector3(0, -3, 0), timer / 3f);
        }
        else
        {
            Move();
            Reload();
            Fire();
        }
    }

    private void Move()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Horizontal") ||
           Input.GetButtonUp("Horizontal"))
        {
            anim.SetInteger("Input", (int)h);
        }
    }

    private void FixedUpdate()
    {
        rigid.velocity = new Vector2(h, v).normalized * speed * Time.deltaTime;
    }

    private void Fire()
    {
        if (!Input.GetMouseButton(0))
            return;
        if (curShotDelay < maxShotDelay)
            return;
        switch (power)
        {
            case 1:
                GameObject bullet = objectManager.MakeObj("BulletPlayerA");
                bullet.transform.position = transform.position;

                Rigidbody2D bulletrigid = bullet.GetComponent<Rigidbody2D>();
                bulletrigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
            case 2:
                GameObject bulletR = objectManager.MakeObj("BulletPlayerA");
                bulletR.transform.position = transform.position + Vector3.right * 0.1f;
                GameObject bulletL = objectManager.MakeObj("BulletPlayerA");
                bulletL.transform.position = transform.position + Vector3.left * 0.1f;


                Rigidbody2D bulletrigidR = bulletR.GetComponent<Rigidbody2D>();
                Rigidbody2D bulletrigidL = bulletL.GetComponent<Rigidbody2D>();
                bulletrigidR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                bulletrigidL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
            case 3:
                GameObject bulletC = objectManager.MakeObj("BulletPlayerB");
                bulletC.transform.position = transform.position;

                Rigidbody2D bulletrigidC = bulletC.GetComponent<Rigidbody2D>();
                bulletrigidC.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
            case 4:
                GameObject bulletRR = objectManager.MakeObj("BulletPlayerA");
                bulletRR.transform.position = transform.position + Vector3.right * 0.35f;
                GameObject bulletCC = objectManager.MakeObj("BulletPlayerB");
                bulletCC.transform.position = transform.position;
                GameObject bulletLL = objectManager.MakeObj("BulletPlayerA");
                bulletLL.transform.position = transform.position + Vector3.left * 0.35f;

                Rigidbody2D bulletrigidRR = bulletRR.GetComponent<Rigidbody2D>();
                Rigidbody2D bulletrigidCC = bulletCC.GetComponent<Rigidbody2D>();
                Rigidbody2D bulletrigidLL = bulletLL.GetComponent<Rigidbody2D>();
                bulletrigidRR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                bulletrigidCC.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                bulletrigidLL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
            case 5:
                GameObject bulletCR = objectManager.MakeObj("BulletPlayerB");
                bulletCR.transform.position = transform.position + Vector3.right * 0.25f;
                GameObject bulletCL = objectManager.MakeObj("BulletPlayerB");
                bulletCL.transform.position = transform.position + Vector3.left * 0.25f;
                Rigidbody2D bulletrigidCR = bulletCR.GetComponent<Rigidbody2D>();
                Rigidbody2D bulletrigidCL = bulletCL.GetComponent<Rigidbody2D>();
                bulletrigidCR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                bulletrigidCL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
        }

        curShotDelay = 0;
    }

    private void Reload()
    {
        curShotDelay += Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = GetComponent<Enemy>();
        if ((collision.gameObject.tag == "EnemyBullet" || collision.gameObject.tag == "Enemy"))
        {
            if (isUnbeatable)
                return;

            isUnbeatable = true;
            if (PlayerHp.health <= 0)
            {
                gameManager.GameOver();
            }
            else
            {
                gameManager.RespawnPlayer();
            }
            collision.gameObject.SetActive(false);
        }
        if (collision.gameObject.tag == "Item" || collision.gameObject.tag == "BloodItem")
        {
            Item item = collision.gameObject.GetComponent<Item>();
            switch (item.type)
            {
                case "Power":
                    if (power == maxPower)
                        score += 300;
                    else
                        power++;
                    break;
                case "Unbeatable":
                    isUnbeatable = true;
                    gameManager.Unbeatable();
                    break;
                case "Healing":
                    PlayerHp.health += 10;
                    break;
                case "Coin":
                    score += 1000;
                    break;
                case "PainLess":
                    PlayerPain.pain -= 15;
                    break;
                case "ShootSpeed":
                    if (maxShotDelay <= 0.2f)
                        score += 300;
                    else
                        maxShotDelay -= 0.1f;
                    break;
            }
            collision.gameObject.SetActive(false);
        }
    }
}
