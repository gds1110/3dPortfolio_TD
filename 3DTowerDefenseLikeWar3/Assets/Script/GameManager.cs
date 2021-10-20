using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum goType
{
    ENEMY,
    TOWER
}

public class GameManager : MonoSingleton<GameManager>
{
    public bool CastingMode = false;

    [Header("Select")]
    public GameObject SelObj;

    [SerializeField]
    private GameObject selIcons;


    [Header("Resource")]
    [SerializeField]
    private int StartGold = 10;
    [SerializeField]
    public int Gold;
    [Header("Mana")]
    public int MaxMana;
    public int Mana;
    private void Start()
    {
        selIcons.SetActive(false);
        MaxMana = 100;
        Mana = 10;
        Gold = StartGold;
        InvokeRepeating("GetMana", 1f, 1f);
    }
    void GetMana()
    {
        if (LevelManager.Instance.isPlay == true)
        {
            if (Mana < MaxMana)
            {
                Mana += 2;
                if (Mana > MaxMana)
                {
                    Mana = MaxMana;
                }
            }
        }
    }
    private void Update()
    {
      if(SelObj!=null)
        {
            if(SelObj.activeSelf==false)
            {
                selIcons.SetActive(false);
                SelObj = null;
            }

        }

      if(CastingMode==true)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.gameObject.CompareTag("Tower"))
                {
                 //   Debug.Log(hit.collider.gameObject.name);

                    SeleGo(hit.collider.gameObject);
                }
                if (hit.collider.gameObject.CompareTag("Enemy"))
                {
                  //  Debug.Log(hit.collider.gameObject.name);

                    SeleGo(hit.collider.gameObject);

                }
              
            }
        }

    }

    public void UseMana(int _mana)
    {
        Mana -= _mana;
    }

    public void SeleGo(GameObject go)
    {
        if (SelObj!=null)
        {
            selIcons.SetActive(false);
            SelObj = null;
        }

        SelObj = go;
        if(go.CompareTag("Enemy"))
        {
            selIcons.GetComponent<Image>().color = Color.red;
        }
        else
        {
            selIcons.GetComponent<Image>().color = Color.green;
        }

        selIcons.transform.position = new Vector3(go.transform.position.x, 0.55f, go.transform.position.z);
        selIcons.SetActive(true);
        TargetInfo.Instance.target = SelObj;
        TargetInfo.Instance.RefrshInfo();

    }
    public void GetGold(int g)
    {
        Gold += g;
    }
}
