using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup1 : MonoBehaviour, IInteractable
{
    
    // IIinteractable
    
    public string Name { get; } = "item";
    public string Verb { get; } = "Pick up";
    [SerializeField]
    private ObjectValue objectValue = new ObjectValue();
    
    public bool IsInteractable => !IsHeld;
    [SerializeField]
    private float timer;
    private bool pickingUp;
    private Inventory PickUpObject;
    // Pickup
    private bool IsHeld { get;  set; }


    public bool IsAvailable(GameObject interactor)
    {
        return IsInteractable;
        
    }

   public void StartInteract(GameObject interactor)
   {
         PickUpObject = interactor.GetComponent<Inventory>();
        pickingUp = true;
        Debug.Log("True");
   }
    private void Update () 
    {
        if (pickingUp)
        {
            if (timer > objectValue.Value / 10)
            {
                PickUpObject.AddItem(this);
                IsHeld = true;
                timer = 0;
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
    }
   public void EndInteract(GameObject interactor)
   {
       IsHeld = false;
        pickingUp = false;
        timer = 0;
   }
}
