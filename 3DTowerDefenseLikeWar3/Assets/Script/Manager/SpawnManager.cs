using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class SpawnManager : MonoSingleton<SpawnManager>
{

    public Transform spawnPoint;
    public List<EnemyData> enemyDatas = new List<EnemyData>();
    private List<GameObject> activeEnemies = new List<GameObject>();

    public Dictionary<int, List<GameObject>> Enemys2 = new Dictionary<int, List<GameObject>>();
    [SerializeField]
    public GameObject enemyContainers;






    public Dictionary<int, Queue<GameObject>> Enemys = new Dictionary<int, Queue<GameObject>>();

    public void Start()
    {
        //for(int i=0;i< enemyDatas.Count; i++)
        //{
        //    if (!Enemys.ContainsKey(i))
        //    {
        //        Queue<GameObject> e_q = new Queue<GameObject>();
        //        Enemys.Add(i, e_q);
        //    }
        //    for(int j=0;j<40;j++)
        //    {
        //        GameObject tempE;
        //        tempE = Instantiate(enemyDatas[i].EnemyPrafab, enemyContainers.transform, spawnPoint);

        //        tempE.SetActive(false);
        //        tempE.transform.SetParent(enemyContainers.transform);
        //        Enemys[i].Enqueue(tempE);
        //    }
        //}
    }

    public Transform goal;
    public void DestroyEnemy(GameObject go)
    {
        activeEnemies.Remove(go);
        Debug.Log(activeEnemies.Count);
    }

    public void DestroyEnemyInpool(GameObject enemy, int index)
    {

        //Debug.Log("Deadth");
        //Enemys[index].Enqueue(enemy);
        enemy.GetComponent<NavMeshAgent>().enabled = false;
        activeEnemies.Remove(enemy);
        Debug.Log(activeEnemies.Count);

        enemy.SetActive(false);
    }
    public void PoolSpawnOffset(int spawnIndex,WaveInfo offsetData)
    {
        if (!Enemys.ContainsKey(spawnIndex))
        {
            Queue<GameObject> e_q = new Queue<GameObject>();
            Enemys.Add(spawnIndex, e_q);
        }


        if (Enemys[spawnIndex].Count <= 0)
        {
            GameObject tempE;
            tempE = Instantiate(enemyDatas[spawnIndex].EnemyPrafab, enemyContainers.transform, spawnPoint);

            tempE.SetActive(false);
            tempE.transform.SetParent(enemyContainers.transform);
            Enemys[spawnIndex].Enqueue(tempE);
        }
        var var = Enemys[spawnIndex].Dequeue();
        var.transform.position = new Vector3(spawnPoint.position.x, 0, spawnPoint.position.z);

        var.GetComponent<NavMeshAgent>().enabled = true;
        var.SetActive(true);
        var.GetComponent<Moveable>().SendMessage("MovingToDestination");
        var.GetComponent<EnemyCombat>().RestEnemy();
        var.GetComponent<EnemyCombat>().AddoffsetData(offsetData);
        activeEnemies.Add(var);
        Debug.Log("ISpooling!");
    }

    public void PoolSpawnOffset2(int spawnIndex, WaveInfo offsetData)
    {
        if (!Enemys2.ContainsKey(spawnIndex))
        {
            List<GameObject> e_q = new List<GameObject>();
            Enemys2.Add(spawnIndex, e_q);
        }

        GameObject tempEnemy = Enemys2[spawnIndex].Find(c=>(!c.activeSelf));

        if (tempEnemy==null)
        {
            tempEnemy = Instantiate(enemyDatas[spawnIndex].EnemyPrafab, enemyContainers.transform, spawnPoint);

            tempEnemy.SetActive(false);
            tempEnemy.transform.SetParent(enemyContainers.transform);
            Enemys2[spawnIndex].Add(tempEnemy);

        }

        tempEnemy.transform.position = new Vector3(spawnPoint.position.x, 0, spawnPoint.position.z);

        tempEnemy.GetComponent<NavMeshAgent>().enabled = true;
        tempEnemy.SetActive(true);
        tempEnemy.GetComponent<Moveable>().SendMessage("MovingToDestination");
        tempEnemy.GetComponent<EnemyCombat>().RestEnemy();
        tempEnemy.GetComponent<EnemyCombat>().AddoffsetData(offsetData);
        activeEnemies.Add(tempEnemy);
        Debug.Log("ISpooling!");
    }

    public void PoolSpawn(int spawnPrefabIndex)
    {
        if(!Enemys.ContainsKey(spawnPrefabIndex))
        {
            Queue<GameObject> e_q = new Queue<GameObject>();
            Enemys.Add(spawnPrefabIndex, e_q);
        }
        if(Enemys[spawnPrefabIndex].Count<=0)
        {
            GameObject tempE;
            //tempE.index = spawnPrefabIndex;
            tempE = Instantiate(enemyDatas[spawnPrefabIndex].EnemyPrafab, enemyContainers.transform, spawnPoint) ;
           
            tempE.SetActive(false);
            tempE.transform.SetParent(enemyContainers.transform);
            //tempE.go.SetActive(false);
            Enemys[spawnPrefabIndex].Enqueue(tempE);
        }
        var var = Enemys[spawnPrefabIndex].Dequeue();
        var.transform.position = new Vector3(spawnPoint.position.x,0, spawnPoint.position.z);
       // var.GetComponent<EnemyCombat>().InitCombat();
        var.GetComponent<NavMeshAgent>().enabled = true;
        var.SetActive(true);
        var.SendMessage("MovingToDestination");
        //var.SendMessage("SetTarget", goal);
    }

    public void Spawn(int spawnPrefabIndex, int spawnPoint)
    {

    }
    public void Spawn(int spawnPrefabIndex)
    {
        var var = Instantiate(enemyDatas[spawnPrefabIndex].EnemyPrafab, spawnPoint);
        var.SendMessage("SetTarget",goal);
        //var.SendMessage("MovingToDestination");
    }
    public int GetLeftEnemys()
    {
        return activeEnemies.Count;
    }
    private void Update()
    {
    }
}
