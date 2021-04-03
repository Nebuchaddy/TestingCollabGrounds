using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualCursorManager : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] GameObject player;
    [SerializeField] float lerpSpeed = 10, maxDistance = 5;

    [Header("Internal Data")]
    [SerializeField] InputManager inputManager;
    [SerializeField] Camera mainCamera;
    [SerializeField] Vector3 currentPos, targetPos;
    [SerializeField] Ray cameraRay;
    [SerializeField] float time;

    public void Awake() => SetUp();

    void Update()
    {
        time = Time.deltaTime;
        HandleCursorPosition();
    }

    void HandleCursorPosition()
    {
        cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        currentPos = transform.position;
        Physics.Raycast(cameraRay, out RaycastHit hit, Mathf.Infinity);

        if (!inputManager.aimFlag)
        {
            targetPos = hit.point;
        }
        else
        {
            float distance = Vector3.Distance(hit.point, player.transform.position);

            if(distance > maxDistance)
            {
                Vector3 newPos = hit.point - player.transform.position;
                newPos *= maxDistance / distance;
                targetPos = player.transform.position + newPos;
            }

            else targetPos = hit.point;

            targetPos.x += 2;
            targetPos.y += 2;
            targetPos.z += 2;
        }

        transform.position = Vector3.Lerp(currentPos, targetPos, time * lerpSpeed);
    }

    void SetUp()
    {
        inputManager = FindObjectOfType<InputManager>();
    }
}
