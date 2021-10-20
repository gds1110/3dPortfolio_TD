using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetInfo : MonoSingleton<TargetInfo>
{

    public GameObject target;

    public GameObject targetInfo;
    public GameObject E_info;
    public GameObject T_info;

    [Header("Common Info")]

    public Text go_Name;
    public Image Icon;

    [Header("Tower Info")]


    public Text t_atk;
    public Text t_spd;
    public Text t_lv;

    public Button lvUpButton;
    public Button DestroyButton;
    public Text lvUpGold;

    [Header("Enemy Info")]

    public Text e_hp;
    public Text e_spd;
    public Text e_def;

    // Start is called before the first frame update

    private void Start()
    {
       // targetInfo.SetActive(false);
    }

    private void Update()
    {
        // target = GameManager.Instance.SelObj;

  
   

        if (target == null)
        {
            targetInfo.SetActive(false);
        }
        if (GameManager.Instance.SelObj==null)
        {
            target = null;
            targetInfo.SetActive(false);
        }
        if (target != null)
        {
            if (target.activeSelf == false)
            {
                targetInfo.SetActive(false);
            }
        }
    }

    public void ButtonActionLvup()
    {
        target.GetComponent<Tower>().LevelUp();
        RefrshInfo();
    }
    public void ButtonActionDestroy()
    {
        target.GetComponent<Tower>().destroyTower();
    }

    public void RefrshInfo()
    {
        targetInfo.SetActive(true);
       
        E_info.SetActive(false);
        T_info.SetActive(false);
        if (target.CompareTag("Enemy")==true)
        {
        

            E_info.SetActive(true);

            EnemyData _ed = target.GetComponent<EnemyCombat>().enemyData;
            EnemyCombat _ec = target.GetComponent<EnemyCombat>();

            Icon.sprite = _ed.Icon;
            go_Name.text = _ed.EnemyName;

            e_hp.text = "HP : " + _ec.HitPoint;
            e_spd.text = "SPD : " + _ec.agent.speed;
            e_def.text = "DEF : " + _ec.def;



        }
        else
        {
            T_info.SetActive(true);


            TowerData _td = target.GetComponent<Tower>().td;
            Tower _tower = target.GetComponent<Tower>();

            Icon.sprite = _td.Icon;
            go_Name.text = _td.TowerName;

            t_atk.text = "ATK : " + _tower.dmgamount;
            t_spd.text = "SPD : " + _tower.cooldown;
            t_lv.text = "LV : " + _tower.Level;
            lvUpGold.text = (target.GetComponent<Tower>().Level*2).ToString();
           
        }

    }
}
