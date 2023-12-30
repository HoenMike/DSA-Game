/* Name: #20
 Mai Nguyen Hoang - ITITIU21208
 Purpose: A vampire survivors clone that implements DSA.
*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public bool isLeft;
    public SpriteRenderer sprite;

    SpriteRenderer player;

    Vector2 rightPos = new Vector2(.35f, -.15f);
    Vector2 rightPosReverse = new Vector2(-.15f, -.15f);
    Quaternion lefRotate = Quaternion.Euler(0, 0, -35);
    Quaternion lefRotateReverse = Quaternion.Euler(0, 0, -135);

    // Awake is called when the script instance is being loaded.
    void Awake()
    {
        player = GetComponentsInParent<SpriteRenderer>()[1];
    }

    // LateUpdate is called once per frame, after all Update functions have been called.
    void LateUpdate()
    {
        bool isReverse = player.flipX;

        if (isLeft)
        {
            transform.localRotation = isReverse ? lefRotateReverse : lefRotate;
            sprite.flipY = isReverse;
            sprite.sortingOrder = isReverse ? 4 : 6;

        }
        else
        {
            transform.localPosition = isReverse ? rightPosReverse : rightPos;
            sprite.flipX = isReverse;
            sprite.sortingOrder = isReverse ? 6 : 4;
        }
    }
}
