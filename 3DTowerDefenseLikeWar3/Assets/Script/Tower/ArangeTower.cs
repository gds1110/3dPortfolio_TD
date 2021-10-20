using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArangeTower : Tower
{
    public GameObject projectile;
    public Transform tempTarget;
    public Transform sp;
    public ArangeTower()
    {
        //range = 8.0f;
        //coolDown = 0.7f;
    }

    protected override void Action(Transform target)
    {
        towerAnim.ChangeAni(TowerAnim.A_ATTACK);

        lastAction = Time.time;
        tempTarget = target;
        DamageInfo dmg = new DamageInfo();
        dmg.ammount = Dmg;        

    }
    

    public void ArrowLunch()
    {
        if (tempTarget != null)
        {
            GameObject arrow = Instantiate(projectile, sp.position, Quaternion.identity,gameObject.transform) as GameObject;
            DamageInfo dmg = new DamageInfo();
            dmg.ammount = Dmg;
            arrow.GetComponent<BaseProjectile>().Launch(sp, tempTarget, dmg);
        }
    }
    public void AfterLunch()
    {
        tempTarget = null;
        towerAnim.ChangeAni(TowerAnim.A_IDLE);

    }

    protected override void TowerInit(TowerData data)
    {
        base.TowerInit(data);
        projectile = data.ProjectilePrefab;
    }

}
