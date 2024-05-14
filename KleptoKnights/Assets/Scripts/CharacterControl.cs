using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    [SerializeField]
    private CharacterController _characterController;

    [SerializeField]
    private GameObject _camera;

    [SerializeField]
    private float _maxWalkingSpeed, _maxRunningSpeed, _acceleration, _dragOnGround, _rotationSpeed;

    private MovementState _movementState = MovementState.Walking;

    [Range(1, 4)]
    public int PlayerNumber;
    
    private Vector2 _input;

    [SerializeField]
    private Vector3 _velocity;

    public Vector3 Velocity => _velocity;

    [SerializeField]
    private float _jumpHeight;
    private bool _isJumping;

    // Start is called before the first frame update
    void Start()
    {
        _camera.GetComponent<CameraControl>().PlayerNumber = PlayerNumber;
    }

    // Update is called once per frame
    void Update()
    {
        SetMovementState();
        UpdateInput();
        Jump();
    }

    private void Jump()
    {
        if (Input.GetButtonDown($"Jump {PlayerNumber}"))
        {
            _isJumping = true;
        }
    }

    private void UpdateInput()
    {
        float inputHorizontal = Input.GetAxis($"Movement Horizontal {PlayerNumber}");
        float inputVertical = -Input.GetAxis($"Movement Vertical {PlayerNumber}");
        _input = new Vector2(inputHorizontal, inputVertical);
    }

    private void SetMovementState()
    {
        if (Input.GetButtonDown($"Run {PlayerNumber}"))
        {
            _movementState = MovementState.Running;
        }
        else if (Input.GetButtonUp($"Run {PlayerNumber}"))
        {
            _movementState = MovementState.Walking;
        }
    }

    private void FixedUpdate()
    {
        ApplyGravity();
        RotateCharacter();

        switch (_movementState)
        {
            case MovementState.Walking:
                SetMovement(_maxWalkingSpeed);
                break;
            case MovementState.Running:
                SetMovement(_maxRunningSpeed);
                break;
        }

        _characterController.Move(_velocity * Time.fixedDeltaTime);
    }

    private void RotateCharacter()
    {
        if (_input.sqrMagnitude == 0)
            return;

        //The angle the character has to be offset from the character's rotation.
        float offsetAngle = Mathf.Atan2(_input.x, _input.y) * Mathf.Rad2Deg;

        //The angle the character needs in world space.
        float worldAngle = _camera.transform.eulerAngles.y + offsetAngle;

        Quaternion targetRotation = Quaternion.Euler(0, worldAngle, 0);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.fixedDeltaTime);
    }

    private void ApplyGravity()
    {
        if (_characterController.isGrounded)
        {
            _velocity.y = Physics.gravity.y * _characterController.skinWidth;
        }
        else
        {
            _velocity.y += Physics.gravity.y * _characterController.skinWidth;
        }
    }

    private void SetMovement(float maxSpeed)
    {
        ApplyMovement();
        ApplyDrag();
        ApplySpeedLimit(maxSpeed);
        ApplyJump();
    }

    private void ApplyJump()
    {
        if (_isJumping && _characterController.isGrounded)
        {
            _velocity.y += _jumpHeight;
        }
        _isJumping = false;
    }

    private void ApplyMovement()
    {
        Vector3 v = transform.forward * Mathf.Clamp01(_input.magnitude);

        _velocity += v * _acceleration;
    }

    private void ApplyDrag()
    {
        if (_characterController.isGrounded)
        {
            _velocity *= (1 - (_dragOnGround * Time.fixedDeltaTime));
        }
    }

    private void ApplySpeedLimit(float maxSpeed)
    {
        float tempY = _velocity.y;

        _velocity.y = 0;

        _velocity = Vector3.ClampMagnitude(_velocity, maxSpeed);

        _velocity.y = tempY;
    }

    private enum MovementState
    {
        Walking,
        Running
    }
}
