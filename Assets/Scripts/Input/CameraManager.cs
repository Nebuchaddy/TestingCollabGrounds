using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] float lerpSpeed;
    [SerializeField] float aimLerpSpeed;
    [SerializeField] float offsetX = 4, offsetY = 5, offsetZ = 4, zoomOutValue = 3;

    [Header("Internal Data")]
    [SerializeField] InputManager inputManager;
    [SerializeField] GameObject player, virtualCursor;
    [SerializeField] Vector3 currentPos, targetPos;
    [SerializeField] Quaternion targetRot;
    [SerializeField] float time;
    [SerializeField] bool followPlayer = true, followVirtualCursor = false;

    void Awake() => SetUp();

    void Update()
    {
        time = Time.deltaTime;
        CameraFollow();
        InputHandler();
    }

    void CameraFollow()
    {
        currentPos = transform.position;

        if (followPlayer)
        {
            targetPos = player.transform.position;
            transform.position = Vector3.Lerp(currentPos, new Vector3(targetPos.x + offsetX,targetPos.y+offsetY,targetPos.z+offsetZ), time * lerpSpeed);
        }

        if (followVirtualCursor)
        {
            targetPos = virtualCursor.transform.position;
            transform.position = Vector3.Lerp(currentPos, new Vector3(targetPos.x + offsetX + zoomOutValue, targetPos.y + offsetY + zoomOutValue, targetPos.z + offsetZ + zoomOutValue), time * aimLerpSpeed);
        }

    }

    void InputHandler()
    {
        if (inputManager.aimFlag)
        {
            followVirtualCursor = true;
            followPlayer = false;
        }
        else
        {
            followVirtualCursor = false;
            followPlayer = true;
        }
    }

    void SetUp()
    {
        inputManager = FindObjectOfType<InputManager>();
        transform.LookAt(player.transform);
    }
}
