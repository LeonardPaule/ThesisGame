using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StopWatch : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    int seconds = 0;
    int min = 0;
    float countCD = 1;
    
    float startTime;
    float elapsedTime;  

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        elapsedTime = Time.time - startTime;
        print(elapsedTime);
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
            print(elapsedTime);
        }
    }
}
