using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerPickup : MonoBehaviour, IInteractable
{
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
            //classes.Class = Classes.PlayerClass.Builder;
            classes.ChangeClass(Classes.PlayerClass.Builder);
        }
        Debug.Log("Builder");
    }
    private void Update()
    {

    }
    public void EndInteract(GameObject interactor)
    {

    }
}
