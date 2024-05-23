using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    [SerializeField] private CharacterControl controller;
    private List<Pickup1> _items = new List<Pickup1>();
    private float handOffset = 1.25f;
    private float _throwForce = 5f;

    [SerializeField] private Classes _playerClass;

    public int HeldWeight
    {
        get
        {
            int totalWeight = 0;

            foreach (var item in _items)
            {
                int itemWeight = item.GetComponent<ObjectValue>().Value;
                totalWeight += itemWeight;
            }

            return totalWeight;
        }
    }

    public int WeightLimit
    {
        get
        {
            return _playerClass.WeightLimit;
        }
    }

    private void Update()
    {
        WobbleItems();
    }

    private void WobbleItems()
    {
        int i = 0;
        Vector3 direction = transform.InverseTransformDirection(controller.Velocity.normalized  * -.1f);

        direction = -controller.AverageVelocity.normalized;
        
        foreach (var item in _items)
        {
            direction = - item.GetComponent<Rigidbody>().velocity;

            Vector3 pos = item.transform.localPosition;
            pos.x =  direction.x * -i;
            pos.z = direction.z * i;
            
            // fix this maybe idk it works but could be cleaner
            if (i == 0) pos.z = handOffset;

            Transform itemTransform = item.transform;
            itemTransform.localRotation = Quaternion.Euler(Vector3.right * (controller.Velocity.magnitude * .15f * -i));
            itemTransform.localPosition = pos;
            
            i++;
        }
        
    }
    

    public void AddItem(Pickup1 newItem)
    {
        int newItemWeight = newItem.GetComponent<ObjectValue>().Value;
        int weightLimit = GetComponent<Classes>().WeightLimit;

        if (HeldWeight + newItemWeight > weightLimit)
        {
            return;
        }

        var itemTransform = newItem.transform;
        Vector3 localOffset = new Vector3(0, 0, handOffset);

        if (_items.Count >= 1)
        {
            var newBounds = GetMaxBounds(newItem.gameObject);
           //  var lastBounds = GetMaxBounds(_items.Last().gameObject);
            

            // localOffset.y = lastBounds.extents.y + newBounds.extents.y + _items.Last().transform.localPosition.y;
            localOffset.y = newBounds.extents.y * 2f;
            
            itemTransform.parent = _items.Last().transform;
        }
        else
        {
            var newBounds = GetMaxBounds(newItem.gameObject);
            //newBounds.center = transform.position;
            localOffset.y = newBounds.min.y + 1.0f;
            itemTransform.parent = transform;
        }


        itemTransform.localPosition = localOffset; 
            
        
        itemTransform.rotation = Quaternion.Euler(Vector3.zero);

        newItem.gameObject.layer = LayerMask.NameToLayer("HeldObject");
        newItem.GetComponent<Rigidbody>().isKinematic = true;

        _items.Add(newItem);

        UpdateUi();
    }

    private void UpdateUi()
    {
        UI ui = GetComponent<UI>();

        ui.UpdateWeightBar(HeldWeight, WeightLimit);
    }

    public void DropItem()
    {
        if (_items.Count == 0)
        {
            return;
        }  

        _items.Last().gameObject.layer = 8;
        
        Rigidbody itemBody = _items.Last().GetComponent<Rigidbody>();
        itemBody.isKinematic = false;
        itemBody.AddForce(transform.forward * _throwForce, ForceMode.Impulse);
        
        _items.Last().transform.parent = null;
        _items.Last().Drop(gameObject);     


        _items.Remove(_items.Last());

        UpdateUi();
    }

    public void DropAllItems()
    {
        for (int i = _items.Count - 1; i >= 0; i--)
        {
            DropItem();
        }
    }

    // calculates the full bounds of an item's mesh. might produce weird results with odd item shapes.
    private static Bounds GetMaxBounds(GameObject item) 
    {
        Collider collider = item.GetComponent<Collider>();
        collider.transform.rotation = Quaternion.identity;
        Bounds bounds = collider.bounds;
        bounds.center = Vector3.zero;
        return bounds;
    }

    private void OnDrawGizmos()
    {
        Vector3 drawPosition = transform.position + transform.forward * handOffset + transform.up * 1f;
        Gizmos.DrawWireSphere(drawPosition, .5f);
    }
}
