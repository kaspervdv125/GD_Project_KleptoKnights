using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    private Vector2 _input;

    [SerializeField]
    private float _acceleration, _dragOnGround, _maxWalkingSpeed, _maxRunningSpeed, _rotationSpeed;

    [SerializeField]
    private Vector3 _velocity;

    public CharacterController ChrControl;

    [SerializeField]
    private bool _previousIsGrounded = true;
    public bool IsClimbingLadder = false;

    private Vector2 _climbingUpDirection;

    [SerializeField]
    private float _topLadderJump = 0.2f;

    [SerializeField]
    private LayerMask _layerMask;

    private Quaternion _ladderRotation;

    // Update is called once per frame
    void Update()
    {
        _input = new Vector2(Input.GetAxis("Horizontal1"), Input.GetAxis("Vertical1"));
    }

    private void FixedUpdate()
    {
        ApplyGravity();

        if (IsClimbingLadder)
        {
            TargetRotation(_ladderRotation);

            if (ChrControl.isGrounded)
            {
                Vector2 normInput = _input.normalized;

                float forwardUpDirectionDot = Vector2.Dot(_climbingUpDirection, normInput);

                if (forwardUpDirectionDot >= 0)
                {
                    ApplyMovement(Vector3.up);
                    ApplyDrag();
                }
                else
                {
                    ApplyMovement(transform.forward);
                    ApplyDrag();
                    ApplySpeedLimit();
                }
            }
            else
            {
                Vector2 normInput = _input.normalized;

                float forwardUpDirectionDot = Vector2.Dot(_climbingUpDirection, normInput);

                if (forwardUpDirectionDot >= 0)
                {
                    ApplyMovement(Vector3.up);
                    ApplyDrag();
                }
                else
                {
                    ApplyMovement(Vector3.down);
                    ApplyDrag();
                }
            }
        }
        else
        {
            if (ChrControl.isGrounded)
            {
                ApplyMovement(transform.forward);
                ApplyDrag();
                ApplySpeedLimit();
            }
            else if (_previousIsGrounded)
            {
                if (Physics.Raycast(new Ray(transform.position, Vector3.down), 1.0f, _layerMask))
                {
                    _velocity.y = Physics.gravity.y;

                    ApplyMovement(transform.forward);
                    ApplyDrag();
                    ApplySpeedLimit();
                }
                else
                {
                    ChrControl.Move(_velocity * 0.2f);

                    _velocity = new Vector3(0, _velocity.y, 0);
                }
            }
        }

        _previousIsGrounded = ChrControl.isGrounded;

        ChrControl.Move(_velocity * Time.fixedDeltaTime);
    }

    public void TargetRotation(Quaternion targetRotation)
    {
        //if (_chrControl.isGrounded || !_isClimbingLadder)
        //{
        //    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.fixedDeltaTime);
        //}

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.fixedDeltaTime);
    }

    private void ApplySpeedLimit()
    {
        float tempY = _velocity.y;

        _velocity.y = 0;

        float maxSpeed;

        //if (Input.GetButton("Run"))
        //{
        //    maxSpeed = _maxRunningSpeed;
        //}
        //else
        //{
            maxSpeed = _maxWalkingSpeed;
        //}

        _velocity = Vector3.ClampMagnitude(_velocity, maxSpeed);

        _velocity.y = tempY;
    }

    private void ApplyDrag()
    {
        _velocity *=  (1 - (_dragOnGround * Time.fixedDeltaTime));
    }

    private void ApplyMovement(Vector3 direction)
    {
        Vector3 v = direction * Mathf.Clamp01(_input.magnitude);

        _velocity += v * _acceleration;
    }

    private void ApplyGravity()
    {
        if (ChrControl.isGrounded)
        {
            _velocity.y = Physics.gravity.y * ChrControl.skinWidth;
            //_velocity.y = Physics.gravity.y;
        }
        if (IsClimbingLadder)
        {
            //_velocity.y = 0;
            return;
        }
        else
        {
            _velocity.y += Physics.gravity.y * Time.fixedDeltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ladder")
        {
            IsClimbingLadder = true;
            _ladderRotation = other.transform.rotation * Quaternion.Euler(0, -90, 0);
            transform.rotation = _ladderRotation;
            _climbingUpDirection = new Vector2(transform.forward.x, transform.forward.z);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ladder")
        {
            IsClimbingLadder = false;
            ChrControl.Move(transform.forward * _topLadderJump);
            _velocity.y = 0.0f;
        }
    }
}
