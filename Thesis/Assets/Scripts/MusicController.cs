using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioSource SLOW;
    public AudioSource MED;
    public AudioSource FAST;

    // Start is called before the first frame update
    void Start()
    {
        MED.Play();
    }
    
    public void PlayPredict()
    {
        if (NNReadModel.tempo == 0)
        {
            MED.Stop();
            FAST.Stop();
            if (!SLOW.isPlaying)
            {
                SLOW.Play();
            }
            print("we go slow");
        }
        if (NNReadModel.tempo == 1)
        {
            SLOW.Stop();
            FAST.Stop();
            if (!MED.isPlaying)
            {
                MED.Play();
            }
            print("we go moderate");
        }
        if (NNReadModel.tempo == 2)
        {
            SLOW.Stop();
            MED.Stop();
            if (!FAST.isPlaying)
            {
                FAST.Play();
            }
            
            print("we go fast");
        }

    }

}
