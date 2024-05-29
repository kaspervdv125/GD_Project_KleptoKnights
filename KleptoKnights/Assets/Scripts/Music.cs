using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public AudioSource MusicSource1, MusicSource2;
    public AudioClip MusicClip;

    private void Start()
    {
        MusicSource1.volume = 0.5f;
        MusicSource2.volume = 0.5f;

    }

    // Update is called once per frame
    void Update()
    {
        if (!MusicSource1.isPlaying)
        {
          MusicSource1.clip = MusicClip;
          MusicSource1.Play();
        
        }
        if (!MusicSource2.isPlaying)
        {
            MusicSource2.clip = MusicClip;
            MusicSource2.Play();

        }
    }
}
