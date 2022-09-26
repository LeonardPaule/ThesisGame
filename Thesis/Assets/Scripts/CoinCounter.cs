using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinCounter : MonoBehaviour
{
    public static CoinCounter instance;
    public TextMeshProUGUI text;

    public static int score = 0;
    public AudioSource coinSound;
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        score = 0;
    }
    public void ChangeScore(int coinValue)
    {

        score += coinValue;

        coinSound.Play();

        text.text = "= " + score.ToString();

        

    }


}
