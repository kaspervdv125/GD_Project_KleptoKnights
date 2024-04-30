using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private Component cameraEmpty;
    [SerializeField] private Component cameraBoom;
    [SerializeField] private Camera camera;
    
    // Movement 
    private const float BaseSpeed = 4f; 
    private MoveMode CurrentMode 
    {
        get
        {
            if (_healthState == HealthState.Downed) return MoveMode.Crawling;
            
            if (Input.GetKey(KeyCode.LeftShift)) return MoveMode.Running;
            
            return Input.GetKey(KeyCode.LeftControl) ? MoveMode.Crouching : MoveMode.Walking;
        }
    }
    private static readonly Dictionary<MoveMode, float> SpeedMultiplier = new Dictionary<MoveMode, float>()
    {
        {MoveMode.Crawling, .15f },
        {MoveMode.Crouching, .3f},
        {MoveMode.Walking, .6f},
        {MoveMode.Running, 1f},
        {MoveMode.Sprinting, 1.5f},
    }; 
    private float CurrentSpeed => BaseSpeed * SpeedMultiplier[CurrentMode];
    private HealthState _healthState = HealthState.Healthy;
    private float _gravity; 
    
    // Camera
    private Vector3 _controlRotation = new(20f,0,0);
    private float _sensitivity = 10f;
    
    // Interaction
    private Interactable _interactionTarget;
    private Pickup _heldItem;
    private bool _isInteracting;


    
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    private void Update()
    {
        SetControlRotation();

        

        Interact();
        

    }

    private void FixedUpdate()
    {
        MoveCharacter();
        GetInteractable();
        CameraBoomArm();
    }

    private void Interact()
    {
        if(Input.GetMouseButtonDown(0) && _interactionTarget != null) _interactionTarget.StartInteract(this);

        if (!Input.GetMouseButton(0) && _isInteracting)
        {
            _interactionTarget.EndInteract(this);
            _isInteracting = false;
        }
    }

    private void GetInteractable()
    {
        float interactionRadius = 5f;
        int interactLayer = 8;
            
        Collider[] targetsInRange = Physics.OverlapSphere(transform.position, interactionRadius, interactLayer);
        
        foreach (var target in targetsInRange.Cast<Interactable>().ToArray())
        {
            
        }
    }

    private void MoveCharacter()
    {
        _gravity = controller.isGrounded ? 0f :(_gravity - 9.81f * Time.deltaTime);
        
        Vector3 camForward = Vector3.Scale(cameraEmpty.transform.forward, new Vector3(1,0,1)).normalized ;
        Vector3 camRight = Vector3.Scale(cameraEmpty.transform.right, new Vector3(1,0,1)).normalized;
        
        Vector3 inputDirection = Vector3.ClampMagnitude(camForward * Input.GetAxis("Vertical") + camRight * Input.GetAxis("Horizontal"), 1f);

        inputDirection *= CurrentSpeed;
        inputDirection.y = _gravity;
        
        controller.Move(inputDirection * Time.deltaTime); 
        
        
    }

    private void SetControlRotation()
    {
        float minAngle = -40f;
        float maxAngle = 20f;
        
        _controlRotation += new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0) * _sensitivity;
        _controlRotation.y = Mathf.Clamp(_controlRotation.y, minAngle, maxAngle);
        
        cameraEmpty.transform.rotation = Quaternion.Euler(-_controlRotation.y, _controlRotation.x,0 );
    }

    private void CameraBoomArm()
    {
        Vector3 startPosition = cameraEmpty.transform.position;
        Vector3 boomPosition = cameraBoom.transform.position;
        Vector3 direction = boomPosition - startPosition;
        float boomLength = 50f;

        if (Physics.Raycast(startPosition, direction, out var armHit, boomLength, 0))
        {
            camera.transform.position = armHit.transform.position;
        }
        else
        {
            camera.transform.position = boomPosition;
        }
    }
}

public enum MoveMode
{ 
    Crawling,
    Crouching,
    Walking,
    Running,
    Sprinting
}

public enum HealthState
{
    Healthy,
    Injured,
    DeepWounds,
    Downed
}
