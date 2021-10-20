using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackSpell : BaseSpell
{
    private float hitLength;
    private Transform hitOrigin;
    private LayerMask targetMask;

    public MeleeAttackSpell()
    {
        coolDown = 0.5f;
        //lastCast = Time.time - coolDown;
        hitLength = 3.0f;
        hitOrigin = transform;
        targetMask = LayerMask.GetMask("Enemy");
    }
    public override void Action()
    {
        DamageInfo dmg = new DamageInfo();
        dmg.ammount = 5;

        foreach(Collider c in Physics.OverlapSphere(hitOrigin.position,hitLength,targetMask))
        {
            c.SendMessage("OnDamage", dmg);
        }
    }
}
