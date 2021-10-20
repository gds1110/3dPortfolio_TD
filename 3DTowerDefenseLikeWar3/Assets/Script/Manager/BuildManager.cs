using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoSingleton<BuildManager>
{

    public bool BuildingMode;

    public GameObject Tower;
    public List<GameObject> TowerPrefabs;
    private Grids grid;

    public Transform TowerContainer;
    public List<GameObject> TowerList;

    private NodeGo SelectedNodeGo;
    private GameObject TowerPreview;

    private bool isActive = false;

    public NodeUi nodeUi;

    public Stack<NodeGo> towersStack;

    void Start()
    {
        towersStack = new Stack<NodeGo>();
        grid = GetComponent<Grids>();
        TowerList = new List<GameObject>();
        BuildingMode = false;
 

    }

    public void BuildTowerToNode()
    {
        SelectedNodeGo.BuildTower(Tower);
        DeselectNode();
        Tower = null;
    }

    public void BuildTower(Vector3 clickPoint)
    {
        var finalPosition = grid.GetNodeInWorldPosition(clickPoint);
        var var= Instantiate(Tower);
        var.transform.position = finalPosition.worldPosition;
        finalPosition.isPlaceable = false;
    }

    // Update is called once per frame
    void Update()
    {
      //  Invoke("BuildPreview", 1f);
        //BuildPreview();
       // PoolInput();
        

        //if (Input.GetMouseButtonDown(0))
        //{
        //    RaycastHit hitInfo;
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //    if (Physics.Raycast(ray, out hitInfo))
        //    {
        //        // print(hitInfo.point);
        //        //PlaceCubeNear(hitInfo.point);
        //        BuildTower(hitInfo.point);
        //    }

        //    // Vector3 mousePosition = MouseScript.GetMouseWorldPosition();
        //    //PlaceCubeNear(mousePosition);

        //}


    }
    private void BuildPreview()
    {
        TowerPreview.SetActive(true);
        TowerPreview.transform.position = GetMouseWorldSnappedPosition();

    }

    private void PoolInput()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            if(isActive)
            {
                DisableBuildMode();
            }
            else
            {
                ActivateBuildMode();
            }
        }
    }
    private void ActivateBuildMode()
    {
        isActive = true;
        TowerPreview.SetActive(true);
        TowerPreview.transform.position = GetMouseWorldSnappedPosition();
    }
    private void DisableBuildMode()
    {
        isActive = false;
        TowerPreview.SetActive(false);
    }

    private Vector3 GetMouseWorldSnappedPosition()
    {
        RaycastHit hitInfo;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hitInfo)) //레이어마스크 적용하자.
        {
            Vector3 mousePosition = hitInfo.point;
            Debug.Log(mousePosition);
            var finalPosition = grid.GetNodePosInWorldPosition(mousePosition);
            
            return finalPosition;
        }
        else
        {
            return default;
        }
    }

    public GameObject GetTowerBuild()
    {
        return Tower;
    }
   
  
    public void ResetBuildNode()
    {
        SelectedNodeGo = null;
    }
    public void SelectNode(NodeGo node)
    {

        if (SelectedNodeGo == node)
        {
            DeselectNode();
            return;
        }
        SelectedNodeGo = node;

        nodeUi.SetTarget(node);
    }
    public void DeselectNode()
    {
        SelectedNodeGo = null;
        nodeUi.Hide();
    }

}


