using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTipUI : MonoSingleton<ToolTipUI>
{
    public GameObject Tooltipgo;
    public Text nameText;
    public Text descriptionText;
    public Text manaCost;
    public Text coolTime;

    public void Update()
    {
        transform.position = Input.mousePosition;
    }


    public void SetUpTooltip(string name,string des,string mana,string cool)
    {
        nameText.text = name;
        descriptionText.text = des;
        manaCost.text ="MANA COST : "+mana;
        coolTime.text ="COOL TIME : "+cool;



        Tooltipgo.SetActive(true);
    }
    public void Hide()
    {
        Tooltipgo.SetActive(false);
    }

}
