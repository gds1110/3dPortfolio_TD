using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DamageInfo
{
    public int ammount;
}

public class BaseCombat : MonoBehaviour
{
    [SerializeField]
    protected int hitpoint =0;
    [SerializeField]
    protected int maxHitpoint;

    [SerializeField]
    public int def = 0;
    public Animator anim;

    [Header("Unity Stuff")]
    public Image healthBar;

    public int HitPoint { set { hitpoint = value; } get { return hitpoint; } }
    public int MaxHitpoint { set { maxHitpoint = value; } get { return maxHitpoint; } }
    public void Awake()
    {
        InitCombat();
    }

    // Start is called before the first frame update
    void  Start()
    {
        anim = GetComponent<Animator>();
        
    }
    public virtual void InitCombat()
    {
        hitpoint = maxHitpoint;
    }
    public virtual void OnDamage(DamageInfo dmg)
    {
        EffectManager.Instance.Show(transform.position, Vector3.zero, 0.2f,Effect.EffectType.BLOOD);
        int hitDmg = dmg.ammount-def;
        if(hitDmg<=0)
        {
            hitDmg = 1;
        }
        CombatTextManager.Instance.Show(hitDmg.ToString(), 50, Color.red, transform.position, Vector3.up, 1.5f);

        hitpoint -= hitDmg;
        healthBar.fillAmount = (float)hitpoint/(float)maxHitpoint;
        if(HitPoint<=0)
        {
            OnDeath();
        }
    }

    public virtual void OnDeath()
    {

        Debug.Log("death");
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //public void OnMouseDown()
    //{
    //    GameManager.Instance.SeleGo(gameObject);
    //}
}
