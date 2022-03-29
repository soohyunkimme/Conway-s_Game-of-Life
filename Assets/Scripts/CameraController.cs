using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed;

    private float xMove;
    private float yMove;

    private float cameraMaxWidth = 0;
    private float cameraMaxHeight = 0;

    private const float minOrthographicSize = 3f;

    private void Update()
    {
        xMove = Input.GetAxisRaw("Horizontal");
        yMove = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        ChangeCameraSize();
        CameraMove();
    }


    private float orthographicSize;
    private float targetOrthographicSize;
    private float maxOrthographicSize;
    private void ChangeCameraSize()
    {
        float zoomAmount = 2f;
        targetOrthographicSize += -Input.mouseScrollDelta.y * zoomAmount;
        targetOrthographicSize = Mathf.Clamp(targetOrthographicSize, minOrthographicSize, maxOrthographicSize);

        // 부드러운 느낌을 주기 위한 코드
        float zoomSpeed = 5f;
        orthographicSize = Mathf.Lerp(orthographicSize, targetOrthographicSize, Time.deltaTime * zoomSpeed);

        Camera.main.orthographicSize = orthographicSize;
    }

    private void CameraMove()
    {
        Vector3 cameraPos = new Vector3(xMove, yMove) * moveSpeed + Camera.main.transform.position;

        if (cameraMaxWidth < Camera.main.orthographicSize * Camera.main.aspect)
        {
            cameraPos.x = 0;
        }
        else
        {
            cameraPos.x = Mathf.Clamp(cameraPos.x, -cameraMaxWidth + Camera.main.orthographicSize * Camera.main.aspect, cameraMaxWidth - Camera.main.orthographicSize * Camera.main.aspect);
        }

        if (cameraMaxHeight < Camera.main.orthographicSize)
        {
            cameraPos.y = 0;
        }
        else
        {
            cameraPos.y = Mathf.Clamp(cameraPos.y, -cameraMaxHeight + Camera.main.orthographicSize, cameraMaxHeight - Camera.main.orthographicSize);
        }

        Camera.main.transform.position = cameraPos;
    }

    public void SetCameraMaxDistance(float Width, float Height)
    {
        cameraMaxWidth = Width / 2;
        cameraMaxHeight = Height / 2;

        if (cameraMaxWidth / Camera.main.aspect < cameraMaxHeight)
            maxOrthographicSize = cameraMaxWidth / Camera.main.aspect;
        else
            maxOrthographicSize = cameraMaxHeight;

        cameraMaxWidth += 0.5f;
        cameraMaxHeight += 0.5f;
    }
}
