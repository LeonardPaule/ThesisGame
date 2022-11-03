using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour

{
    public int coinValue = 1;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Character"))
        {

            CoinCounter.instance.ChangeScore(coinValue);
            Destroy(gameObject);
        }
    }
}
