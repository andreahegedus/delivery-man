using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coinable : MonoBehaviour
{
    public AudioSource Coinsound;
    public GameObject CoinBarPanel;
    
    void OnTriggerStay2D(Collider2D col) 
    {
        if (Input.GetKey(KeyCode.E))
            {
                CoinText.CoinAmount += 1; //DontDestroyOnLoad("variable");
                Coinsound.Play();
                Destroy (gameObject);
                Debug.Log("CoinHolder detected");
                Debug.Log("coins = " + CoinText.CoinAmount);

                if ( CoinText.CoinAmount != 0 )
                        {
                            CoinBarPanel.SetActive(true);
                        }
                        else CoinBarPanel.SetActive(false);
            }
    }
       
}
