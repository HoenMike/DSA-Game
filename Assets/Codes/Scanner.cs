/* Name: #20
 Mai Nguyen Hoang - ITITIU21208
 Purpose: A vampire survivors clone that implements DSA.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//* Handle scan function to get nearest target *//
public class Scanner : MonoBehaviour
{
    //* GameObjects *//
    public LayerMask targetLayer;
    public RaycastHit2D[] targets;
    public Transform nearestTarget;

    //* Variables *//
    public float scanRange;

    //* Unity's Functions *//
    // FixedUpdate is called at a fixed interval and is independent of frame rate (use for physics calculations)
    void FixedUpdate()
    {
        targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0, targetLayer);
        nearestTarget = GetNearestTarget();
    }

    //* Custom Functions *//
    Transform GetNearestTarget()
    {
        Transform aimTarget = null;
        float diff = 100;

        foreach (RaycastHit2D hit in targets)
        {
            Vector2 myPosition = transform.position;
            Vector2 targetPosition = hit.transform.position;

            float currDiff = Vector2.Distance(myPosition, targetPosition);

            if (currDiff < diff)
            {
                diff = currDiff;
                aimTarget = hit.transform;
            }
        }

        return aimTarget;
    }
}
