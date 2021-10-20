using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteo : Spell
{
    [SerializeField]
    private GameObject SpellEffect;
    private LayerMask targetMask;

    

    private void Start()
    {
        SpellInit();
        targetMask = LayerMask.GetMask("Enemy");
        manaCost = 10;
        spellname = "BOOM!";
        des = "범위 내 적에게 강력한 폭발을 일으켜 10의 데미지를 줍니다.";
    }

    private void Update()
    {
     
        if(Cast==true)
        {

            // SpellCast();
            magicMarker.SetActive(true);
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                magicMarker.transform.position = hit.point+new Vector3(0,1,0);

            }
            else
            {
                magicMarker.SetActive(false);
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (GameManager.Instance.Mana < manaCost)
                {
                    UIManager.Instance.ShowGeneralMessage("마나가 부족합니다", Color.red, 1.0f);
                    magicMarker.SetActive(false);
                    Cast = false;
                    GameManager.Instance.CastingMode = false;

                }
                else
                {

                    DamageInfo dmg = new DamageInfo();
                    dmg.ammount = 10;
                    foreach (Collider c in Physics.OverlapSphere(magicMarker.transform.position, 2f, targetMask))
                    {

                        c.GetComponent<EnemyCombat>().OnDamage(dmg);
                    }
                    magicMarker.SetActive(false);
                    Cast = false;
                    GameManager.Instance.CastingMode = false;

                    SpellEffect.transform.position = magicMarker.transform.position;
                    SpellEffect.SetActive(true);
                    GameManager.Instance.Mana -= manaCost;

                    Invoke("setoff", 0.5f);
                }
            }
            else if(Input.GetMouseButtonDown(1))
            {
                GameManager.Instance.CastingMode = false;
                Cast = false;
                magicMarker.SetActive(false);
            }
        }
        else
        {
            magicMarker.SetActive(false);
        }


    }
    private void OnDisable()
    {
        //private void OnEnable()
        //{
        //    Collider[] colliders = Physics.OverlapSphere(transform.position, 1.5f);


        //    foreach (Collider col in colliders)
        //    {
        //        if (col.tag == "Enemy")
        //        {
        //            //col.SendMessage("OnDamage", dmg);
        //            col.GetComponent<EnemyCombat>().OnDamage(dmg);
        //            Debug.Log("hit splash" + col.name);
        //        }
        //    }


        //    Invoke("SetFalse", 0.5F);

        //}
    }

    void setoff()
    {
        SpellEffect.SetActive(false);
    }


    public override void Action()
    {
        //StartCoroutine(PreCast());
        Cast = true;
        GameManager.Instance.CastingMode = true;
    }

   private void SpellCast()
    {
        if (magicMarker)
        {
            while (true)
            {
                magicMarker.SetActive(true);

                RaycastHit hit;
                Ray ray = new Ray(Camera.main.transform.position + new Vector3(0, 2, 0), Camera.main.transform.forward);
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    magicMarker.transform.position = hit.point;

                }
                else
                {
                    magicMarker.SetActive(false);
                }

                if (Input.GetMouseButtonDown(0))
                {

                    magicMarker.SetActive(false);
                    break;

                }


            }
        }
        else
        {
            return;
        }


    }


    protected override void SpellInit()
    {
        manaCost = 10;
        value = 10f;
    }

    public IEnumerator PreCast()
    {
        if(magicMarker)
        {
            while (true)
            {
                magicMarker.SetActive(true);

                RaycastHit hit;
                Ray ray = new Ray(Camera.main.transform.position + new Vector3(0, 2, 0), Camera.main.transform.forward);
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    magicMarker.transform.position = hit.point;
                    
                }
                else
                {
                    magicMarker.SetActive(false);
                }

                if (Input.GetMouseButtonDown(0))
                {

                    magicMarker.SetActive(false);
                    yield break;

                }


            }
        }


        yield break;

    }

}
