using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Spell : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField]
    protected GameObject magicMarker;
    public int manaCost;
    protected float value;
    protected bool Cast = false;

    public float CoolTime;

    public Text costTxt;

    protected string spellname;
    protected string des;

    protected virtual void SpellInit()
    {
      //  Debug.Log(this.ToString() + " does not implement Action()");

    }

    public virtual void Action()
    {
       // Debug.Log(this.ToString() + " does not implement Action()");
    }

    private void Start()
    {
        manaCost = 10;
        costTxt.text = manaCost.ToString();
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ToolTipUI.Instance.SetUpTooltip(spellname, des, manaCost.ToString(), CoolTime.ToString());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ToolTipUI.Instance.Hide();
    }
}
