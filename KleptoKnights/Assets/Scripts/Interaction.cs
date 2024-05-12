
using System;
using UnityEngine;
using Input = UnityEngine.Input;



public class Interaction : MonoBehaviour
{
    
    // Input
    //private static bool IsInteractDown => Input.GetMouseButton(0);
    private bool IsInteractDown;
    private int _playerNumber;
    
    // Interaction
    private IInteractable _interactionTarget;
    public IInteractable InteractionTarget
    {
        get => _interactionTarget;
        private set
        {
            if (value != _interactionTarget)
            {
                _interactionTarget = value;
            }

        }
    }

    private bool _isInteracting;
    private bool CanInteract 
    {
        get => !_isInteracting && _interactionTarget != null ;
        set => _isInteracting = value;
    }

    private void Start()
    {
        _playerNumber = GetComponent<CharacterControl>().PlayerNumber;
    }

    private void Update()
    {
        SetIsInteractDown();
        GetInteractable();
        Interact();
        DropItem();
    }

    private void DropItem()
    {
        if (Input.GetButtonDown($"Drop {_playerNumber}"))
        {
            GetComponent<Inventory>().DropItem();
        }
    }

    private void SetIsInteractDown()
    {
        IsInteractDown = Input.GetButtonDown($"Pickup {_playerNumber}") | Input.GetMouseButton(0);
    }

    private void Interact()
    {
        if(IsInteractDown && CanInteract) _interactionTarget?.StartInteract(gameObject);

        if (!CanInteract) // double if statement for easy refactoring later down the line
        {
            _interactionTarget?.EndInteract(gameObject);
            CanInteract = false;
        }
    }

    private void GetInteractable()
    {
        var target = InteractionFilter(InteractionRaycast());
        if (target == null)
        {
            _interactionTarget = null;
            return;
        }

        _interactionTarget = target ;
    }



    private Collider[] InteractionRaycast()
    {
        float interactionRadius = 2.5f;
        // Collider[] overlapSphere = new Collider[20];
        
        // Physics.OverlapSphereNonAlloc(transform.position, interactionRadius, overlapSphere, LayerMask.GetMask("Interactable"));

        
        return Physics.OverlapSphere(transform.position, interactionRadius, LayerMask.GetMask("Interactable"));
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawWireSphere(transform.position, 2.5f);
    //}

    //Iterates through each found interactactable GameObject and selects the most front-facing one.
    private IInteractable InteractionFilter(Collider[] interactables)
    {
        if (interactables.Length <= 0) return null;
        
        IInteractable target = null;

        foreach (var interactable in interactables)
        {
            if (!interactable.gameObject.TryGetComponent<IInteractable>(out var component ) && target == component ) continue;
            target ??= component;
            
            // figure out proper names for these variables lol
            var position = transform.position;
            
            var targetDirection = target.gameObject.transform.position - position;
            var newDirection = component.gameObject.transform.position - position;
            
            float newDot = Vector3.Dot(targetDirection, transform.forward);
            float oldDot = Vector3.Dot(newDirection, transform.forward);

            if (newDot > oldDot) target = component;
        }
        
        return target;
    }
    
   
        
    }



