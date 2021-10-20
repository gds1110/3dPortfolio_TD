using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Grids : MonoSingleton<Grids>
{
	[SerializeField]
	private float size;

	public int width;
	public int height;
    Vector3 worldBottomLeft;
    [SerializeField]
    List<Vector3> wplist;
    [SerializeField]
    private Node[,] grid;
	Vector3 originPosition;

    [SerializeField]
    private GameObject pathRender;

    [SerializeField]
    private GameObject NodeGo;
    [SerializeField]
    private List<GameObject> NodeGoList;

    private void Awake()
    {
        wplist = new List<Vector3>();
        NodeGoList = new List<GameObject>();
        worldBottomLeft = transform.position - Vector3.right * width / 2 - Vector3.forward *height / 2;
        
        CreateGrid();
    }

    void CreateGrid()
    {
		grid = new Node[width, height];

		for(int x=0;x<grid.GetLength(0);x++)
        {
			for(int z=0;z<grid.GetLength(1);z++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * size+size/2) + Vector3.forward * (z * size+size/2);
                grid[x, z] = new Node(true,worldPoint ,x, z);
                wplist.Add(worldPoint);
                var var= Instantiate(NodeGo, worldPoint + new Vector3(0, 0.1f, 0), Quaternion.identity, gameObject.transform);
                NodeGoList.Add(var);
            }
        }
    }
	public Vector3 GetWorldPosition(float x,float z)
    {
		return new Vector3(x, 0, z) * size - originPosition;
    }

	//void OnDrawGizmos()
	//{
	//	Gizmos.DrawWireCube(transform.position, new Vector3(width, 1, height));

 //       if (grid != null)
 //       {
 //           //foreach (Node n in grid)
 //           //{
 //           //    Gizmos.color = (n.isPlaceable) ? Color.white : Color.red;

               

 //           //    Gizmos.DrawCube(n.worldPosition, Vector3.one * (size - .1f));
 //           //}
 //           //Gizmos.color = Color.yellow;

 //           //foreach (Node n in grid)
 //           //{
 //           //    Gizmos.DrawSphere(n.worldPosition, 0.1f);
 //           //}
            
 //           for(int x=0;x<width;x++)
 //           {
 //               for(int y=0;y<height;y++)
 //               {
 //                   Gizmos.color = Color.white;
 //                   if(x==0)
 //                   {
 //                       Gizmos.color = Color.red;
 //                   }
 //                   if(grid[x,y].isPlaceable==false)
 //                   {
 //                       Gizmos.color = Color.blue;
 //                   }

 //                   Gizmos.DrawCube(grid[x, y].worldPosition, Vector3.one * (size));

 //               }
 //           }
        
        
 //       }



 //   }
 
    public void ToggleLayer()
    {
        foreach(var go in NodeGoList)
        {
            go.GetComponent<NodeGo>().SendMessage("ToggleLayer");
        }
        pathRender.GetComponent<NavCanPathCheck>().SendMessage("ToggleLayer");
    }

    public void setOBJTest(Vector3 wp)
    {
        int x = Mathf.RoundToInt(wp.x);
        int z = Mathf.RoundToInt(wp.z);
        grid[x,z].isPlaceable = false;
    }

    public void GetXZ(Vector3 worldPosition, out int x, out int z)
    {
        x = Mathf.FloorToInt(worldPosition.x / width);
        z = Mathf.FloorToInt(worldPosition.z / height);

    }
    public Node GetGridObject(int x, int z)
    {
        if (x >= 0 && z >= 0 && x < width && z < height)
        {
            return grid[x, z];
        }
        else
        {
            return default;
        }

    }
    public Node GetGridObject(Vector3 worldPosition)
    {
        int x;
        int z;
        GetXZ(worldPosition, out x, out z);
        return GetGridObject(x, z);
    }
   // Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * size + size / 2) + Vector3.forward * (z * size + size / 2);

    public Node GetNodeInWorldPosition(Vector3 worldPos)
    {
        Debug.Log(worldPos);
        worldPos -= worldBottomLeft;
        worldPos.x = worldPos.x / size - size / 2;
        worldPos.z = worldPos.z / size - size / 2;
        int ix = Mathf.RoundToInt(worldPos.x);
        int iz = Mathf.RoundToInt(worldPos.z);
        //예외처리만하면 ok
        if (ix < 0 || ix > width || iz < 0 || iz > height)
        {
            return default;
        }

        Debug.Log(ix + " " + iz);
        return grid[ix, iz];
    }
    public Vector3 GetNodePosInWorldPosition(Vector3 worldPos)
    {
        Debug.Log(worldPos);
        worldPos -= worldBottomLeft;
        worldPos.x = worldPos.x / size - size / 2;
        worldPos.z = worldPos.z / size - size / 2;
        int ix = Mathf.RoundToInt(worldPos.x);
        int iz = Mathf.RoundToInt(worldPos.z);
        //예외처리만하면 ok
        if(ix<0||ix>width||iz<0||iz>height)
        {
            return default;
        }

        Debug.Log(ix + " " + iz);
        return grid[ix, iz].worldPosition;
    }

    public Node NodeFromWorldPoint(Vector3 a_vWorldPos)
    {
        float ixPos = ((a_vWorldPos.x + width / 2) / width);
        float iyPos = ((a_vWorldPos.z + height/2) / height);
        print(ixPos);
        print(iyPos);
        ixPos = Mathf.Clamp01(ixPos);
        iyPos = Mathf.Clamp01(iyPos);

        int ix = Mathf.RoundToInt((size ) * ixPos);
        int iy = Mathf.RoundToInt((size ) * iyPos);
        print(ix);
        print(iy);
        return grid[ix, iy];
    }
    public Vector3 GetNearestPointOnGrid(Vector3 position)
    {
        position -= transform.position;
        int xCount = Mathf.RoundToInt(position.x / size);
        int yCount = Mathf.RoundToInt(position.y / size);
        int zCount = Mathf.RoundToInt(position.z / size);

        Vector3 result = new Vector3
            (
            (float)xCount *size,
            1,
            (float)zCount*size
            );
        result += transform.position;
        return result;
    }

}
