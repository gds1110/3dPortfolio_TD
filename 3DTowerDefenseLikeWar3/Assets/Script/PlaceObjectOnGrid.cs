using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObjectOnGrid : MonoBehaviour
{
    public Transform cube;
    private Grids grid;
    void Start()
    {
        grid = GetComponent<Grids>();
    }



  
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hitInfo)) //레이어마스크 적용하자.
            {
               // print(hitInfo.point);
                //PlaceCubeNear(hitInfo.point);
               // PlaceOBJ(hitInfo.point);
            }

            // Vector3 mousePosition = MouseScript.GetMouseWorldPosition();
            //PlaceCubeNear(mousePosition);

        }
    }
    private void PlaceOBJ(Vector3 clickPoint)
    {
        var finalPosition = grid.GetNodeInWorldPosition(clickPoint);
        GameObject.CreatePrimitive(PrimitiveType.Cube).transform.position = finalPosition.worldPosition + new Vector3(0, 1, 0);
        finalPosition.isPlaceable = false;
    }

    private void PlaceCubeNear(Vector3 clickPoint)
    {
       // var finalPosition = grid.NodeFromWorldPoint(clickPoint).worldPosition;
        //Vector3 placePos = new Vector3(finalPosition.x, 1, finalPosition.z);
        var finalPosition = grid.GetNearestPointOnGrid(clickPoint);
        GameObject.CreatePrimitive(PrimitiveType.Cube).transform.position = finalPosition + new Vector3(0, 1, 0);
    }

    public Vector3 GetMouseWorldSnappedPosition()
    {
      //  Vector3 mousePosition = MouseScript.GetMouseWorldPosition();
      //  grid.GetXZ(mousePosition, out int x, out int z);

        Vector3 PlaceObjectWorldPositon = grid.GetWorldPosition(0,0);
        return PlaceObjectWorldPositon;
    }
  

}
