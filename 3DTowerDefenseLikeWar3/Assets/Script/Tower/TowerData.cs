using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Tower Data", menuName = "Scriptable Object/Tower Data", order = int.MaxValue)]

public class TowerData : ScriptableObject
{

    [SerializeField]
    private goType type = goType.TOWER;

    [SerializeField]
    private Sprite icon;

    public Sprite Icon { get { return icon; } }


    [SerializeField]
    private GameObject towerPrefab;
    public GameObject TowerPrefab { get { return towerPrefab; } }

    [SerializeField]
    private GameObject projectilePrefab;
    public GameObject ProjectilePrefab { get { return projectilePrefab; } }

    [SerializeField]
    private int index;
    public int Index { get { return index; } }

    [SerializeField]
    private int dmg;
    public int Dmg { get { return dmg; } }

    [SerializeField]
    private string towerName;
    public string TowerName { get { return towerName; } }

    [SerializeField]
    private float range;
    public float Range { get { return range; } }

    [SerializeField]
    private float coolDown;
    public float CoolDown { get { return coolDown; } }

    [SerializeField]
    private int buildGold;
    public int BuildGold { get { return buildGold; } }

    [SerializeField]
    private TowerType towerType;
    public TowerType TowerTypes { get { return towerType; } }

}
