using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowProjectile : BaseProjectile
{

    public override void Launch(Transform tower, Transform target, DamageInfo dmg)
    {
        base.Launch(tower, target, dmg);
        TimeToTarget = 0.5f;
        IsLockedOnTarget = true;
    }
    protected override void ReachTarget()
    {
        Target.GetComponent<EnemyCombat>().TakeBuff("Slow");

        base.ReachTarget();
    }

    protected virtual void Update()
    {
        base.Update();
        transform.LookAt(Tower);
    }
}
