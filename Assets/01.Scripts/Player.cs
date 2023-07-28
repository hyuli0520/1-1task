using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    float h, v;
    public float speed;
    public int power;
    public float maxShotDelay;
    public float curShotDelay;

    public GameObject bulletLeveL1;
    public GameObject bulletLeveL2;

    Rigidbody2D rigid;
    Animator anim;

    private void Start()
    {
        speed = 250f;
        maxShotDelay = 0.2f;
        power = 1;
    }

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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
        if (!Input.GetMouseButtonDown(0))
            return;
        if (curShotDelay < maxShotDelay)
            return;
        switch (power)
        {
            case 1:
                GameObject bullet = Instantiate(bulletLeveL1, transform.position, transform.rotation);
                Rigidbody2D bulletrigid = bullet.GetComponent<Rigidbody2D>();
                bulletrigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
            case 2:
                break;
            case 3:
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
}
