﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Components")]
    new Camera camera;

    [Header("Camera")]
    public float XSensitivity = 2f;
    public float YSensitivity = 2f;
    public float MinimumX = -90f;
    public float MaximumX = 90f;

    [Header("Transforms")]
    public Transform headTransform;
    public Vector3 headPosition { get { return headTransform.position; } }
    Vector3 originalCameraPosition;

    [Header("Zoom")]
    public LayerMask viewBlockingLayers;
    public float zoomSpeed = 0.5f;
    public float distance = 0f;
    public float minDistance = 0f;
    public float maxDistance = 7f;

    [Header("Camera Offsets")]
    public Vector2 firstPersonStandingOffset = Vector2.zero;
    public Vector2 thirdPersonStandingOffset = Vector2.up;
    public Vector2 thirdPersonStandingOffsetMultiplier = Vector2.zero;

    [Header("IK Offsets")]
    public Vector3 firstPersonLeftArmIKOffset = Vector3.zero;
    public Vector3 firstPersonRightArmIKOffset = Vector3.zero;
    public Vector3 chestIkOffset = new Vector3(-1.6f, -86.3f, -85.48f);

    // IKs
    IKHandler chestIK;

    void Awake()
    {
        camera = Camera.main;
    }

    void Start()
    {
        camera.transform.SetParent(transform, false);
        camera.transform.rotation = Quaternion.identity;
        camera.transform.Rotate(0, 180, 0);
        camera.transform.position = headPosition;

        originalCameraPosition = camera.transform.localPosition;

        // Init iks
        chestIK = new IKHandler(this.GetComponent<Animator>(), IKBone.CHEST);
    }

    void Update()
    {
        float xExtra = Input.GetAxis("Mouse X") * XSensitivity;
        float yExtra = Input.GetAxis("Mouse Y") * YSensitivity;

        transform.Rotate(new Vector3(0, xExtra, 0));
        camera.transform.Rotate(new Vector3(-yExtra, 0, 0));

        // Animator
        HandleAnimations();
    }

    void LateUpdate()
    {
        camera.transform.localRotation =
            QuaternionUtils.ClampRotationAroundXAxis(camera.transform.localRotation, MinimumX, MaximumX);

        float step = GetMouseScroll() * zoomSpeed;

        // We only want half steps for scroll when we are far away from the character model
        // otherwise, we will see some ugly clipping
        step = distance > 1f ? step * 0.5f : Mathf.Round(step);

        // Apply the step offset to the distance
        distance = Mathf.Clamp(distance - step, minDistance, maxDistance);

        if (distance == 0f) // First Person
        {
            Vector3 headLocal = transform.InverseTransformPoint(headPosition);
            Vector3 origin = Vector3.zero;
            Vector3 offset = Vector3.zero;

            origin = headLocal;
            offset = firstPersonStandingOffset;

            // Final Position
            Vector3 target = transform.TransformPoint(origin + offset);
            camera.transform.position = target;
        }
        else // Third Person
        {
            Vector3 origin = Vector3.zero;
            Vector3 offsetBase = Vector3.zero;
            Vector3 offsetMultiplier = Vector3.zero;

            origin = originalCameraPosition;
            offsetBase = thirdPersonStandingOffset;
            offsetMultiplier = thirdPersonStandingOffsetMultiplier;

            Vector3 target = transform.TransformPoint(origin + offsetBase + (offsetMultiplier * distance));
            Vector3 newPosition = target - (camera.transform.rotation * Vector3.forward * distance);

            float finalDistance = distance;
            RaycastHit hit;
            if (Physics.Linecast(target, newPosition, out hit, viewBlockingLayers))
            {
                // Find a better position, with some space added in
                finalDistance = Vector3.Distance(target, hit.point) - 0.1f;
            }

            camera.transform.position = target - (camera.transform.rotation * Vector3.forward * finalDistance);
        }

        // After camera logic, handle camera owner IK
        HandleIK();
    }

    void HandleIK()
    {
        chestIK.LookAt(camera.transform.rotation, chestIkOffset);
    }

    void HandleAnimations()
    {
    }

    public bool InFirstPerson()
    {
        return distance == 0;
    }

    public static float GetMouseScroll()
    {
        float scroll = Input.GetAxisRaw("Mouse ScrollWheel");
        if (scroll < 0f) { return -1f; }
        if (scroll > 0f) { return  1f; }
        return 0f;
    }
}
