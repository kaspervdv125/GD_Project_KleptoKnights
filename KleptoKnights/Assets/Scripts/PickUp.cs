using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private GameObject _topItem;
    private List<GameObject> _items = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        _topItem = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            DropItems();
        }
    }

    private void DropItems()
    {
        foreach (GameObject item in _items)
        {
            item.transform.SetParent(null, true);
            //item.GetComponent<Rigidbody>().useGravity = true;
        }
        _items.Clear();
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "PickUp")
    //    {
    //        collision.collider.enabled = false;
    //        collision.rigidbody.useGravity = false;

    //        collision.transform.position = _topItem.transform.position + Vector3.up * 0.25f + Vector3.forward * 0.25f;
    //        collision.transform.parent = _topItem.transform;

    //        _items.Add(collision.gameObject);

    //        _topItem = collision.gameObject;
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PickUp")
        {
            other.GetComponent<BoxCollider>().enabled = false;
            other.GetComponent<Rigidbody>().useGravity = false;

            other.transform.position = _topItem.transform.position + Vector3.up * 0.25f + Vector3.forward * 0.25f;
            other.transform.parent = _topItem.transform;

            _items.Add(other.gameObject);

            _topItem = other.gameObject;
        }
    }
}
