/* Name: #20
 Mai Nguyen Hoang - ITITIU21208
 Purpose: A vampire survivors clone that implements DSA.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//* Handle player's logic *//
public class Player : MonoBehaviour
{
    //* GameObjects *//
    public UnityEngine.Vector2 inputVector;
    public Hand[] hands;
    public RuntimeAnimatorController[] animCon;
    public Scanner scanner;
    Rigidbody2D rb;
    SpriteRenderer sprite;
    Animator animator;

    //* Variables *//
    public float speed;

    //* Unity's Functions *//
    // Awake is called when the script instance is being loaded.
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
        hands = GetComponentsInChildren<Hand>(true);
    }
    void OnEnable()
    {
        speed *= Character.Speed;
        animator.runtimeAnimatorController = animCon[GameManager.instance.playerId];
    }
    // FixedUpdate is called at a fixed interval and is independent of frame rate (use for physics calculations)
    void FixedUpdate()
    {
        if (!GameManager.instance.isAlive) // if player is dead do nothing
            return;

        UnityEngine.Vector2 nextVector = inputVector.normalized * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + nextVector);
    }
    // LateUpdate is called once per frame, after all Update functions have been called.
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
    void OnCollisionStay2D()
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
            GameManager.instance.GameOver();
        }
    }
    void OnMove(InputValue value) // Special InputSystem function
    {
        inputVector = value.Get<Vector2>();
    }
}
