using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [Header("Data")]

    [Header("Internal Data")]
    [SerializeField] CameraManager cameraManager;
    public GameObject camMovement;
    public bool movementFlag, aimFlag, sprintFlag, sneakFlag;
    public float moveX, moveZ;

    public void Awake() => SetUp();
    public void Update()
    {
        CameraMovement();
        FlagHandler();
        AxisHandler();
    }

    void SetUp()
    {
        cameraManager = FindObjectOfType<CameraManager>();
        camMovement = new GameObject("MoveRef GO");
    }
    void CameraMovement()
    {
        camMovement.transform.position = cameraManager.transform.position;
        camMovement.transform.rotation = cameraManager.transform.rotation;
        camMovement.transform.eulerAngles = new Vector3(0, cameraManager.transform.eulerAngles.y, 0);
    }

    void FlagHandler()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) movementFlag = true; else movementFlag = false;
        if (Input.GetMouseButton(1)) aimFlag = true; else aimFlag = false;
        if (Input.GetButton("Sneak")) sneakFlag = true; else sneakFlag = false;
        if (Input.GetButton("Sprint")) { sprintFlag = true; sneakFlag = false; } else sprintFlag = false;
    }
    void AxisHandler()
    {
        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");
    }
}
