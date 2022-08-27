using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinCounter : MonoBehaviour
{
    public static CoinCounter instance;
    public TextMeshProUGUI text;

    int score = 0;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void ChangeScore(int coinValue)
    {

        score += coinValue;
        text.text = "= " + score.ToString();

        print("Score: " + score);

    }


}
