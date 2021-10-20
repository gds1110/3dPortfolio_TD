using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestColider : MonoBehaviour
{
    [SerializeField]
    private float rad;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rad);
    }
}
