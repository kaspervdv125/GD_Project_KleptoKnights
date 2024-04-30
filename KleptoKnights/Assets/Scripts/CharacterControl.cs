using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    private float _targetAngle;

    private Vector2 _input;

    [SerializeField]
    private float _acceleration, _dragOnGround, _maxMovementSpeed, _rotationSpeed;

    [SerializeField]
    private Vector3 _velocity;

    [SerializeField]
    private CharacterController _chrControl;

    [SerializeField]
    private bool _isFalling = false, _previousIsGrounded = true;
    private Vector3 _leftGroundPosition;

    [SerializeField]
    private float _distanceBeforeFall;

    // Update is called once per frame
    void Update()
    {
        _input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        SetTargetAngle();
    }

    private void FixedUpdate()
    {
        ApplyGravity();
        
        //if (!_isFalling)
        //{
        //    ApplyMovement();
        //    ApplyDrag();
        //    ApplySpeedLimit();
        //    //TargetRotation();

        //    if (_previousIsGrounded && !_chrControl.isGrounded)
        //    {
        //        _leftGroundPosition = transform.position;
        //    }
        //    if (!_previousIsGrounded && (transform.position - _leftGroundPosition).magnitude > _distanceBeforeFall)
        //    {
        //        _isFalling = true;
        //    }
        //    _previousIsGrounded = _chrControl.isGrounded;
        //}
        //else
        //{
        //    _velocity = new Vector3(0, _velocity.y, 0);

        //    if (_chrControl.isGrounded)
        //    {
        //        _isFalling = false;
        //    }
        //}

        if (_chrControl.isGrounded)
        {
            ApplyMovement();
            ApplyDrag();
            ApplySpeedLimit();
        }
        else if (_previousIsGrounded)
        {
            _chrControl.Move(_velocity * 0.2f);

            _velocity = new Vector3(0, _velocity.y, 0);
        }

        _previousIsGrounded = _chrControl.isGrounded;

        _chrControl.Move(_velocity * Time.fixedDeltaTime);
    }

    public void TargetRotation(Quaternion targetRotation)
    {
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, _targetAngle, 0), _rotationSpeed * Time.fixedDeltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.fixedDeltaTime);
    }

    private void ApplySpeedLimit()
    {
        float tempY = _velocity.y;

        _velocity.y = 0;
        _velocity = Vector3.ClampMagnitude(_velocity, _maxMovementSpeed);

        _velocity.y = tempY;
    }

    private void ApplyDrag()
    {
        if (!_isFalling)
        {
            _velocity *=  (1 - (_dragOnGround * Time.fixedDeltaTime));
        }
    }

    private void ApplyMovement()
    {
        Vector3 v = transform.forward * Mathf.Clamp01(_input.magnitude);

        _velocity += v * _acceleration;
    }

    private void ApplyGravity()
    {
        if (_chrControl.isGrounded)
        {
            _velocity.y = Physics.gravity.y * _chrControl.skinWidth;
        }
        else
        {
            _velocity.y += Physics.gravity.y * Time.fixedDeltaTime;
        }
    }

    private void SetTargetAngle()
    {
        if (_input.sqrMagnitude > 0)
        {
            _targetAngle = Mathf.Atan2(_input.x, _input.y) * Mathf.Rad2Deg;
        }
    }
}
