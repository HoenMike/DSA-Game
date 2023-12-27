using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class Reposition : MonoBehaviour
{
    Collider2D collisionArea;

    void Awake()
    {
        collisionArea = GetComponent<Collider2D>();
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area"))
            return;

        Vector2 playerPosition = GameManager.instance.player.transform.position;
        Vector2 myPosition = transform.position;

        float diffX = Mathf.Abs(playerPosition.x - myPosition.x);
        float diffY = Mathf.Abs(playerPosition.y - myPosition.y);

        Vector2 playerDirection = GameManager.instance.player.inputVector;
        float directionX = playerDirection.x < 0 ? -1 : 1;
        float directionY = playerDirection.y < 0 ? -1 : 1;

        switch (transform.tag)
        {
            case "Ground":
                if (diffX > diffY)
                {
                    transform.Translate(Vector2.right * directionX * 40f);
                }
                else if (diffX < diffY)
                {
                    transform.Translate(Vector2.up * directionY * 40f);
                }
                break;
            case "Enemy":
                if (collisionArea.enabled)
                {
                    transform.Translate(playerDirection * 20f + new Vector2(Random.Range(-3f, 3f), Random.Range(-3f, 3f)));
                }
                break;
        }

    }
}
