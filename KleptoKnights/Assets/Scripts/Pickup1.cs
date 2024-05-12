using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup1 : MonoBehaviour, IInteractable
{
    
    // IIinteractable
    
    public string Name { get; } = "item";
    public string Verb { get; } = "Pick up";
    public bool IsInteractable => !IsHeld;

    // Pickup
    private bool IsHeld { get;  set; }


    public bool IsAvailable(GameObject interactor)
    {
        return IsInteractable;
        
    }

   public void StartInteract(GameObject interactor)
   {
       interactor.GetComponent<Inventory>()?.AddItem(this);
       IsHeld = true;

   }

   public void EndInteract(GameObject interactor)
   {
       IsHeld = false;
   }
}
