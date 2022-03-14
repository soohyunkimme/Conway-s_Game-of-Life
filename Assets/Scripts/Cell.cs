using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public bool isAlive = false;
    public int neighborCount = 0;

    private SpriteRenderer spriteRender;

    private void Awake()
    {
        spriteRender = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        if (isAlive)
        {
            SetAlive(false);
        }
        else
        {
            SetAlive(true);
        }
    }

    public void SetAlive(bool setAlive)
    {
        if (setAlive)
        {
            isAlive = true;
            spriteRender.color = Color.white;
        }
        else
        {
            isAlive = false;
            spriteRender.color = Color.black;
        }
    }
}
