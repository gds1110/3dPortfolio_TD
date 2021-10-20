using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : BaseCombat
{
    private List<BaseSpell> spellBook = new List<BaseSpell>();

    public override void InitCombat()
    {
        base.InitCombat();

        spellBook.Add(gameObject.AddComponent<MeleeAttackSpell>() as BaseSpell);
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            spellBook[0].Cast();
        }
    }
}
