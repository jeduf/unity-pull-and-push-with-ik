using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoForPositionEmpty : MonoBehaviour
{
    public float radius;
    void OnDrawGizmosSelected()
    {
        // Draw a red sphere at the transform's position to casting distance.
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
