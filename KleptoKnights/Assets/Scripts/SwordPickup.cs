using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordPickup : MonoBehaviour, IInteractable
{
    // IIinteractable

    public string Name { get; } = "item";
    public string Verb { get; } = "Pick up";
    public bool IsInteractable => !IsHeld;

    // Pickup
    private bool IsHeld { get; set; }


    public bool IsAvailable(GameObject interactor)
    {
        return IsInteractable;

    }

    public void StartInteract(GameObject interactor)
    {
        interactor.GetComponent<Inventory>()?.AddItem(this);
        IsHeld = true;

        var playerComponent = interactor.GetComponent<Classes.Player>();
        if (playerComponent != null)
        {
            var swordItem = new Classes.Item(Classes.ItemType.Sword);
            playerComponent.PickupItem(swordItem);
            Destroy(gameObject); // Destroy the sword after picking it up

            
        }
    }

    public void EndInteract(GameObject interactor)
    {
        IsHeld = false;
    }
}
