using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyOffsetData
{

}

public class EnemyCombat : BaseCombat
{
    [SerializeField]
    public EnemyData enemyData;
    public Enemy enemy = new Enemy();
    public NavMeshAgent agent;
    public SkinnedMeshRenderer Skin;
    public MeshRenderer[] MeshRenderers;
    private Color originColor;
    private float originSpd;


    public override void InitCombat()
    {
        base.InitCombat();

        Skin = GetComponentInChildren<SkinnedMeshRenderer>();
        MeshRenderers = GetComponentsInChildren<MeshRenderer>();
        agent = GetComponent<NavMeshAgent>();
        originColor = Skin.material.color;
        enemy.index = enemyData.Index;
        MaxHitpoint = enemyData.HP;
        agent.speed = enemyData.Spd;
        hitpoint = maxHitpoint;
        def = enemyData.Def;
        healthBar.fillAmount = 1;
        originSpd = enemyData.Spd;

    }
    public void RestEnemy()
    {
        enemy.index = enemyData.Index;
        MaxHitpoint = enemyData.HP;
        //agent.speed = enemyData.Spd;
        hitpoint = maxHitpoint;
        def = enemyData.Def;
        healthBar.fillAmount = 1;
    }

    public void AddoffsetData(WaveInfo offset)
    {
        agent.speed += offset.OffsetSpd;
        def += offset.OffsetDef;
        maxHitpoint += offset.OffsetHp;
        hitpoint = maxHitpoint;
    }

    public void OnCross()
    {
        SpawnManager.Instance.DestroyEnemyInpool(this.gameObject, enemyData.Index);
    }

    public override void OnDeath()
    {
        EffectManager.Instance.Show(transform.position, Vector3.up, 1.0f, Effect.EffectType.SKULL);
        GameManager.Instance.GetGold(enemyData.DropGold);
        // SpawnManager.Instance.DestroyEnemy(this.gameObject);
        //GameManager.Instance.GetGold(enemyData.DropGold);
        anim.SetTrigger("Dead");
        SpawnManager.Instance.DestroyEnemyInpool(this.gameObject,enemyData.Index);
       // this.gameObject.SetActive(false);
    }

    public void GOpool()
    {
        SpawnManager.Instance.DestroyEnemyInpool(this.gameObject, enemyData.Index);

    }

    public void TakeBuff(string name)
    {
        if (gameObject.activeSelf == true)
        {
            switch (name)
            {
                case "Poison":
                    StartCoroutine(BuffCoroutine("DotDmg", 5, 1f));
                    break;
                case "Slow":
                    StartCoroutine(BuffCoroutine("Slow", 5, 0.5f));
                    break;
                case "Rooted":
                    StartCoroutine(BuffCoroutine("Rooted", 5, 1f));
                    break;
                case null:
                    break;
            }
        }
    }
    IEnumerator BuffCoroutine(string operation,int time, float value)
    {
       originSpd= agent.speed;
        
        //버프아이콘?
        while(time>0)
        {
            if(operation=="DotDmg")
            {
                // hitpoint -= (int)value;
                DamageInfo dmg = new DamageInfo();
                dmg.ammount = (int)value/time;
                if(dmg.ammount<=0)
                {
                    dmg.ammount = 1;
                }

                Skin.material.color = Color.green;
                //MeshRenderers.material.color = Color.green;
                MeshrenderColorChange(Color.green);
                Debug.Log("현재 효과 : 독 , 남은 시간 :" + time);
                OnDamage(dmg);
            }
            else if(operation=="Rooted")
            {
                Skin.material.color = Color.black;
               // MeshRenderers.material.color = Color.black;
                MeshrenderColorChange(Color.black);
                Debug.Log("현재 효과 : 속박 , 남은 시간 :" + time);

                agent.speed = 0;
            }
            else if(operation=="Slow")
            {
                Skin.material.color = Color.blue;
               // MeshRenderers.material.color = Color.blue;
                MeshrenderColorChange(Color.blue);
                Debug.Log("현재 효과 : 슬로우 , 남은 시간 :" + time);

                agent.speed = 0.5f;
            }
            time--;
            yield return new WaitForSeconds(1);
        }
        if (operation == "Rooted" || operation == "Slow")
        {
            agent.speed = originSpd;
        }
        Skin.material.color = originColor;
        //MeshRenderers.material.color = originColor;
        MeshrenderColorChange(originColor);

    }
    private void MeshrenderColorChange(Color color)
    {
        foreach(var a in MeshRenderers)
        {
            a.material.color = color;
        }
    }

    private void OnDisable()
    {
        Skin.material.color = originColor;
        // MeshRenderers.material.color = originColor;
        MeshrenderColorChange(originColor);

        agent.speed = originSpd;
        StopCoroutine("BuffCoroutine");
    }

}
