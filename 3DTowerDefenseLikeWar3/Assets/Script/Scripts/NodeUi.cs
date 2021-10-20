using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeUi : MonoBehaviour
{
    public GameObject ui;

    private NodeGo target;

    public void SetTarget(NodeGo _target)
    {
        target = _target;

       // transform.position = target.GetBuildPosition();
      if(BuildManager.Instance.BuildingMode==false)
        {
            BuildManager.Instance.BuildingMode = true;
        }
        ui.SetActive(true);
    }

    public void Hide()
    {
        ui.SetActive(false);
        BuildManager.Instance.ResetBuildNode();
        BuildManager.Instance.BuildingMode = false;
    }
}
