using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightCharacter : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private Component cameraEmpty;
    [SerializeField] private Component cameraBoom;
    //[SerializeField] private Camera camera;
    
    // Interaction
    private IInteractable _interactionTarget;
    private GameObject _heldItem;
    private bool _isInteracting;
    private float _gravity;
    public float MoveSpeed { get; private set; } = 6f;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        GetInteractable();
    }
    
    private void GetInteractable()
    {

    }
    
    private void MoveCharacter()
    {
        _gravity = controller.isGrounded ? 0f :(_gravity - 9.81f * Time.deltaTime);
        
        Vector3 camForward = Vector3.Scale(cameraEmpty.transform.forward, new Vector3(1,0,1)).normalized ;
        Vector3 camRight = Vector3.Scale(cameraEmpty.transform.right, new Vector3(1,0,1)).normalized;
        
        Vector3 inputDirection = Vector3.ClampMagnitude(camForward * Input.GetAxis("Vertical1") + camRight * Input.GetAxis("Horizontal1"), 1f);

        inputDirection *= MoveSpeed;
        inputDirection.y = _gravity;
        
        controller.Move(inputDirection * Time.deltaTime); 
        
        
    }


}
