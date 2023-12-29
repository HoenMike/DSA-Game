/* Name: #20
 Mai Nguyen Hoang - ITITIU21208
 Purpose: A vampire survivors clone that implements DSA.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public float scanRange;
    public LayerMask targetLayer;
    public RaycastHit2D[] targets;

    public Transform nearestTarget;

    void FixedUpdate()
    {
        targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0, targetLayer);
        nearestTarget = GetNearestTarget();
    }

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
