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
    public int power;
    public float maxShotDelay;
    public float curShotDelay;

    public GameObject bulletObjA;
    public GameObject bulletObjB;

    public GameManager manager;
    public Enemy enemy;
    public bool isUnbeatable;

    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer sprite;

    private void Start()
    {
        speed = 250f;
        maxShotDelay = 0.2f;
        power = 3;
    }

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
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
                GameObject bulletRR = Instantiate(bulletObjA, transform.position + Vector3.right * 0.35f, transform.rotation);
                GameObject bulletCC = Instantiate(bulletObjB, transform.position, transform.rotation);
                GameObject bulletLL = Instantiate(bulletObjA, transform.position + Vector3.left * 0.35f, transform.rotation);
                Rigidbody2D bulletrigidRR = bulletRR.GetComponent<Rigidbody2D>();
                Rigidbody2D bulletrigidCC = bulletCC.GetComponent<Rigidbody2D>();
                Rigidbody2D bulletrigidLL = bulletLL.GetComponent<Rigidbody2D>();
                bulletrigidRR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                bulletrigidCC.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                bulletrigidLL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
            case 4:
                break;
            case 5:
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
    }
}
