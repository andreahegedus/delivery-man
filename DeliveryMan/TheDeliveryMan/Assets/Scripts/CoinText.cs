using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinText : MonoBehaviour
{
    // Start is called before the first frame update
    Text text;
    public static int CoinAmount;

    void Start() {
        text = GetComponent<Text>();
    }

    void Update() {
        text.text = CoinAmount.ToString();    
    }
}
