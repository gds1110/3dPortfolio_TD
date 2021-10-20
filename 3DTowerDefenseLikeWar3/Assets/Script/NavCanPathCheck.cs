using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavCanPathCheck : MonoSingleton<NavCanPathCheck>
{

    public Transform target;
    private NavMeshAgent agent;
    private LineRenderer line;
    public List<Vector3> pathVector;

    // Start is called before the first frame update
    void Start()
    {

        pathVector = new List<Vector3>();
        line = GetComponent<LineRenderer>();
        agent = GetComponent<NavMeshAgent>();
        InvokeRepeating("CheckToPath", 1f, 1f);
        agent.speed = 0;
        agent.SetDestination(target.position);
        pathVector.Add(agent.transform.position);
      

    }
    private void Update()
    {
       DisplayLineDestination();
    }

    public void DisplayLineDestination()
    {
        if (agent.path.corners.Length < 2) return;

        int i = 1;
        while(i<agent.path.corners.Length)
        {
            line.positionCount = agent.path.corners.Length;
            for(int j=0;j<agent.path.corners.Length;j++)
            {
                line.SetPosition(j, agent.path.corners[j]);
            }
            i++;
        }
    }

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        foreach (var p in pathVector) {
            Gizmos.DrawSphere(p, 0.1f);
                }
    }

    void CheckToPath()
    {
        if(CanReachPosition(target.position))
        {
            //Debug.Log("CanGo");
            pathVector.Clear();
            pathVector.Add(agent.transform.position);

            foreach(var p in agent.path.corners)
            {
                pathVector.Add(p);
            }

        }
        else
        {


            UIManager.Instance.ShowGeneralMessage("길을 가로막고 있어 타워가 파괴됩니다!", Color.red, 1.0f);
            //Debug.Log("Can'tGo");
            Destroy(BuildManager.Instance.TowerList[BuildManager.Instance.TowerList.Count - 1]);

        }



    }
    public bool CheckPath()
    {
        if (CanReachPosition(target.position))
        {
            Debug.Log("Can go");
            return true;
        }
        else
        {
            Debug.Log("Can't go");

            return false;
        }
    }

    public bool CanReachPosition(Vector3 position)
    {
        NavMeshPath path = new NavMeshPath();
        agent.CalculatePath(position, path);
        return path.status == NavMeshPathStatus.PathComplete;
    }

    public bool CanReach()
    {
        NavMeshPath path = new NavMeshPath();
        agent.CalculatePath(target.position, path);
        return path.status == NavMeshPathStatus.PathComplete;

    }

    void ToggleLayer()
    {
        if (gameObject.layer == 30)
        {
            gameObject.layer = 31;
        }
        else if(gameObject.layer==31)
        {
            gameObject.layer = 30;
        }
    }
}
