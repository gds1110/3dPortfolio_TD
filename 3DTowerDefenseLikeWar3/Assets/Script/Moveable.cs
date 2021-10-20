using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Moveable : MonoBehaviour
{
    NavMeshAgent agent;

    [SerializeField]
    Transform target;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

    }
    public void MovingToDestination()
    {
        agent.SetDestination(SpawnManager.Instance.goal.position);
    }

    
    public void SetTarget(Transform _target)
    {
        target = _target;
    }

}
