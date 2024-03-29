/* Name: #20
 Mai Nguyen Hoang - ITITIU21208
 Purpose: A vampire survivors clone that implements DSA.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

//* Repositions the object when it leaves the area *//
//* Used for ground and enemy *//
public class Reposition : MonoBehaviour
{
    //* GameObject *//
    Collider2D collisionArea;

    //* Unity's Functions *//
    // Awake is called when the script instance is being loaded.
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

        switch (transform.tag)
        {
            case "Ground": // when ground out of the screen, reposition it to the opposite side
                float diffX = playerPosition.x - myPosition.x;
                float diffY = playerPosition.y - myPosition.y;

                float directionX = diffX < 0 ? -1 : 1;
                float directionY = diffY < 0 ? -1 : 1;

                diffX = Mathf.Abs(diffX);
                diffY = Mathf.Abs(diffY);

                if (diffX > diffY)
                {
                    transform.Translate(Vector2.right * directionX * 40f);
                }
                else if (diffX < diffY)
                {
                    transform.Translate(Vector2.up * directionY * 40f);
                }
                break;
            case "Enemy": // when enemy out of the screen, relocate it to a random position (near the player)
                if (collisionArea.enabled)
                {
                    Vector2 distance = playerPosition - myPosition;
                    Vector2 random = new Vector2(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
                    transform.Translate(random + distance * 2);
                }
                break;
        }
    }
}
