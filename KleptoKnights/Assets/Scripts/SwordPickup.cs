using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordPickup : MonoBehaviour, IInteractable
{
    // IIinteractable



    public string Name { get; } = "item";
    public string Verb { get; } = "Pick up";

    public bool IsInteractable => !IsHeld;
    private Inventory PickUpObject;
    // Pickup
    private bool IsHeld { get; set; }


    public bool IsAvailable(GameObject interactor)
    {
        return IsInteractable;

    }

    public void StartInteract(GameObject interactor)
    {
        PickUpObject = interactor.GetComponent<Inventory>();
        Classes classes = interactor.GetComponent<Classes>();
        if (classes != null)
        {
            //classes.Class = Classes.PlayerClass.Knight;
            classes.ChangeClass(Classes.PlayerClass.Knight);
        }
        Debug.Log("Knight!");
    }
    private void Update()
    {
        
    }
    public void EndInteract(GameObject interactor)
    {

    }
}
