using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    int rows, columns;
    Node[,] gridNodeArray;
    public List<Node> path = new List<Node>();
    private void Start()
    {
        rows = 9;
        columns = 9;
        gridNodeArray = new Node[rows, columns];
        gridNodeArray = GamePlayManager.instance.gridArray;
    }
    public void InitialSetup(int _startX,int _startY)
    {
        foreach (var node in gridNodeArray)
        {
            if(node.occupiedBall!=null)
            {
                node.visited = -2;
            }
            else
            {
                node.visited = -1;
            }
        }
        gridNodeArray[_startX, _startY].visited = 0;
    }
    public bool Direction(int x, int y, int step, int direction)
    {
        switch (direction)
        {
            //top
            case 1:
                if (y + 1 < rows && gridNodeArray[x, y + 1] && gridNodeArray[x, y + 1].visited == step)
                    return true;
                break;
            //down
            case 2:
                if (y - 1 > -1 && gridNodeArray[x, y - 1] && gridNodeArray[x, y - 1].visited == step)
                    return true;
                break;
            //right
            case 3:
                if (x + 1 < columns && gridNodeArray[x + 1, y] && gridNodeArray[x + 1, y].visited == step)
                    return true;
                break;
            //left
            case 4:
                if (x - 1 > -1 && gridNodeArray[x - 1, y] && gridNodeArray[x - 1, y].visited == step)
                    return true;
                break;
            default:
                break;
        }
        return false;
    }
    public void SetVisited(int x, int y, int step)
    {
        if (gridNodeArray[x, y].gameObject != null)
        {
            gridNodeArray[x, y].visited = step;
        }
    }
    public void SetDistance(int _startX,int _startY)
    {
        InitialSetup(_startX,_startY);
        for (int step = 1; step < rows*columns; step++)
        {
            foreach (var node in gridNodeArray)
            {
                if (node && node.visited == step - 1)
                {
                    FourDirection((int)node.transform.position.x, (int)node.transform.position.y, step);
                }
            }
        }
    }
    public List<Node> SetPath(int endX,int endY)
    {
        int step;
        int x = endX;
        int y = endY;
        List<Node> tempList = new List<Node>();
        path.Clear();
        if (gridNodeArray[x, y] && gridNodeArray[x, y].visited > 0)
        {
            path.Add(gridNodeArray[x, y]);
            step = gridNodeArray[x, y].visited - 1;
        }
        else
        {
            return null;
        }
        for (int i= step; i > -1; i--)
        {
            //top
            if (Direction(x, y, i, 1))
            {
                tempList.Add(gridNodeArray[x, y + 1]);
            }
            //down
            if (Direction(x, y, i, 2))
            {
                tempList.Add(gridNodeArray[x, y - 1]);
            }//right
            if (Direction(x, y, i, 3))
            {
                tempList.Add(gridNodeArray[x + 1, y]);
            }//left
            if (Direction(x, y, i, 4))
            {
                tempList.Add(gridNodeArray[x - 1, y]);
            }
            Node tempNode = FindClosestPath(gridNodeArray[x, y].transform, tempList);
            path.Add(tempNode);
            x = (int)tempNode.transform.position.x;
            y = (int)tempNode.transform.position.y;
            tempList.Clear();
        }
        return path;
    }
    public void FourDirection(int x, int y, int step)
    {
        //top
        if (Direction(x, y, -1, 1))
            SetVisited(x, y + 1, step);
        //todown
        if (Direction(x, y, -1, 2))
            SetVisited(x, y - 1, step);
        //right
        if (Direction(x, y, -1, 3) )
            SetVisited(x + 1, y, step);
        //left
        if (Direction(x, y, -1, 4))
            SetVisited(x - 1, y, step);
    }
    public Node FindClosestPath(Transform targetPos, List<Node> list)
    {
        float currentDistance = rows * columns;
        int indexNumber = 0;
        for (int i = 0; i < list.Count; i++)
        {
            if (Vector3.Distance(targetPos.position, list[i].transform.position) < currentDistance)
            {
                currentDistance = Vector3.Distance(targetPos.position, list[i].transform.position);
                indexNumber = i;
            }
        }
        return list[indexNumber];
    }
}
