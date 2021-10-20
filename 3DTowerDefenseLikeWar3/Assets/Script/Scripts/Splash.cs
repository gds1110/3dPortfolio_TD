using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash : MonoBehaviour
{
    public DamageInfo dmg;
    SphereCollider sphereCollider;
    private void Awake()
    {

        sphereCollider = GetComponent<SphereCollider>();
        
        gameObject.SetActive(false);
    
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1.5f);
    }

    private void OnEnable()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 1.5f);

    
        foreach(Collider col in colliders)
        {
            if(col.tag=="Enemy")
            {
                //col.SendMessage("OnDamage", dmg);
                col.GetComponent<EnemyCombat>().OnDamage(dmg);
                Debug.Log("hit splash" + col.name);
            }
        }


        Invoke("SetFalse", 0.5F);

    }

    private void SetFalse()
    {
        gameObject.SetActive(false);
    }

    //private void OnTriggerEnter(Collider other)
    //{

    //    if(other.tag=="Enemy")
    //    {
    //        other.SendMessage("OnDamage", dmg);
    //        Debug.Log("hit splash" + other.name);

    //    }
    //    gameObject.SetActive(false);
    //}

}
