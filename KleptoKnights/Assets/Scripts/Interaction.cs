
using UnityEngine;
using Input = UnityEngine.Input;



public class Interaction : MonoBehaviour
{
    
    // Input
    private static bool IsInteractDown => Input.GetMouseButton(0);
    
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

    private void Update()
    {
        GetInteractable();
        Interact();
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
        _interactionTarget = InteractionFilter(InteractionRaycast());
    }



    private Collider[] InteractionRaycast()
    {
        float interactionRadius = 5f;
        Collider[] overlapSphere = new Collider[20];
        
        Physics.OverlapSphereNonAlloc(transform.position, interactionRadius, overlapSphere, LayerMask.GetMask("Interactable"));
        
        return overlapSphere;
    }
    
    //Iterates through each found interactactable GameObject and selects the most front-facing one.
    private IInteractable InteractionFilter(Collider[] interactables)
    {
        if (interactables.Length <= 0) return null;
        
        IInteractable target = null;

        foreach (var interactable in interactables)
        {
            if (!interactable.TryGetComponent<IInteractable>(out var component ) && target == component ) continue;

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



