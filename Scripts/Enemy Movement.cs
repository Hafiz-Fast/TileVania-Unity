using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Tilemaps;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float movespeed = 1f;
    Rigidbody2D rb;
    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.velocity = new Vector2(movespeed, 0f);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        movespeed = -movespeed;
        FlipEnemy();
    }
    void FlipEnemy()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(rb.velocity.x)), 1f);
    }
}
