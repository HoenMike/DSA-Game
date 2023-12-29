/* Name: #20
 Mai Nguyen Hoang - ITITIU21208
 Purpose: A vampire survivors clone that implements DSA.
*/
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
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
    Collider2D col;
    Animator anim;
    SpriteRenderer sprite;
    WaitForFixedUpdate wait;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        wait = new WaitForFixedUpdate();
    }

    void FixedUpdate()
    {
        // if Enemy is not live do nothing;
        if (!isAlive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            return;


        UnityEngine.Vector2 direction = target.position - rb.position;
        UnityEngine.Vector2 nextVector = direction.normalized * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + nextVector);
        rb.velocity = UnityEngine.Vector2.zero;
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
        col.enabled = true;
        rb.simulated = true;
        sprite.sortingOrder = 6;
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
        if (!collision.CompareTag("Bullet") || !isAlive)
            return;

        health -= collision.GetComponent<Bullet>().damage;
        StartCoroutine(KnockBack());

        if (health > 0)
        {
            anim.SetTrigger("Hit");
        }
        else
        {
            isAlive = false;
            col.enabled = false;
            rb.simulated = false;
            sprite.sortingOrder = 1;

            anim.SetBool("Dead", true);
            GameManager.instance.kill++;
            GameManager.instance.GetExp();
        }


    }

    IEnumerator KnockBack()
    {
        yield return wait;
        UnityEngine.Vector2 playerPosition = GameManager.instance.player.transform.position;
        UnityEngine.Vector2 direction = (UnityEngine.Vector2)transform.position - playerPosition;

        rb.AddForce(direction.normalized * 3, ForceMode2D.Impulse);
    }
    void Dead()
    {
        gameObject.SetActive(false);
    }
}
