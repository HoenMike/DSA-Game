/* Name: #20
 Mai Nguyen Hoang - ITITIU21208
 Purpose: A vampire survivors clone that implements DSA.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//* Follows the player's position *//
public class Follow : MonoBehaviour
{
    RectTransform rect;
    // Awake is called when the script instance is being loaded.
    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    // FixedUpdate is called at a fixed interval and is independent of frame rate (use for physics calculations)
    void FixedUpdate()
    {
        rect.position = Camera.main.WorldToScreenPoint(GameManager.instance.player.transform.position);
    }
}
