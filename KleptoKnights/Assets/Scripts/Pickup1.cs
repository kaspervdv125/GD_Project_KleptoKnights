using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class Pickup1 : MonoBehaviour, IInteractable
{
    
    // IIinteractable
    
    public string Name { get; } = "item";
    public string Verb { get; } = "Pick up";

    [SerializeField] private ObjectValue objectValue;
    
    public bool IsInteractable => !IsHeld;
    public AudioSource TreasureSource;
    public AudioClip sfxTreasure;
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
            if (timer > objectValue.Value * ObjectValue.ValuePickupTimeMultiplier)
            {
                PickUpObject.AddItem(this);
                IsHeld = true;
                pickingUp = false;
                timer = 0;
                TreasureSource.clip = sfxTreasure;
                TreasureSource.Play();
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
    }
   public void EndInteract(GameObject interactor)
   {
        pickingUp = false;
        timer = 0;
   }

   public void Drop(GameObject interactor)
    {
        IsHeld = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Water")
        {
            Vector2 targetPosition = Random.insideUnitCircle * 10 + new Vector2(-5, 0);
            transform.position = new Vector3(targetPosition.x, 2.0f, targetPosition.y);
        }
    }
}
