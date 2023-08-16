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

    public GameObject bulletObjA;
    public GameObject bulletObjB;

    public GameManager manager;
    public PlayerHp playerHp;
    public Enemy enemy;
    public bool isUnbeatable;

    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer sprite;

    private void Start()
    {
        speed = 250f;
        maxShotDelay = 0.2f;
        maxPower = 5;
        power = 1;
    }

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        playerHp = GetComponent<PlayerHp>();
    }

    private void Update()
    {
        Move();
        Reload();
        Fire();
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
                GameObject bullet = Instantiate(bulletObjA, transform.position, transform.rotation);
                Rigidbody2D bulletrigid = bullet.GetComponent<Rigidbody2D>();
                bulletrigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
            case 2:
                GameObject bulletR = Instantiate(bulletObjA, transform.position + Vector3.right * 0.1f, transform.rotation);
                GameObject bulletL = Instantiate(bulletObjA, transform.position + Vector3.left * 0.1f, transform.rotation);
                Rigidbody2D bulletrigidR = bulletR.GetComponent<Rigidbody2D>();
                Rigidbody2D bulletrigidL = bulletL.GetComponent<Rigidbody2D>();
                bulletrigidR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                bulletrigidL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
            case 3:
                GameObject bulletC = Instantiate(bulletObjB, transform.position, transform.rotation);
                Rigidbody2D bulletrigidC = bulletC.GetComponent<Rigidbody2D>();
                bulletrigidC.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
            case 4:
                GameObject bulletRRR = Instantiate(bulletObjA, transform.position + Vector3.right * 0.35f, transform.rotation);
                GameObject bulletCCC = Instantiate(bulletObjB, transform.position, transform.rotation);
                GameObject bulletLLL = Instantiate(bulletObjA, transform.position + Vector3.left * 0.35f, transform.rotation);
                Rigidbody2D bulletrigidRRR = bulletRRR.GetComponent<Rigidbody2D>();
                Rigidbody2D bulletrigidCCC = bulletCCC.GetComponent<Rigidbody2D>();
                Rigidbody2D bulletrigidLLL = bulletLLL.GetComponent<Rigidbody2D>();
                bulletrigidRRR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                bulletrigidCCC.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                bulletrigidLLL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
            case 5:
                GameObject bulletCR = Instantiate(bulletObjB, transform.position + Vector3.right * 0.25f, transform.rotation);
                GameObject bulletCL = Instantiate(bulletObjB, transform.position + Vector3.left * 0.25f, transform.rotation);
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
        if (collision.gameObject.tag == "EnemyBullet")
        {
            if (isUnbeatable)
            {
                return;
            }
            enemy.PlayerBulletAtc();
            isUnbeatable = true;
            if (PlayerHp.health <= 0)
            {
                manager.GameOver();
            }
            else
            {
                manager.RespawnPlayer();
            }
            sprite.color = new Color(1, 1, 1, 0.5f);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            if (isUnbeatable)
            {
                return;
            }
            enemy.PlayerAtc();
            isUnbeatable = true;
            if (PlayerHp.health <= 0)
            {
                manager.GameOver();
            }
            else
            {
                manager.RespawnPlayer();
            }
            sprite.color = new Color(1, 1, 1, 0.5f);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Item")
        {
            Item item = collision.gameObject.GetComponent<Item>();
            switch (item.type)
            {
                case "Power":
                    if (power == maxPower)
                        score += 500;
                    else
                        power++;
                    break;
                case "Unbeatable":
                    isUnbeatable = true;
                    sprite.color = new Color(1, 1, 1, 0.5f);
                    manager.Unbeatable();
                    break;
            }
            Destroy(collision.gameObject);
        }
    }
}
