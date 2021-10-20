using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class debuff 
{
    protected GameObject target;
    
   public debuff(GameObject _target)
    {
        target = _target;
    }

    public virtual void Update()
    {

    }
}
