using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int width;
    [SerializeField]
    private int height;

    public bool isStop = false;

    public float checkTick;
    private float time;

    public GameObject cellObj;
    private Cell[,] grid;


    private void Start()
    {
        //setScale();
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
        if (isStop) return;

        time += Time.deltaTime;
        if (time > checkTick)
        {
            time = 0;
            CheckAlive();
        }
    }

    public void SetScale(int _width, int _height)
    {
        //Â¦¼ö°¡ ¾ÈµÊ
        width = _width;
        height = _height;

        grid = new Cell[width + 1, height + 1];
        for (int y = -(height / 2); y <= height / 2; y++)
        {
            for (int x = -(width / 2); x <= width / 2; x++)
            {
                //vector ¹Ù²Ù±â;
                Vector2 vector = new Vector2(x, y);
                Cell cell = Instantiate(cellObj, this.gameObject.transform).GetComponent<Cell>();
                cell.gameObject.transform.position = vector;
                grid[x + (width / 2), y + (height / 2)] = cell;
            }
        }
        GetComponent<CameraController>().SetCameraMaxDistance(width, height);
    }

    public void DestroyCells()
    {
        Cell[] ts = gameObject.GetComponentsInChildren<Cell>();
        foreach (Cell item in ts)
        {
            Destroy(item.gameObject);
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
