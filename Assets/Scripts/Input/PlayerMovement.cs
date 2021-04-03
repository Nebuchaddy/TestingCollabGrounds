using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Internal Data")]
    [SerializeField] InputManager inputManager;
    [SerializeField] GameObject camRef,virtualCursor;
    [SerializeField] Vector3 forward, right;
    [SerializeField] Quaternion currentRot, targetRot;
    [SerializeField] float time, defaultMoveSpeed, curMoveSpeed, moveSpeed, sprintSpeed, sneakSpeed,rotationSpeed;

    void Awake() => AwakeSetUp();
    void Start() => StartSetUp();
    void Update()
    {
        time = Time.deltaTime;
        HandleMovement();
    }

    void HandleMovement()
    {
        forward = camRef.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;

        if (inputManager.sprintFlag) curMoveSpeed = sprintSpeed;
        else if (inputManager.sneakFlag) curMoveSpeed = sneakSpeed;
        else curMoveSpeed = defaultMoveSpeed;

        Vector3 direction = new Vector3(inputManager.moveX, 0, inputManager.moveZ);

        if(direction.magnitude > 0.1f && !inputManager.aimFlag)
        {
            Vector3 rightMovement = right * curMoveSpeed * time * inputManager.moveX;
            Vector3 forwardMovement = forward * curMoveSpeed * time * inputManager.moveZ;
            Vector3 heading = Vector3.Normalize(rightMovement + forwardMovement);

            targetRot = Quaternion.LookRotation(heading, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, time * rotationSpeed);
            heading = transform.forward;
            transform.position += heading * curMoveSpeed * time;
        }
        if (inputManager.aimFlag)
        {
            Vector3 rightMovement = right * curMoveSpeed * time * inputManager.moveX;
            Vector3 forwardMovement = forward * curMoveSpeed * time * inputManager.moveZ;
            Vector3 heading = virtualCursor.transform.position - transform.position;

            targetRot = Quaternion.LookRotation(heading, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, time * rotationSpeed);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

            transform.position += (rightMovement + forwardMovement).normalized * curMoveSpeed * time;
        }

    }

    void AwakeSetUp()
    {
        inputManager = FindObjectOfType<InputManager>();
        moveSpeed = defaultMoveSpeed;
        sprintSpeed = moveSpeed * 1.5f;
        sneakSpeed = moveSpeed * 0.75f;
    }
    void StartSetUp()
    {
        camRef = inputManager.camMovement;
        virtualCursor = FindObjectOfType<VirtualCursorManager>().gameObject;
    }
}
