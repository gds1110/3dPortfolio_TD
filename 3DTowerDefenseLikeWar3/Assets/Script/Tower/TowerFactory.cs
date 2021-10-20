using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract class TowerFactory : MonoBehaviour
{
    public abstract void CreateTower(Transform tran);
}
