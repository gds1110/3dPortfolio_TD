using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName ="Enemy Data",menuName ="Scriptable Object/Enemy Data",order =int.MaxValue)]
public class EnemyData : ScriptableObject
{

    [SerializeField]
    private goType type = goType.ENEMY;

    [SerializeField]
    private Sprite icon;
    public Sprite Icon { get { return icon; } }

    [SerializeField]
    private GameObject enemyPrafab;
    public GameObject EnemyPrafab { get { return enemyPrafab; } }

    [SerializeField]
    private int index;
    public int Index { get { return index; } }

    [SerializeField]
    private string enemyName;
    public string EnemyName { get { return enemyName; } }

    [SerializeField]
    private int hp;
    public int HP { get { return hp; } }

    [SerializeField]
    private int def;
    public int Def { get { return def; } }


    [SerializeField]
    private float spd;
    public float Spd { get { return spd; } }

    [SerializeField]
    private int dropGold;
    public int DropGold { get { return dropGold; } }

}
