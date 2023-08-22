using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string type;
    Rigidbody2D rigid;

    public ObjectManager objectManager;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
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
            if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "PlayerBullet")
            {
                int ran = Random.Range(0, 10);
                if (ran < 1)
                    return;
                else if (ran < 3)
                {
                    GameObject itemPower = objectManager.MakeObj("ItemPower");
                    itemPower.transform.position = transform.position;
                }
                else if (ran < 6)
                {
                    GameObject itemPower = objectManager.MakeObj("ItemUnbeatable");
                    itemPower.transform.position = transform.position;
                }
                else if (ran < 9)
                {
                    GameObject itemPower = objectManager.MakeObj("ItemHealing");
                    itemPower.transform.position = transform.position;
                }
                Bullet bullet = collision.gameObject.GetComponent<Bullet>();
                bullet.gameObject.SetActive(false);
                gameObject.SetActive(false);
            }
        }
        if (gameObject.tag == "WhiteBlood")
        {
            if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "PlayerBullet")
            {
                PlayerPain.pain += 10f;
                Bullet bullet = collision.gameObject.GetComponent<Bullet>();
                bullet.gameObject.SetActive(false);
                gameObject.SetActive(false);
            }
        }
    }
}
