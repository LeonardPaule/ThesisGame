using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class StopWatch : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    int seconds = 0;
    int min = 0;
    double countCD = 1;
    
    float startTime;
    float elapsedTime; 
    
    public static int finalTime;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        finalTime = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        elapsedTime = Time.time - startTime;
        if (elapsedTime >= countCD)
        {
            countCD = elapsedTime + 1.0f;
            

            if (seconds == 59)
            {
                min++;
                seconds = 0;
            }
            else
            {
                seconds++;
            }
            timeText.text = string.Format("= {0:00}:{1:00}", min, seconds);
            finalTime = (int) Math.Round(elapsedTime);
            
           
        }
    }
}
