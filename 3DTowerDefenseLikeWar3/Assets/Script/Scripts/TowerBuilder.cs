using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBuilder : MonoBehaviour
{

    [SerializeField]
    private TowerData tower;

    public void BuildingTower()
    {
        if (tower.BuildGold <= GameManager.Instance.Gold)
        {
            BuildManager.Instance.Tower = tower.TowerPrefab;
            BuildManager.Instance.BuildTowerToNode();
            GameManager.Instance.Gold -= tower.BuildGold;
            BuildManager.Instance.nodeUi.Hide();
            return;
        }
        else
        {
            UIManager.Instance.ShowGeneralMessage("돈이 부족합니다", Color.red, 1.0f);

            return;
        }
    }
}
