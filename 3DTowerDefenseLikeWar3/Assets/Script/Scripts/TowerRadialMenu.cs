using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class TowerRadialMenu : MonoBehaviour
{

    [SerializeField]
    float Radius = 50f;
    bool thisActive;
    [SerializeField]
    List<GameObject> TowerEntries;

    void Start()
    {
        thisActive = false;
    }

    
    public void Open()
    {
        GameObject item = TowerEntries.Find(x => !x.activeSelf);
        if (item == null)
        {
            for (int i = 0; i < TowerEntries.Count; i++)
            {

                item = Instantiate(TowerEntries[i],transform);
            }
        }
        Rearrannge();
        thisActive = true;
    }
    public void Close()
    {
        for (int i = 0; i < TowerEntries.Count; i++)
        {
            RectTransform rect = TowerEntries[i].GetComponent<RectTransform>();
            GameObject entry = TowerEntries[i].gameObject;
            rect.DOAnchorPos(Vector3.zero, .3f).SetEase(Ease.OutQuad).onComplete =
                delegate ()
                {
                    Debug.Log("Close");
                    entry.SetActive(false);
                };
        }
        thisActive = false;
        //Entries.Clear();
    }
    public void Toggle()
    {
        if (thisActive==false)
        {
            Open();
        }
        else
        {
            Close();
        }
    }

    public void Rearrannge()
    {
        float radianOfSeparation = (Mathf.PI * 2) / TowerEntries.Count;
        for (int i = 0; i < TowerEntries.Count; i++)
        {
            TowerEntries[i].SetActive(true);
            float x = Mathf.Sin(radianOfSeparation * i) * Radius;
            float y = Mathf.Cos(radianOfSeparation * i) * Radius;

            RectTransform rect = TowerEntries[i].GetComponent<RectTransform>();
            // Entries[i].GetComponent<RectTransform>().anchoredPosition = new Vector3(x, y, 0);
            rect.localScale = Vector3.zero;
            rect.DOScale(Vector3.one, .3f).SetEase(Ease.OutQuad).SetDelay(.05f * i);
            rect.DOAnchorPos(new Vector3(x, y, 0), .3f).SetEase(Ease.OutQuad).SetDelay(.05f * i);
        }
    }

}
