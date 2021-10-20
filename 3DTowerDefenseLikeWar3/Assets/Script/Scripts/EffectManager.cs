using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect
{

    public enum EffectType
    {
        BLOOD,
        SKULL,
        LEVELUP
    }

    public bool active;
    public GameObject go;
    public float duration;
    public float lastShow;
    public Vector3 motion;
    public EffectType type; 
    public void Show()
    {
        active = true;
        lastShow = Time.time;
        go.SetActive(true);
    }

    public void Hide()
    {
        active = false;
        go.SetActive(false);
    }

    public void UpdateEffect()
    {
        if (!active)
        {
            return;
        }
        if (Time.time - lastShow > duration)
        {
            Hide();
        }
        if (type == EffectType.SKULL)
        {
            go.transform.position += motion * Time.deltaTime;
        }
    }

}

public class EffectManager : MonoSingleton<EffectManager>
{

    public GameObject skullContainer;
    public GameObject skullPrefab;

    public GameObject bloodContainer;
    public GameObject bloodPrefab;
    
    public GameObject lvUpContainer;
    public GameObject lvUpPrefab;

    private List<Effect> bloodEffects = new List<Effect>();
    private List<Effect> SKULLEffects = new List<Effect>();
    private List<Effect> LvupEffects = new List<Effect>();

    private void Update()
    {
        foreach(Effect eft in bloodEffects)
        {
            eft.UpdateEffect();
        }
        foreach (Effect eft in SKULLEffects)
        {
            eft.UpdateEffect();
        } 
        
        foreach (Effect eft in LvupEffects)
        {
            eft.UpdateEffect();
        }
    }


    public void Show(Vector3 position, Vector3 motion, float duration,Effect.EffectType type)
    {
        Effect eft = GetEffect(type);
  
        eft.go.transform.position = position;
        eft.motion = motion;
        eft.duration = duration;
        eft.go.transform.rotation = Camera.main.transform.rotation;
        eft.Show();
    }
    public void Show(Vector3 position, Vector3 motion, float duration)
    {
        Effect eft = GetEffect();
  
        eft.go.transform.position = position;
        eft.motion = motion;
        eft.duration = duration;
        eft.go.transform.rotation = Camera.main.transform.rotation;
        eft.Show();
    }

    private Effect GetEffect(Effect.EffectType type)
    {
        if (type == Effect.EffectType.BLOOD)
        {
            Effect eft = bloodEffects.Find(c => !c.active);
            if (eft == null)
            {
                eft = new Effect();
                eft.go = Instantiate(bloodPrefab);
                eft.go.transform.SetParent(bloodContainer.transform);
                bloodEffects.Add(eft);
            }
            return eft;

        }
        else if(type==Effect.EffectType.SKULL)
        {
              Effect eft = SKULLEffects.Find(c => !c.active);
            if (eft == null)
            {
                eft = new Effect();
                eft.go = Instantiate(skullPrefab);
                eft.go.transform.SetParent(skullContainer.transform);
                SKULLEffects.Add(eft);
            }
            return eft;

        }
        else if(type==Effect.EffectType.LEVELUP)
        {
              Effect eft = LvupEffects.Find(c => !c.active);
            if (eft == null)
            {
                eft = new Effect();
                eft.go = Instantiate(lvUpPrefab);
                eft.go.transform.SetParent(lvUpContainer.transform);
                LvupEffects.Add(eft);
            }
            return eft;

        }
        return null;
    }
    private Effect GetEffect()
    {
       
        Effect eft = bloodEffects.Find(c => !c.active);
        if (eft == null)
        {
           eft = new Effect();
           eft.go = Instantiate(bloodPrefab);
           eft.go.transform.SetParent(bloodContainer.transform);
           bloodEffects.Add(eft);
        }
        return eft;
    }


}
