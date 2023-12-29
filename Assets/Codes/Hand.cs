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

    void Awake()
    {
        player = GetComponentsInParent<SpriteRenderer>()[1];
    }

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
