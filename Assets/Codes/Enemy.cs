using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D target;

    bool isAlive = true;

    Rigidbody2D rb;
    SpriteRenderer sprite;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        // if Enemy is not live do nothing;
        if (!isAlive)
            return;


        Vector2 direction = target.position - rb.position;
        Vector2 nextVector = direction.normalized * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + nextVector);
        rb.velocity = Vector2.zero;
    }

    void LateUpdate()
    {
        // if Enemy is not live do nothing; 
        if (!isAlive)
            return;

        sprite.flipX = target.position.x < rb.position.x;
    }

    void OnEnable()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
    }
}
