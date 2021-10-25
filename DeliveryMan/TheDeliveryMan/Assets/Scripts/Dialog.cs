using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Dialog : MonoBehaviour
{
    public TextMeshProUGUI textdisplay;
    public PlayerController player;
    public string[] sentences;
    private int index;
    public float typingSpeed;

    private void Start()
    {
        GameObject g = GameObject.FindGameObjectWithTag("Player");
        player = g.GetComponent<PlayerController>();
        StartCoroutine(Type());
    }

    IEnumerator Type()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            Debug.Log(index);
            player.canmove = false; //player cant move during dialog
            textdisplay.text += letter; //write one by one


            yield return new WaitForSeconds(typingSpeed);
            int a = 33;
        }
        while (true) {
            if (Input.GetKey(KeyCode.E))
            {
                NextSentence();
                break;
            }
        }
        

        Debug.Log(sentences.Length);


    }
    public void NextSentence()
    {
        if (index < sentences.Length - 1)
        {
            index++;
            textdisplay.text = "";
            StartCoroutine(Type());

        }
        else
        {
            textdisplay.text = "";
        }

    }
}
