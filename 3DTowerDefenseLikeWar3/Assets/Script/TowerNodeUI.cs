using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerNodeUI : MonoBehaviour
{
    public GameObject ui;

    private Tower target;

    public void SetTarget(Tower _target)
    {
        target = _target;

        transform.position = _target.transform.position + new Vector3(0, 1f, 0);
       
        ui.SetActive(true);
    }

    public void Hide()
    {
        ui.SetActive(false);
    }
}
