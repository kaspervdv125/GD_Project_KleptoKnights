using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [HideInInspector]
    public int PlayerNumber;

    [SerializeField]
    private Transform _cameraY;

    private float _rotationHorizontal, _rotationVertical;

    [SerializeField]
    private Vector2 _verticalRotationLimit;

    private Vector2 _input;

    [SerializeField]
    private float _rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _rotationHorizontal = transform.eulerAngles.y;
        _rotationVertical = _cameraY.eulerAngles.x;


        Camera camera = transform.GetComponentInChildren<Camera>();
        if (PlayerNumber == 1)
        {
            camera.rect = new Rect(0, 0, 0.5f, 1);
        }
        else if (PlayerNumber == 2)
        {
            camera.rect = new Rect(0.5f, 0, 0.5f, 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        SetInput();
    }

    private void FixedUpdate()
    {
        RotateCamera();
    }

    private void SetInput()
    {
        float inputHorizontal = Input.GetAxis($"Camera Horizontal {PlayerNumber}");
        float inputVertical = Input.GetAxis($"Camera Vertical {PlayerNumber}");

        _input = new Vector2(inputHorizontal, inputVertical);
    }

    private void RotateCamera()
    {
        _rotationHorizontal += _input.x * _rotationSpeed * Time.fixedDeltaTime;
        _rotationVertical -= _input.y * _rotationSpeed * Time.fixedDeltaTime;

        _rotationVertical = Mathf.Clamp(_rotationVertical, _verticalRotationLimit.x, _verticalRotationLimit.y);

        transform.rotation = Quaternion.Euler(0, _rotationHorizontal, 0);
        _cameraY.localRotation = Quaternion.Euler(_rotationVertical, 0, 0);
    }
}
