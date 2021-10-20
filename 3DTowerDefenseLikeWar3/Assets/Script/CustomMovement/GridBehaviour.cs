using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBehaviour : MonoBehaviour
{
    public bool findDinstance = false;
    public int rows = 10;
    public int col = 10;
    public int scale = 1;
    public GameObject gridPrefab;
    public Vector3 leftBottomLoaction = new Vector3(0, 0, 0);
    public GameObject[,] gridArray;
    public int startX = 0;
    public int startY = 0;
    public int endX = 2;
    public int endY = 2;
    public List<GameObject> path = new List<GameObject>();
    // Start is called before the first frame update

    private void Awake()
    {
        gridArray = new GameObject[col, rows];

        if (gridPrefab)
            GeneateGrid();

        else print("missing gridPrefab, please assing");
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(findDinstance)
        {
            SetDistance();
            SetPath();
            findDinstance = false;
        }
        
    }
    void GeneateGrid()
    {
        for(int i=0;i<col;i++)
        {
            for(int j=0;j<rows;j++)
            {
                GameObject obj = Instantiate(gridPrefab, new Vector3(leftBottomLoaction.x + scale * i, leftBottomLoaction.y, leftBottomLoaction.z + scale * j), Quaternion.identity);
                obj.transform.SetParent(gameObject.transform);
                obj.GetComponent<GridStats>().x = i;
                obj.GetComponent<GridStats>().y = j;

                gridArray[i, j] = obj;

            }
        }
    }
    void SetDistance()
    {
        InitialSetUp();
        int x = startX;
        int y = startY;
        int[] testArray = new int[rows * col];
        for(int step=1;step<rows*col;step++)
        {
            foreach(GameObject obj in gridArray)
            {
                if(obj&&obj.GetComponent<GridStats>().visited==step-1)
                {
                    TestFourDirections(obj.GetComponent<GridStats>().x, obj.GetComponent<GridStats>().y, step);
                }
            }
        }
    }
    void SetPath()
    {
        int step;
        int x = endX;
        int y = endY;
        List<GameObject> tempList = new List<GameObject>();
        path.Clear();
        if(gridArray[endX,endY]&&gridArray[endX,endY].GetComponent<GridStats>().visited>0)
        {
            path.Add(gridArray[x, y]);
            step = gridArray[x, y].GetComponent<GridStats>().visited - 1;
        }
        else
        {
            print("Can't reach the desired location");
            return;
        }
        for (int i = step; step > -1; step--)
        {
            if(TestDirection(x,y,step,1))
            {
                tempList.Add(gridArray[x, y + 1]);
            }
            if (TestDirection(x, y, step, 2))
            {
                tempList.Add(gridArray[x+1, y]);
            }
            if (TestDirection(x, y, step, 3))
            {
                tempList.Add(gridArray[x, y - 1]);
            }
            if (TestDirection(x, y, step, 4))
            {
                tempList.Add(gridArray[x-1, y]);
            }
            GameObject tempObj = FindCloset(gridArray[endX, endY].transform, tempList);
            path.Add(tempObj);
            x = tempObj.GetComponent<GridStats>().x;
            y = tempObj.GetComponent<GridStats>().y;

            tempList.Clear();
        }
      
    }


    void InitialSetUp()
    {
        foreach(GameObject obj in gridArray)
        {
            obj.GetComponent<GridStats>().visited = -1;
        }
        gridArray[startX, startY].GetComponent<GridStats>().visited = 0;

    }
    bool TestDirection(int x,int y,int step,int direction)
    {
        // int 1 up int 2 right int 3 down int 4 left

        switch(direction)
        {
            case 1:
                if (y + 1 < rows && gridArray[x, y + 1] && gridArray[x, y + 1].GetComponent<GridStats>().visited == step)
                {
                    return true;
                }
                else return false;
            case 2:
                if (x+ 1 < col && gridArray[x+1, y ] && gridArray[x+1, y].GetComponent<GridStats>().visited == step)
                {
                    return true;
                }
                else return false;
            case 3:
                if (y -1 > -1 && gridArray[x, y - 1] && gridArray[x, y - 1].GetComponent<GridStats>().visited == step)
                {
                    return true;
                }
                else return false;
            case 4:
                if (x - 1 >-1 && gridArray[x+1, y] && gridArray[x+1, y].GetComponent<GridStats>().visited == step)
                {
                    return true;
                }
                else return false;
        }
        return false;
    }
    void TestFourDirections(int x,int y,int step)
    {
        if(TestDirection(x,y,-1,1))
        {
            SetVisited(x, y + 1, step);
        }
        if(TestDirection(x,y,-1,2))
        {
            SetVisited(x + 1, y, step);
        }
        if(TestDirection(x,y,-1,3))
        {
            SetVisited(x, y - 1, step);
        }
        if(TestDirection(x,y,-1,4))
        {
            SetVisited(x - 1, y, step);
        }
    }

    void SetVisited(int x,int y,int step)
    {
        if(gridArray[x,y])
        {
            gridArray[x, y].GetComponent<GridStats>().visited = step;
        }
    }
    GameObject FindCloset(Transform targetLocation, List<GameObject> list)
    {
        float currentDistance = scale * rows * col;
        int indexNumber = 0;
        for(int i=0;i<list.Count;i++)
        {
            if(Vector3.Distance(targetLocation.position,list[i].transform.position)<currentDistance)
            {
                currentDistance = Vector3.Distance(targetLocation.position, list[i].transform.position);
                indexNumber = i;
            }
        }
        return list[indexNumber];
    }
}
