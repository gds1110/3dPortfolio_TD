using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerShopInfo : MonoBehaviour
{
    [SerializeField]
    private TowerData TD;

    [SerializeField]
    private Text Name;
    [SerializeField]
    private Text dmg;
    [SerializeField]
    private Text spd;
    [SerializeField]
    private Text gold;

    private void Start()
    {
        Name.text = TD.TowerName;
        dmg.text = "DMG : "+TD.Dmg.ToString();
        spd.text ="SPD : "+TD.CoolDown.ToString();
        gold.text ="GOLD : "+TD.BuildGold.ToString();
    }


}
