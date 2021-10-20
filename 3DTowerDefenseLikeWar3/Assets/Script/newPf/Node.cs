using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Node 
{
	public Vector3 worldPosition;
	public int gridX;
	public int gridY;
	public bool isPlaceable;
	public int gCost;
	public int hCost;
	public Node parent;
	public Transform obj;

	public Node(bool _isPlaceable, Vector3 _worldPos, int _gridX, int _gridY)
	{
		isPlaceable = _isPlaceable;
		worldPosition = _worldPos;
		gridX = _gridX;
		gridY = _gridY;
		
	}
	public Node(bool _isPlaceable,int _gridX,int _gridY)
    {
		isPlaceable = _isPlaceable;
		gridX = _gridX;
		gridY = _gridY;
    }

	public int fCost
	{
		get
		{
			return gCost + hCost;
		}
	}
}
