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
        Transform nearestTarget = null;
        float diff = 100;

        foreach (RaycastHit2D hit in targets)
        {
            Vector2 myPosition = transform.position;
            Vector2 targetPosition = hit.transform.position;

            float currDiff = Vector2.Distance(myPosition, targetPosition);

            if (currDiff < diff)
            {
                diff = currDiff;
                nearestTarget = hit.transform;
            }
        }

        return nearestTarget;
    }
}
