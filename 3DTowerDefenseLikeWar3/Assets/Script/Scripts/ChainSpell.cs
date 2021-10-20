using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainSpell : Spell
{
    [SerializeField]
    private GameObject SpellEffect;
    private LayerMask targetMask;

    // Start is called before the first frame update
    void Start()
    {

        SpellInit();

        targetMask = LayerMask.GetMask("Enemy");
        spellname = "ChainChain";
        des = "범위 내 적을 5초간 묶어둡니다.";
    }

    // Update is called once per frame
    void Update()
    {//광역으로 바꾸기
        if (Cast == true)
        {

            // SpellCast();
            magicMarker.SetActive(true);
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                magicMarker.transform.position = hit.point + new Vector3(0, 0.5f, 0);

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
                    foreach (Collider c in Physics.OverlapSphere(magicMarker.transform.position, 1f, targetMask))
                    {

                        c.GetComponent<EnemyCombat>().TakeBuff("Rooted");
                    }
                    magicMarker.SetActive(false);
                    Cast = false;
                    GameManager.Instance.CastingMode = false;

                    SpellEffect.transform.position = magicMarker.transform.position;
                    SpellEffect.SetActive(true);
                    GameManager.Instance.Mana -= manaCost;
                    Invoke("setoff", 5f);
                }
            }
            else if (Input.GetMouseButtonDown(1))
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
}
