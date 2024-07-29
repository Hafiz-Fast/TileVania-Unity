using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletspeed = 15f;
    Rigidbody2D rb;
    PlayerInput player;
    float xSpeed;
    static int count;
    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
        player=FindObjectOfType<PlayerInput>();
        xSpeed = player.transform.localScale.x * bulletspeed;
    }

    void Update()
    {
        rb.velocity = new Vector2(xSpeed, 0f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            count++;
            if (count == 4)
            {
                Destroy(collision.gameObject);
                count = 0;
            }
        }
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
