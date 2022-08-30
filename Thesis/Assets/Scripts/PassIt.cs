using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PassIt : MonoBehaviour
{
    int sceneManager;

    public static int level;
    public static int finalTime;
    public static int finalCoins;
    public static int finalDeaths;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Awake()
    {
        sceneManager = SceneManager.GetActiveScene().buildIndex;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Character")
        {
            finalTime = StopWatch.finalTime;
            finalCoins = CoinCounter.score;
            finalDeaths = DeathCounter.deathCount;

            if (sceneManager == 3)
            {
                level = 1;
            }else if(sceneManager == 4)
            {
                level = 2;
            }else if(sceneManager == 5)
            {
                level = 3;
            }
            SceneManager.LoadScene(6);

        }
    }
  
}
