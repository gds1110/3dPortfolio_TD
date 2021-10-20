using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meeleTower : Tower
{
    protected bool IsSplash;
    public Splash splash;
    public Transform tempTarget;

    protected override void Action(Transform target)
    {

        if (Vector3.Distance(target.position, transform.position) > range)
        {
            towerAnim.ChangeAni(TowerAnim.A_IDLE);
            return;
        }
          towerAnim.ChangeAni(TowerAnim.A_ATTACK);

        if (!splash)
        {

            lastAction = Time.time;

           // tempTarget = target;
          

             target.SendMessage("OnDamage", dmg);

        }
        else
        {
            lastAction = Time.time;

           // splash.dmg = dmg;
            //스플래쉬 소환
            
        }
    }

    protected void attack()
    { 
    //{
    //    if(tempTarget!=null)
    //    {
    //        tempTarget.GetComponent<EnemyCombat>().SendMessage("OnDamage", dmg);
    //    }

    //    tempTarget = null;  
    }
    public void afterAttack()
    {
        tempTarget = null;
        towerAnim.ChangeAni(TowerAnim.A_IDLE);

    }


    protected override void TowerInit(TowerData data)
    {
        base.TowerInit(data);
        if(splash)
        {
            splash.dmg = dmg;
        }
    }

    protected virtual void SplashOn()
    {
        splash.gameObject.SetActive(true);
    }
}
