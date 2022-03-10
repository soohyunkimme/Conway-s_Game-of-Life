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
        width = 50;
        height = 50;
        setScale();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isStop = !isStop;
        }
    }

    private void FixedUpdate()
    {
        if (isStop)
            return;

        CheckAlive();
    }

    public void setScale()
    {
        grid = new Cell[width + 1, height + 1];
        for (int x = -(width / 2); x < (width / 2); x++)
        {
            for (int y = height / 2; y > -(height / 2); y--)
            {
                Vector2 vector2 = new Vector2(x, y);
                Cell cell = Instantiate(cellObj, vector2, Quaternion.identity).GetComponent<Cell>();

                grid[x + (width / 2), y + (height / 2)] = cell;
            }
        }
    }

    private void CheckAlive()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (grid[x, y].isAlive)
                {
                    int neighborCount = CheckNeighborCount(x, y);
                    if (neighborCount > 1 && neighborCount < 4)
                    {
                        grid[x, y].SetAlive(true);
                    }
                    else
                    {
                        grid[x, y].SetAlive(false);
                    }
                }
                else
                {
                    int neighborCount = CheckNeighborCount(x, y);
                    if (neighborCount == 3)
                    {
                        grid[x, y].SetAlive(true);
                    }
                    else
                    {
                        grid[x, y].SetAlive(false);
                    }
                }
            }
        }
    }

    private int CheckNeighborCount(int x, int y)
    {
        int count = 0;
        if (grid[x + 1, y + 1].isAlive) count++;
        if (grid[x + 1, y].isAlive) count++;
        if (grid[x + 1, y - 1].isAlive) count++;
        if (grid[x, y + 1].isAlive) count++;
        if (grid[x, y - 1].isAlive) count++;
        if (grid[x - 1, y + 1].isAlive) count++;
        if (grid[x - 1, y].isAlive) count++;
        if (grid[x - 1, y - 1].isAlive) count++;

        return count;
    }
}
