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

    public Vector2 VerticalRotationLimit;

    private Vector2 _input;

    [SerializeField]
    private float _rotationSpeed;

    public float TargetYRotation;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _rotationHorizontal = transform.eulerAngles.y;
        _rotationVertical = _cameraY.eulerAngles.x;

        int playerCount = FindObjectsOfType<CharacterController>().Length;

        switch (playerCount)
        {
            case 1:
                OnePlayerCam();
                break;
            case 2:
                TwoPlayerCam();
                break;
            case 3:
                ThreePlayerCam();
                break;
            case 4:
                FourPlayerCam();
                break;
            default:
                throw new Exception("Invalid PlayerCount.");
        }
    }

    private void FourPlayerCam()
    {
        Camera camera = transform.GetComponentInChildren<Camera>();

        switch (PlayerNumber)
        {
            case 1:
                camera.rect = new Rect(0, 0.5f, 0.5f, 0.5f);
                break;
            case 2:
                camera.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
                break;
            case 3:
                camera.rect = new Rect(0, 0, 0.5f, 0.5f);
                break;
            case 4:
                camera.rect = new Rect(0.5f, 0, 0.5f, 0.5f);
                break;
            default:
                throw new Exception("Invalid PlayerNumber");
        }
    }

    private void ThreePlayerCam()
    {
        Camera camera = transform.GetComponentInChildren<Camera>();

        switch (PlayerNumber)
        {
            case 1:
                camera.rect = new Rect(0, 0, 0.5f, 1);
                break;
            case 2:
                camera.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
                break;
            case 3:
                camera.rect = new Rect(0.5f, 0, 0.5f, 0.5f);
                break;
            default:
                throw new Exception("Invalid PlayerNumber");
        }
    }

    private void TwoPlayerCam()
    {
        Camera camera = transform.GetComponentInChildren<Camera>();
        
        switch (PlayerNumber)
        {
            case 1:
                camera.rect = new Rect(0, 0, 0.5f, 1);
                break;
            case 2:
                camera.rect = new Rect(0.5f, 0, 0.5f, 1);
                break;
            default:
                throw new Exception("Invalid PlayerNumber");
        }
    }

    private void OnePlayerCam()
    {
        Camera camera = transform.GetComponentInChildren<Camera>();

        camera.rect = new Rect(0, 0, 1, 1);
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
        float inputVertical = -Input.GetAxis($"Camera Vertical {PlayerNumber}");

        _input = new Vector2(inputHorizontal, inputVertical);
    }

    private void RotateCamera()
    {
        _rotationHorizontal += _input.x * _rotationSpeed * Time.fixedDeltaTime;

        float targetYDifference = _input.y * _rotationSpeed * Time.fixedDeltaTime;
        TargetYRotation -= targetYDifference;

        TargetYRotation = Mathf.Clamp(TargetYRotation, VerticalRotationLimit.x, VerticalRotationLimit.y);

        transform.rotation = Quaternion.Euler(0, _rotationHorizontal, 0);

        if (Mathf.Abs(_rotationVertical - TargetYRotation) > targetYDifference)
        {
            _rotationVertical = Mathf.Lerp(_rotationVertical, TargetYRotation, 0.1f);
        }
        else
        {
            _rotationVertical = TargetYRotation;
        }

        _cameraY.localRotation = Quaternion.Euler(_rotationVertical, 0, 0);
    }
}
