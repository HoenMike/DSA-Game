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
    // GameObjects
    Rigidbody2D rb;
    Collider2D col;
    Animator anim;
    SpriteRenderer sprite;
    public RuntimeAnimatorController[] animCon;
    public Rigidbody2D target;

    // Variables
    public float speed;
    public float health;
    public float maxHealth;
    bool isAlive;
    WaitForFixedUpdate wait;

    //* Unity's Function *//
    // Awake is called when the script instance is being loaded.
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        wait = new WaitForFixedUpdate();
    }
    //FixedUpdate is called at a fixed interval and is independent of frame rate (use for physics calculations)
    void FixedUpdate()
    {
        if (!GameManager.instance.isAlive) // if player is dead do nothing
            return;
        if (!isAlive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit")) // if Enemy is dead do nothing
            return;
        UnityEngine.Vector2 direction = target.position - rb.position;
        UnityEngine.Vector2 nextVector = direction.normalized * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + nextVector);
        rb.velocity = UnityEngine.Vector2.zero;
    }
    // LateUpdate is called once per frame, after all Update functions have been called.
    void LateUpdate()
    {
        if (!GameManager.instance.isAlive) // if player is dead do nothing
            return;
        if (!isAlive)
            return;
        sprite.flipX = target.position.x < rb.position.x;
    }
    // OnEnable is called when the object becomes enabled and active.
    void OnEnable()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        isAlive = true;
        col.enabled = true;
        rb.simulated = true;
        sprite.sortingOrder = 6;
        health = maxHealth;
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
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Hit);
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

            if (GameManager.instance.isAlive)
                AudioManager.instance.PlaySfx(AudioManager.Sfx.Dead);
        }
    }

    //* Custom Function *//
    public void Init(SpawnData data)
    {
        anim.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }
    IEnumerator KnockBack()
    {
        yield return wait;
        UnityEngine.Vector2 playerPosition = GameManager.instance.player.transform.position;
        UnityEngine.Vector2 direction = (UnityEngine.Vector2)transform.position - playerPosition;

        rb.AddForce(direction.normalized * 0.2f, ForceMode2D.Impulse);
    }
    void Dead()
    {
        gameObject.SetActive(false);
    }
}
