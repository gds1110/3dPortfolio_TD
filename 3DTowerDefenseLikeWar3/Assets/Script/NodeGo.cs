using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;
using UnityEngine.EventSystems;
public class NodeGo : MonoBehaviour
{
    public Color hoverColor;

    private Renderer rend;
    private Color startColor;
    private GameObject turret;

    public UnityEvent unityEvent = new UnityEvent();
    public GameObject button;
    // Start is called before the first frame update
    void Start()
    {
        button = this.gameObject;
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        gameObject.layer = 31;
    }

    // Update is called once per frame
    void Update()
    {
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit hit;
        //if(Input.GetMouseButtonDown(0))
        //{
        //    if(Physics.Raycast(ray,out hit)&&hit.collider.gameObject==gameObject)
        //    {
        //        unityEvent.Invoke();
        //    }
        //}
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ToggleLayer();
        }
    }

    void ToggleLayer()
    {
        if(turret!=null)
        {
            gameObject.layer = 31;
            return;
        }

      if(gameObject.layer==30)
        {
            gameObject.layer = 31;
        }
      else
        {
            gameObject.layer = 30;
        }

    }
    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (GameManager.Instance.CastingMode == true)
            {
                return;
            }

            if (turret == null)
            {
                if (BuildManager.Instance.BuildingMode == false)
                {
                    //gameObject.layer = 31;

                    BuildManager.Instance.SelectNode(this);
                    BuildManager.Instance.BuildingMode = true;

                    GameManager.Instance.SelObj = null;
                    TargetInfo.Instance.targetInfo.SetActive(false);

                }

                return;
            }
            else
            {
                UIManager.Instance.ShowGeneralMessage("Already Tower is Done.", Color.red, 1.0f);
            }
        }
    }

    public void DestroyTower()
    {
        if(BuildManager.Instance.TowerList.Contains(turret))
        {
            BuildManager.Instance.TowerList.Remove(turret);
        }
        Destroy(turret);
        turret = null;

    }

    public void BuildTower(GameObject tower)
    {
        turret = Instantiate(tower.gameObject, transform.position, transform.rotation,BuildManager.Instance.TowerContainer);
        BuildManager.Instance.BuildingMode = false;
        turret.GetComponent<Tower>().node = this;
        if(NavCanPathCheck.Instance.CanReach()==true)
        {
            BuildManager.Instance.TowerList.Add(turret);
        }
        else
        {
            UIManager.Instance.ShowGeneralMessage("길을 가로막고 있어 타워가 파괴됩니다!", Color.red, 1.0f);
            Destroy(turret);
            turret = null;
        }
        ToggleLayer();
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position;
    }

    private void OnMouseEnter()
    {
        rend.material.color = hoverColor;
        if(turret!=null)
        {
            rend.material.color = Color.red;
        }
    }
    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
