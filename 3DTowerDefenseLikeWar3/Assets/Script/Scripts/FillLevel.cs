using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillLevel : MonoBehaviour
{
    Material material;
    float value = 0;
    private void Start()
    {
        material = GetComponent<Image>().material;
        material.SetFloat("_FillLevel", value);
        InvokeRepeating("ShowMana", 0.5f, 0.5f);

    }


    private void ShowMana()
    {

        material.SetFloat("_FillLevel", GameManager.Instance.Mana / 100f);
    }

}
