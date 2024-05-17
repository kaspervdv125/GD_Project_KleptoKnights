using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamScoreCounter : MonoBehaviour
{
    public int TeamScore;

    public GlobalUI GlobalUI;

    public LayerMask ObjectLayers;

    // Start is called before the first frame update
    void Start()
    {
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        Collider[] colliders = Physics.OverlapBox(boxCollider.center + transform.position, boxCollider.size / 2, Quaternion.identity, LayerMask.NameToLayer("Interactable"), QueryTriggerInteraction.Collide);

        foreach (var collider in colliders)
        {
            TeamScore += collider.GetComponent<ObjectValue>().Value;
        }        
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.layer == LayerMask.NameToLayer("Interactable"))
        //{
        //    TeamScore += other.gameObject.GetComponent<ObjectValue>().Value;

        //    SetPlayerScore();
        //}

        CalculateScore();
        GlobalUI.SetScore(this.gameObject);
    }

    private void CalculateScore()
    {
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        Collider[] colliders = Physics.OverlapBox(boxCollider.center + transform.position, boxCollider.size / 2, Quaternion.identity, ObjectLayers, QueryTriggerInteraction.Collide);

        int tempScore = 0;

        foreach (var collider in colliders)
        {
            tempScore += collider.GetComponent<ObjectValue>().Value;
        }

        TeamScore = tempScore;
    }

    private void OnTriggerExit(Collider other)
    {
        //if (other.gameObject.layer == LayerMask.NameToLayer("Interactable"))
        //{
        //    TeamScore -= other.gameObject.GetComponent<ObjectValue>().Value;
        //    SetPlayerScore();
        //}

        CalculateScore();
        
    }

    private void OnDrawGizmos()
    {
        BoxCollider boxCollider = GetComponent<BoxCollider>();

        Gizmos.DrawWireCube(boxCollider.center + transform.position, boxCollider.size);
    }
}
