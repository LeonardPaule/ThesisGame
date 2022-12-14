using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DeathCounter : MonoBehaviour
{

    float deathCD = 0.1f;
    public static int deathCount = 0;
    public TextMeshProUGUI deathText;

    public AudioSource hurtSound;
    // Start is called before the first frame update
    void Start()
    {
        deathCount = 0;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "DeathZone" && Time.time > deathCD)
        {

            deathCD = Time.time + 0.1f;
            deathCount++;

            hurtSound.Play();
            deathText.text = ("= " + deathCount.ToString());
            changeMusic();

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Spike" && Time.time > deathCD)
        {
            deathCD = Time.time + 0.1f;

            hurtSound.Play();

            deathCount++;
            deathText.text = ("= " + deathCount.ToString());
            changeMusic();


        }
    }
    public void changeMusic()
    {
        //Make a prediction every n deaths
        if (deathCount % 2 == 0)
        {
            gameObject.GetComponent<NNReadModel>().Init();
            gameObject.GetComponent<MusicController>().PlayPredict();
        }

    }
}
