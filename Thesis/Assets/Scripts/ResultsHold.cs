using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultsHold : MonoBehaviour
{
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI deathText;

    int level;
    int time;
    int coins;
    int deaths;
    // Start is called before the first frame update
    void Start()
    {
        level = PassIt.level;
        time = PassIt.finalTime;
        coins = PassIt.finalCoins;
        deaths = PassIt.finalDeaths;

        levelText.text = "Level: " + level + " completed";
        timeText.text = "Time: " + time + " seconds";
        coinText.text = "Coins: " + coins + " collected";
        deathText.text = "Deaths: " + deaths + " times";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
