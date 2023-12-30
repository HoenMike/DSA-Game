/* Name: #20
 Mai Nguyen Hoang - ITITIU21208
 Purpose: A vampire survivors clone that implements DSA.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//* Bullet class (Also use for Enemy Cleaner that kill all the enemy when player Win) *//
public class Bullet : MonoBehaviour
{
    // GameObjects
    Rigidbody2D rb;

    // Variables
    public float damage;
    public int per;

    //* Unity's Function *//
    // Awake is called when the script instance is being loaded.
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void OnTriggerEnter2D(Collider2D collision) // when bullet hit enemy
    {
        if (!collision.CompareTag("Enemy") || per == -100)
            return;

        per--;

        if (per < 0)
        {
            rb.velocity = Vector3.zero;
            gameObject.SetActive(false);
        }
    }
    void OnTriggerExit2D(Collider2D collision) // method to clear bullet when it out of the screen
    {
        if (!collision.CompareTag("Area") || per == -100)
            return;

        gameObject.SetActive(false);
    }

    //* Custom Function *//
    public void Init(float damage, int per, Vector2 fireDirection)
    {
        this.damage = damage;
        this.per = per;

        if (per >= 0)
        {
            rb.velocity = fireDirection * 15f; // 15f is bullet speed
        }
    }
}
