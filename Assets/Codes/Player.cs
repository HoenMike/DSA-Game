/* Name: #20
 Mai Nguyen Hoang - ITITIU21208
 Purpose: A vampire survivors clone that implements DSA.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public UnityEngine.Vector2 inputVector;
    public float speed;
    public Scanner scanner;
    public Hand[] hands;

    Rigidbody2D rb;
    SpriteRenderer sprite;
    Animator animator;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
        hands = GetComponentsInChildren<Hand>(true);
    }

    // void Update()
    // {
    //     if (!GameManager.instance.isAlive)
    //         return;
    // }

    void FixedUpdate()
    {
        if (!GameManager.instance.isAlive) // if player is dead do nothing
            return;

        UnityEngine.Vector2 nextVector = inputVector.normalized * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + nextVector);
    }

    void OnMove(InputValue value)
    {
        inputVector = value.Get<Vector2>();
    }

    void LateUpdate()
    {
        if (!GameManager.instance.isAlive) // if player is dead do nothing
            return;

        animator.SetFloat("Speed", inputVector.magnitude);

        if (inputVector.x != 0)
        {
            sprite.flipX = inputVector.x < 0;
        }
    }

    void OnCollisionStay2D(Collision2D hitBox)
    {
        if (!GameManager.instance.isAlive) // if player is dead do nothing
            return;

        GameManager.instance.health -= Time.deltaTime * 10;

        if (GameManager.instance.health < 0)
        {
            for (int i = 2; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }

            animator.SetTrigger("Dead");
        }
    }
}
