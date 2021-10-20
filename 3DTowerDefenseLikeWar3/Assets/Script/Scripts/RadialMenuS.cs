using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RadialMenuS : MonoBehaviour
{
    [SerializeField]
    GameObject EntryPrefab;

    [SerializeField]
    float Radius = 50f;

    [SerializeField]
    List<Texture> Icons;

    List<RadialMenuEntry> Entries;

    void Start()
    {
        Entries = new List<RadialMenuEntry>();
    }

    void AddEntry(string pLabel,Texture pIcon)
    {

        GameObject entry = Instantiate(EntryPrefab, transform);

        RadialMenuEntry rme = entry.GetComponent<RadialMenuEntry>();

        rme.SetLabel(pLabel);
        rme.SetIcon(pIcon);

        Entries.Add(rme);
    }

    public void Open()
    {
        for(int i=0;i<7;i++)
        {
            AddEntry("Button" + i.ToString(),Icons[i]);
        }
        Rearrannge();
    }
    public void Close()
    {
        for(int i=0;i<7;i++)
        {
            RectTransform rect = Entries[i].GetComponent<RectTransform>();
            GameObject entry = Entries[i].gameObject;
            rect.DOAnchorPos(Vector3.zero, .3f).SetEase(Ease.OutQuad).onComplete =
                delegate ()
                {
                    Destroy(entry);
                    //entry.SetActive(false);
                };
        }
        Entries.Clear();
    }
    public void Toggle()
    {
        if(Entries.Count==0)
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
        float radianOfSeparation = (Mathf.PI * 2) / Entries.Count;
        for(int i=0;i<Entries.Count;i++)
        {
            float x = Mathf.Sin(radianOfSeparation * i) * Radius;
            float y = Mathf.Cos(radianOfSeparation * i) * Radius;

            RectTransform rect = Entries[i].GetComponent<RectTransform>();
            // Entries[i].GetComponent<RectTransform>().anchoredPosition = new Vector3(x, y, 0);
            rect.localScale = Vector3.zero;
            rect.DOScale(Vector3.one, .3f).SetEase(Ease.OutQuad).SetDelay(.05f * i);
            rect.DOAnchorPos(new Vector3(x, y, 0), .3f).SetEase(Ease.OutQuad).SetDelay(.05f * i);
        }
    }

}
