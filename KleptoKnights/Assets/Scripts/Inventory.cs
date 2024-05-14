using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<Pickup1> _items = new List<Pickup1>();
    private float handOffset = 1.5f;
    private float _throwForce = 5f;
    private void Update()
    {
        WobbleItems();
    }

    private void WobbleItems()
    {
        foreach (var item in _items)
        {
            
        }
    }
    

    public void AddItem(Pickup1 newItem)
    {
        var itemTransform = newItem.transform;
        Vector3 localOffset = new Vector3(0, 0, handOffset);

        if (_items.Count >= 1)
        {
            var newBounds = GetMaxBounds(newItem.gameObject);
            var lastBounds = GetMaxBounds(_items.Last().gameObject);

            localOffset.y = lastBounds.extents.y + newBounds.extents.y + _items.Last().transform.localPosition.y;
        }
        else
        {
            var newBounds = GetMaxBounds(newItem.gameObject);
            localOffset.y = newBounds.extents.y + 0.5f;
        }
        
        itemTransform.parent = transform;
        itemTransform.localPosition = localOffset;

        newItem.gameObject.layer = LayerMask.NameToLayer("HeldObject");
        newItem.GetComponent<Rigidbody>().isKinematic = true;

        _items.Add(newItem);
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
        _items.Last().EndInteract(gameObject);     


        _items.Remove(_items.Last());
    }

    // calculates the full bounds of an item's mesh. might produce weird results with odd item shapes.
    private static Bounds GetMaxBounds(GameObject item) 
    {

        Collider colliders = item.GetComponent<Collider>();
        Bounds bounds = colliders.bounds;
        return bounds;
    }

    private void OnDrawGizmos()
    {
        Vector3 drawPosition = transform.position + transform.forward * handOffset + transform.up * 1f;
        Gizmos.DrawWireSphere(drawPosition, .5f);
    }
}
