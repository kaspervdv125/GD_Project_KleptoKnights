using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private float timerTarget = 30f;
    [SerializeField]
    private float coinThreshold = 5; 

    private int coinCounter;

    private bool endGame;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timerTarget -= Time.deltaTime;

        if(timerTarget < 0 | coinThreshold <= coinCounter )
        {
            endGame = true;
        }

    }
}
