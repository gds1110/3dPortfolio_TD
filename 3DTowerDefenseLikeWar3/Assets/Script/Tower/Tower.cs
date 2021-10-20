using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TowerType
{
    Goblin,
    GoblinHunter,
    Dragon,
    Spyder,
    Golem
}

public abstract class Tower :MonoBehaviour
{
    [SerializeField]
    private TowerData towerData;
    public TowerData td { get { return towerData; } }

    [SerializeField]
    protected TowerType type;
    public int index;
    [SerializeField]
    protected string Names;
    [SerializeField]
    protected int Dmg;
    public int dmgamount { get { return Dmg; } }
    [SerializeField]
    protected DamageInfo dmg;

    [SerializeField]
    protected float Spd;
    protected float Arange;
    [SerializeField]
    protected float range = 5.0f;  //사정거리

    protected float lastTick;
    protected float refreshRate = 0.10f;
    protected float lastAction;
    [SerializeField]
    protected float coolDown = 1.0f;  //공격속도
    public float cooldown { get { return coolDown; } }
    [SerializeField]
    public int needGold = 0;

    public NodeGo node;


    protected TowerAnim towerAnim;

    public delegate void ChainFunction(int value);
    ChainFunction chain;

    public int Level;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }


    protected virtual void Start()
    {
        Level = 1;
        towerAnim = GetComponent<TowerAnim>();
        TowerInit(towerData);
    }


    private void Update()
    {
        if(Time.time-lastAction>coolDown)
        {
            if(Time.time-lastTick>refreshRate)
            {

                lastTick = Time.time;

                Transform target = GetNearestEnemy();
                if(target != null)
                {

                    if (Vector3.Distance(target.position, transform.position) > range)
                    {
                        towerAnim.ChangeAni(TowerAnim.A_IDLE);

                        return;
                    }
                    Action(target);
                }
            }


         }
    
    }
    private Transform GetNearestEnemy()
    {
        Collider[] allEnemeis = Physics.OverlapSphere(transform.position, range,LayerMask.GetMask("Enemy"));

        if(allEnemeis.Length!=0)
        {
            int closetIndex = 0;
            float nearestDistance = Vector3.SqrMagnitude(transform.position - allEnemeis[0].transform.position);

            for(int i=1;i<allEnemeis.Length;i++)
            {
                float distance = Vector3.SqrMagnitude(transform.position - allEnemeis[i].transform.position);
                if(distance<nearestDistance)
                {
                    nearestDistance = distance;
                    closetIndex = i;
                }
            }
            return allEnemeis[closetIndex].transform;
        }

        return null;
    }

    protected virtual void Action(Transform target)
    {
        lastAction = Time.time;
        Debug.Log(gameObject.name + " is Shooting at " + target.name);
    }

    protected virtual void TowerInit(TowerData data)
    {
        dmg = new DamageInfo();
        Names = data.TowerName;
        index = data.Index;
        Dmg = data.Dmg;
        range = data.Range;
        coolDown = data.CoolDown;
        needGold = data.BuildGold;
        type = data.TowerTypes;
        dmg.ammount = data.Dmg;
    }

    public void LevelUp()
    {
        if (Level < 3)
        {
            if (GameManager.Instance.Gold >= Level * 2)
            {
                EffectManager.Instance.Show(transform.position, Vector3.zero, 1.0f, Effect.EffectType.LEVELUP);

                GameManager.Instance.Gold -= (Level * 2);
                Level += 1;
                Dmg += 1;
                dmg.ammount += 1;
                coolDown -= 0.1f;
            }
            else
            {
                UIManager.Instance.ShowGeneralMessage("골드가 부족합니다!", Color.red, 1.0F);
            }
        }
        else
        {
            UIManager.Instance.ShowGeneralMessage("최대 레벨을 충족했습니다!", Color.red, 1.0F);

            return;
        }
    }

    public void destroyTower()
    {
        node.DestroyTower();
    }

    public void OnMouseDown()
    {
        if(GameManager.Instance.SelObj==this.gameObject)
        {
            GameManager.Instance.SelObj = null;
        }
        else
        {
            GameManager.Instance.SeleGo(this.gameObject);

        }
    }
}
