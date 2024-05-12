using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<Pickup1> _items = new List<Pickup1>();
    [SerializeField] private float handOffset = .5f;
    

    public void AddItem(Pickup1 newItem)
    {
        var itemTransform = newItem.transform;
        Vector3 localOffset = transform.forward * handOffset;

        if (_items.Count >= 1)
        {
            var newBounds = GetMaxBounds(newItem.gameObject);
            var lastBounds = GetMaxBounds(_items.Last().gameObject);

            localOffset.y += lastBounds.extents.y + newBounds.extents.y;
        }
        
        itemTransform.parent = transform.parent;
        itemTransform.localPosition = Vector3.zero;
        itemTransform.localPosition += localOffset;
        _items.Add(newItem);
        
    }

    public void DropItem()
    {
        _items.Last().transform.parent = null;
        _items.Remove(_items.Last());
    }

    // calculates the full bounds of an item's mesh. might produce weird results with odd item shapes.
    private static Bounds GetMaxBounds(GameObject item) 
    {
        var renderers = item.GetComponentsInChildren<Renderer>();
        if (renderers.Length == 0) return new Bounds(item.transform.position, Vector3.zero);
        var b = renderers[0].bounds;
        
        foreach (var r in renderers) 
        {
            b.Encapsulate(r.bounds);
        }
        return b;
    }
}
