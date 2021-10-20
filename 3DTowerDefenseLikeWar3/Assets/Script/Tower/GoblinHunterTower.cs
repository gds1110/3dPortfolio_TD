using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinHunterTower : ArangeTower
{

    protected override void Action(Transform target)
    {
        base.Action(target);
       // target.GetComponent<EnemyCombat>().TakeBuff("Slow");
    }
}
