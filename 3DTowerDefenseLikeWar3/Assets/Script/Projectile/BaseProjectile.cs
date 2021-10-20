using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseProjectile : MonoBehaviour
{
    public Transform Tower { set; get; }
    public Transform Target { set; get; }

    public Vector3 TargetLocation { set; get; }
    public DamageInfo Damage { set; get; }
    public bool IsLockedOnTarget { set; get; }
    public float TimeToTarget { set; get; }
    

    private float transition = 0.0f;
    private bool isLaunched = false;

    public BaseProjectile()
    {
        IsLockedOnTarget = false;
        TimeToTarget = 1.0f;
    }

    protected virtual void Update()
    {
        if(!isLaunched)
        {
            return;
        }

        transition += Time.deltaTime / TimeToTarget;

        if(transition>=1.0f)
        {
            ReachTarget();
        }

        if(IsLockedOnTarget&&Target)
        {
            TargetLocation = Target.position;
        }
        transform.position = Vector3.Lerp(Tower.position, TargetLocation, transition);
    }
   protected virtual void ReachTarget()
    {
        if(Target)
        {
            //Target.SendMessage("OnDamage", Damage);

            if(Target==null)
            {
                Destroy(gameObject);
                return;
            }
            if(Target.gameObject.activeSelf==false)
            {
                Destroy(gameObject);
                return;
            }
            Target.GetComponent<EnemyCombat>().OnDamage(Damage);
        }
        Destroy(gameObject);
    }

    public virtual void Launch(Transform tower,Transform target,DamageInfo dmg)
    {
        isLaunched = true;
        Tower = tower;
        Target = target;
        TargetLocation = target.position+new Vector3(0,1f,0);
        Damage = dmg;

       // Debug.Log("Launch To" + target.name);
    }
}
