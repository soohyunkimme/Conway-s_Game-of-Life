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
        xMove = Input.GetAxis("Horizontal");
        yMove = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        ChangeCameraSize();
        Vector2 inputMove = new Vector2(xMove, yMove) * moveSpeed;
        Camera.main.transform.position = (Vector3)inputMove + Camera.main.transform.position;
        CameraLock();
    }


    private float orthographicSize;
    private float targetOrthographicSize;
    private float maxOrthographicSize;
    private void ChangeCameraSize()
    {
        if(minOrthographicSize < cameraMaxWidth || minOrthographicSize < cameraMaxHeight)
        {
            return;
        }
        float zoomAmount = 2f;
        targetOrthographicSize += -Input.mouseScrollDelta.y * zoomAmount;
        targetOrthographicSize = Mathf.Clamp(targetOrthographicSize, minOrthographicSize, maxOrthographicSize);

        // 부드러운 느낌을 주기 위한 코드
        float zoomSpeed = 5f;
        orthographicSize = Mathf.Lerp(orthographicSize, targetOrthographicSize, Time.deltaTime * zoomSpeed);

        Camera.main.orthographicSize = orthographicSize;
    }

    private void CameraLock()
    {
        float ylock = 0;
        float xlock = 0;
        if (Camera.main.transform.position.y + Camera.main.orthographicSize > cameraMaxHeight)
        {
            ylock = cameraMaxHeight - (Camera.main.transform.position.y + Camera.main.orthographicSize);
        }
        if (Camera.main.transform.position.y - Camera.main.orthographicSize < -cameraMaxHeight)
        {
            ylock = -cameraMaxHeight - (Camera.main.transform.position.y - Camera.main.orthographicSize);
        }
        if (Camera.main.transform.position.x + (Camera.main.orthographicSize * Camera.main.aspect) > cameraMaxWidth)
        {
            xlock = cameraMaxWidth - (Camera.main.transform.position.x + (Camera.main.orthographicSize * Camera.main.aspect));
        }
        if (Camera.main.transform.position.x - (Camera.main.orthographicSize * Camera.main.aspect) < -cameraMaxWidth)
        {
            xlock = -cameraMaxWidth - (Camera.main.transform.position.x - (Camera.main.orthographicSize * Camera.main.aspect));
        }

        Vector2 vector2 = new Vector2(xlock, ylock);
        Camera.main.transform.position = (Vector3)vector2 + Camera.main.transform.position;
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
