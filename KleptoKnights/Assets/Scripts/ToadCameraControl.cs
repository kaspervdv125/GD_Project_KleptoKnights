using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField]
    private Transform _cameraY;

    [SerializeField]
    private CharacterControl _characterControl;

    [SerializeField]
    private float _lerpSpeed, _rotationSpeed;
    private float _rotationHorizontal, _rotationVertical = 30;

    [SerializeField]
    private Vector2 _yLimitRotation;

    [SerializeField]
    private float _FOV = 60;
    private int _FOVIndex = 0;

    [SerializeField]
    private float[] _FOVSteps;
    [SerializeField]
    private float _zoomSpeed;

    [Space]
    [Header("Position Limits")]

    [SerializeField]
    private Vector2 _limitPositionX;
    [SerializeField]
    private Vector2 _limitPositionY, _limitPositionZ;

    private bool _isZoom, _isLerping;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _cameraY.localRotation = Quaternion.Euler(_rotationVertical, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        RotateCamera();

        //if (Input.GetButtonDown("Zoom"))
        //{
        //    _isZoom = true;
        //    _isLerping = true;
        //}
    }

    private void FixedUpdate()
    {
        SetPosition();

        if (_characterControl.ChrControl.isGrounded)
            RotateCharacter();

        //Zoom();
    }

    private void Zoom()
    {
        float targetFOV = _FOVSteps[_FOVIndex];

        _FOV = Mathf.Lerp(_FOV, targetFOV, _zoomSpeed * Time.deltaTime);
        Camera.main.fieldOfView = _FOV;

        if (_isZoom)
        {
            _FOVIndex++;
            _FOVIndex %= _FOVSteps.Length;
            _isZoom = false;
        }
    }

    private void SetPosition()
    {
        Vector3 charPosition = _characterControl.transform.position + Vector3.up;

        if (_FOVIndex == 0)
        {
            charPosition.x = ClampVector2(charPosition.x, _limitPositionX);
            charPosition.y = ClampVector2(charPosition.y, _limitPositionY);
            charPosition.z = ClampVector2(charPosition.z, _limitPositionZ);
        }

        transform.position = charPosition;
        //transform.position = Vector3.Lerp(transform.position, charPosition, _lerpSpeed * Time.deltaTime);
        //transform.position = Vector3.MoveTowards(transform.position, charPosition, _lerpSpeed * Time.fixedDeltaTime);
    }

    private void RotateCamera()
    {
        float inputHorizontal = Input.GetAxis("Horizontal2"), inputVertical = Input.GetAxis("Vertical2");

        if (Mathf.Abs(inputHorizontal) >= Mathf.Abs(inputVertical))
        {
            _rotationHorizontal += inputHorizontal * _rotationSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, _rotationHorizontal, 0);
        }
        else
        {
            _rotationVertical -= inputVertical * _rotationSpeed * Time.deltaTime;
            _rotationVertical = ClampVector2(_rotationVertical, _yLimitRotation);
            _cameraY.localRotation = Quaternion.Euler(_rotationVertical, 0, 0);
        }
    }

    private void RotateCharacter()
    {
        float inputHorizontal = Input.GetAxis("Horizontal1"), inputVertical = Input.GetAxis("Vertical1");

        if (inputHorizontal == 0 && inputVertical == 0)
            return;

        float angle = Mathf.Atan2(inputHorizontal, inputVertical) * Mathf.Rad2Deg;

        Quaternion targetRotation = transform.rotation * Quaternion.Euler(0, angle, 0);

        _characterControl.TargetRotation(targetRotation);
    }

    private float ClampVector2(float value, Vector2 limits)
    {
        float clampedValue = Mathf.Clamp(value, limits.x, limits.y);
        return clampedValue;
    }

    private void OnDrawGizmos()
    {
        float centerX = (_limitPositionX.x + _limitPositionX.y) / 2;
        float centerY = (_limitPositionY.x + _limitPositionY.y) / 2;
        float centerZ = (_limitPositionZ.x + _limitPositionZ.y) / 2;

        Vector3 cubeCenter = new Vector3(centerX, centerY, centerZ);

        float sizeX = _limitPositionX.y - _limitPositionX.x;
        float sizeY = _limitPositionY.y - _limitPositionY.x;
        float sizeZ = _limitPositionZ.y - _limitPositionZ.x;

        Vector3 size = new Vector3(sizeX, sizeY, sizeZ);

        Gizmos.DrawWireCube(cubeCenter, size);
    }
}
