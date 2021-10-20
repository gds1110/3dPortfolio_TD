using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    private GameObject poolingObjectPrefab;

    private Queue<GameObject> poolingObjectQueue = new Queue<GameObject>();

    private GameObject CreateNewObject()
    {
        var newGo = Instantiate(poolingObjectPrefab, transform);
        newGo.gameObject.SetActive(false);
        return newGo;
    }

    private void Initialize(int Count)
    {
        for(int i=0;i<Count;i++)
        {
            poolingObjectQueue.Enqueue(CreateNewObject());
        }

    }





}
