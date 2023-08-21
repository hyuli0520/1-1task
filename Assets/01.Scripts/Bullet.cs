using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public int dmg;
    public bool isRotate;

    void Update()
    {
        if(isRotate)
        {
            transform.Rotate(Vector3.forward * 10);
        }    
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "BorderBullet")
        {
            gameObject.SetActive(false);
        }
        else if(collision.gameObject.tag == "Player")
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null && !player.isUnbeatable)
            {
                if (gameObject.tag == "EnemyBullet")
                {
                    PlayerHp.health -= dmg;
                }
            }
        }
    }
}
