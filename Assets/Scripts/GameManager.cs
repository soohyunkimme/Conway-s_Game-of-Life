using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int width;
    [SerializeField]
    private int height;

    [SerializeField]
    private bool isStop = false;

    public GameObject cellObj;
    private Cell[,] grid;




    private void Start()
    {
        setScale();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CheckAlive();
        }
    }

    private void FixedUpdate()
    {

    }

    public void setScale()
    {
        grid = new Cell[width, height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Vector2 vector = new Vector2(x, y);
                Cell cell = Instantiate(cellObj, vector, Quaternion.identity).GetComponent<Cell>();
                grid[x, y] = cell;
            }
        }
    }

    private void CheckAlive()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                grid[x, y].neighborCount = CheckNeighborCount(x, y);
            }
        }

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Cell cell = grid[x, y];
                if (cell.isAlive)
                {
                    if (cell.neighborCount > 1 && cell.neighborCount < 4)
                    {
                        cell.SetAlive(true);
                    }
                    else
                    {
                        cell.SetAlive(false);
                    }
                }
                else
                {
                    if (cell.neighborCount == 3)
                    {
                        cell.SetAlive(true);
                    }
                    else
                    {
                        cell.SetAlive(false);
                    }
                }
                cell.neighborCount = 0;
            }
        }
    }

    private int CheckNeighborCount(int x, int y)
    {
        int count = 0;
        if (x + 1 < width && y + 1 < height)
            if (grid[x + 1, y + 1].isAlive) count++;

        if (x + 1 < width)
            if (grid[x + 1, y].isAlive) count++;

        if (x + 1 < width && y - 1 >= 0)
            if (grid[x + 1, y - 1].isAlive) count++;

        if (y + 1 < height)
            if (grid[x, y + 1].isAlive) count++;

        if (y - 1 >= 0)
            if (grid[x, y - 1].isAlive) count++;

        if (x - 1 >= 0 && y + 1 < height)
            if (grid[x - 1, y + 1].isAlive) count++;

        if (x - 1 >= 0)
            if (grid[x - 1, y].isAlive) count++;

        if (x - 1 >= 0 && y - 1 >= 0)
            if (grid[x - 1, y - 1].isAlive) count++;

        return count;
    }
}
