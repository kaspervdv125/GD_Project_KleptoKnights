using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamScoreCounter : MonoBehaviour
{
    public int TeamScore;

    public UI[] _playerUis;

    // Start is called before the first frame update
    void Start()
    {
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        Collider[] colliders = Physics.OverlapBox(boxCollider.center + transform.position, boxCollider.size / 2, Quaternion.identity, LayerMask.NameToLayer("Interactable"), QueryTriggerInteraction.Collide);

        foreach (var collider in colliders)
        {
            TeamScore += collider.GetComponent<ObjectValue>().Value;
        }

        GameObject[] teamPlayers = GameObject.FindGameObjectsWithTag(name);
        _playerUis = new UI[teamPlayers.Length];

        for (int i = 0; i < teamPlayers.Length; i++)
        {
            _playerUis[i] = teamPlayers[i].GetComponent<UI>();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(name + ": " + TeamScore);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Interactable"))
        {
            TeamScore += other.gameObject.GetComponent<ObjectValue>().Value;

            SetPlayerScore();
        }
    }

    private void SetPlayerScore()
    {
        foreach (var player in _playerUis)
        {
            player.ChangeScore(TeamScore);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Interactable"))
        {
            TeamScore -= other.gameObject.GetComponent<ObjectValue>().Value;
            SetPlayerScore();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        
    }

    private void OnDrawGizmos()
    {
        BoxCollider boxCollider = GetComponent<BoxCollider>();

        Gizmos.DrawWireCube(boxCollider.center + transform.position, boxCollider.size);
    }
}
