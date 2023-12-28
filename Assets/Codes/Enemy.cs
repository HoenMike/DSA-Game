using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float speed;
    public float health;
    public float maxHealth;
    public RuntimeAnimatorController[] animCon;

    public Rigidbody2D target;

    bool isAlive;

    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sprite;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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
        if (!isAlive)
            return;

        sprite.flipX = target.position.x < rb.position.x;
    }

    void OnEnable()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        isAlive = true;
        health = maxHealth;
    }

    public void Init(SpawnData data)
    {
        anim.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet"))
            return;

        health -= collision.GetComponent<Bullet>().damage;

        if (health > 0)
        {

        }
        else
        {
            Dead();
        }


    }
    void Dead()
    {
        gameObject.SetActive(false);
    }
}
