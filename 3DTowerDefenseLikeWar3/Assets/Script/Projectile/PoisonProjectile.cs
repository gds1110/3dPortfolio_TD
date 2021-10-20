using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonProjectile : BaseProjectile
{
    protected override void ReachTarget()
    {
        Target.GetComponent<EnemyCombat>().TakeBuff("Poison");

        base.ReachTarget();
    }

}
