using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string type;
    public bool isDrop;

    Rigidbody2D rigid;

    public ObjectManager objectManager;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        isDrop = false;
    }

    void Update()
    {
        if (gameObject.tag == "Item")
        {
            ItemDown();
        }
    }

    void ItemDown()
    {
        rigid.velocity = new Vector2(0, -0.6f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.tag == "BloodItem")
        {
            if ((collision.gameObject.tag == "Player" || collision.gameObject.tag == "PlayerBullet") && isDrop == false)
            {
                if (collision.gameObject.tag == "PlayerBullet")
                {
                    Bullet bullet = collision.gameObject.GetComponent<Bullet>();
                    bullet.gameObject.SetActive(false);
                    gameObject.SetActive(false);
                    isDrop = true;
                }
                else
                {
                    gameObject.SetActive(false);
                    isDrop = true;
                }
                int ran = Random.Range(2, 20);
                if (ran < 6 && ran >=2)
                {
                    GameObject itemPower = objectManager.MakeObj("ItemPower");
                    itemPower.transform.position = transform.position;
                }
                else if (ran < 8 && ran >=6)
                {
                    GameObject itemPower = objectManager.MakeObj("ItemUnbeatable");
                    itemPower.transform.position = transform.position;
                }
                else if (ran < 10 && ran >= 8)
                {
                    GameObject itemPower = objectManager.MakeObj("ItemHealing");
                    itemPower.transform.position = transform.position;
                }
                else if (ran < 14 && ran >= 10)
                {
                    GameObject itemPower = objectManager.MakeObj("ItemCoin");
                    itemPower.transform.position = transform.position;
                }
                else if (ran < 18 && ran >= 14)
                {
                    GameObject itemPower = objectManager.MakeObj("ItemShootSpeed");
                    itemPower.transform.position = transform.position;
                }
                else if (ran < 20 && ran >= 18)
                {
                    GameObject itemPower = objectManager.MakeObj("ItemPainLess");
                    itemPower.transform.position = transform.position;
                }
            }
        }
        if (gameObject.tag == "WhiteBlood")
        {
            if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "PlayerBullet")
            {
                PlayerPain.pain += 10f;
                if (collision.gameObject.tag == "PlayerBullet")
                {
                    Bullet bullet = collision.gameObject.GetComponent<Bullet>();
                    bullet.gameObject.SetActive(false);
                    gameObject.SetActive(false);
                }
                else
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
