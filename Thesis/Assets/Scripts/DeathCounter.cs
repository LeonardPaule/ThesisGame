using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DeathCounter : MonoBehaviour
{
    float deathCD = 0.1f;
    public static int deathCount = 0;
    public TextMeshProUGUI deathText;
    // Start is called before the first frame update
    void Start()
    {
        deathCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "DeathZone" && Time.time > deathCD)
        {
            
            deathCD = Time.time + 0.1f;
            deathCount++;
            deathText.text = ("= " + deathCount.ToString());
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Spike" && Time.time > deathCD)
        {
            deathCD = Time.time + 0.1f;
            deathCount++;
            deathText.text = ("= " + deathCount.ToString());
        }
    }
}
