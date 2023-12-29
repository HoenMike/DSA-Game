/* Name: #20
 Mai Nguyen Hoang - ITITIU21208
 Purpose: A vampire survivors clone that implements DSA.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int per;

    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Init(float damage, int per, Vector2 fireDirection)
    {
        this.damage = damage;
        this.per = per;

        if (per > -1)
        {
            rb.velocity = fireDirection * 15f; // 15f is bullet speed
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy") || per == -1)
            return;

        per--;

        if (per == -1)
        {
            rb.velocity = Vector3.zero;
            gameObject.SetActive(false);
        }

    }
}
