using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public GameObject StaminaBarPanel;
    //public GameObject CoinBarPanel;
    public int speed; //walking speed
    private float currentSpeed;
    private float move; //value of moving left or right
    public int sprint; ///sprinting speed
    private int maxsprint = 3; //sprint value
    private bool iswalking;
    private bool isrunning;
    public bool canmove; //for future dialog system
    public bool cansprint; //for stamina system
    private Rigidbody2D rb;
    private Animator anim;
    public AudioSource walkingsound;
    public AudioSource pickupnoise;

    public AudioSource crowbarSound;

    public bool isTheCrowbarPickedUp = false;

    //public AudioSource coinsound;
    
    public int coin = 0;


    // Start is called before the first frame update
    void Start()
    {
        cansprint = true;
        canmove = true;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    
    void Update()
    {  

        //////////////MAKE CHARACTER MOVE/////////////////////////////////////////
        //////////////RUNNING//////////////////////////////////////////////////////
        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (canmove) //for future dialog system
            {
                if (cansprint) /////Is there enough stamina? //related to stamina bar
                {
                    if(move != 0) // if input is not zero
                    {
                        if(sprint <= maxsprint) //only add to speed until it reaches the sprint value
                        {                                   
                            currentSpeed =speed + sprint;
                            anim.SetBool("issprinting", true);
                            StaminaBar.instance.UseStamina(1); //value kivonva currentstamina
                        }
                    }
                } else currentSpeed = speed; anim.SetBool("hasstamina", false); anim.SetBool("iswalking", true); 
            }
        }
        else
            {
                
                currentSpeed = speed;  
                anim.SetBool("issprinting", false); 
            }
         //////////////RUNNING//////////////////////////////////////////////////////   
        if (canmove)
        {
            move = Input.GetAxis("Horizontal");
            transform.Translate(move* currentSpeed * Time.deltaTime, 0f,0f);
        }
          //FLIPPING WHEN GOING LEFT OR RIGHT.///
        Vector3 characterScale =  transform.localScale;
        if (move < 0 )
        {
            characterScale.x = -1f; 
            
        }
        if (move > 0 )
        {
            characterScale.x =  1f; 
            
        }
        if (move != 0)
        {
            anim.SetBool("iswalking", true);
            iswalking = true;
            
        }
        else {
            anim.SetBool("iswalking", false);
            walkingsound.Play(); // walking in forest sound
        }
        transform.localScale = characterScale;


        ////HITTING!!!


        if (Input.GetButtonDown("Fire1") && isTheCrowbarPickedUp == true) // támadás ha van crowbar
        {
            if (canmove && cansprint) //for future dialog system
            {
                Debug.Log("Hitting!");
                anim.SetBool("usingcrowbar", true);
                StaminaBar.instance.UseStamina(25);
                crowbarSound.Play();
        
            }
        }else anim.SetBool("usingcrowbar", false);
        //FLIPPING WHEN GOING LEFT OR RIGHT.///
        //////////////MAKE CHARACTER MOVE/////////////////////////////////////////     

        /*if (Input.GetKey(KeyCode.R))
        {
             //Get current scene name
            string scene = SceneManager.GetActiveScene().name;
            //Load it
            SceneManager.LoadScene(scene, LoadSceneMode.Single);
        }   */          
    }

 private void OnTriggerEnter2D(Collider2D other) 
    {      
        ///moved to ontriggerstay
    }

    private void OnTriggerStay2D(Collider2D other) 
    {
        if (other.tag == "Teleporter") SceneManager.LoadScene(2);   //lvl1  

        else if (other.tag == "TeleorterwInput" && !isTheCrowbarPickedUp)  //in lobby at staircase
        {
            if (Input.GetButtonDown("Submit"))
            {
                SceneManager.LoadScene("LevelThree");
                Debug.Log("Level 3");
            }   
            if (isTheCrowbarPickedUp == true) Debug.Log("Crowbar is true");
            else Debug.Log("Crowbar is false");                    
        }
        else if (other.tag == "TeleorterwInput" && isTheCrowbarPickedUp == true)  //in lobby at staircase
        {
            if (Input.GetButtonDown("Submit"))
            {
                SceneManager.LoadScene("LevelThree_2");
                Debug.Log("LevelThree_2");
            }  
            if (isTheCrowbarPickedUp == true) Debug.Log("Crowbar is true");
            else Debug.Log("Crowbar is false");                     
        }

        else if (other.tag == "LevelTwo-ReturnMap"  ) //at staircase on level3
        {
            if (Input.GetButtonDown("Submit"))
            {
                SceneManager.LoadScene("LevelTwo-ReturnMap");
                Debug.Log("lobby");
            }
        }  

        else if (other.tag == "BreakoutRoom") //at breakoutroom in lobby
        {
            if (Input.GetButtonDown("Submit"))
            {
                SceneManager.LoadScene("BreakoutRoom");
                Debug.Log("BreakoutRoom");
            }
        }  

         else if (other.tag == "LevelTwo_return3" ) //in breakout room
        {
            if (Input.GetButtonDown("Submit"))
            {
                SceneManager.LoadScene("LevelTwo-Return3");
                Debug.Log("Lobby");
            }
        }  

        else if (other.tag == "crowbar") 
        {
            if (Input.GetButtonDown("Submit"))
            {
                Destroy(other.gameObject);
                Debug.Log("Crowbar picked up");
                pickupnoise.Play();
                isTheCrowbarPickedUp = true;                
                
            }
            if (isTheCrowbarPickedUp == true) Debug.Log("Crowbar is true");
            else Debug.Log("Crowbar is false");
        }  

        else if (other.tag == "Level1") 
        {
            if (Input.GetButtonDown("Submit"))
            {                
                Debug.Log("LEVEL1 ENTERED");
                isTheCrowbarPickedUp = true;
                SceneManager.LoadScene("levelap3");

                
            }
            if (isTheCrowbarPickedUp == true) Debug.Log("Crowbar is true");
            else Debug.Log("Crowbar is false");
        }  

        else if (other.tag == "level3fl1") 
        {
            if (Input.GetButtonDown("Submit"))
            {
                Debug.Log("Level 3 door 1");
                isTheCrowbarPickedUp = true;                
                SceneManager.LoadScene("LevelThree_2");
            }
        }  

        else if (other.tag == "crowbarenabler") 
        {
           isTheCrowbarPickedUp = true;
        }                
    }
        /*
        else if (other.tag == "CoinHolder") //for coins
        {
            if (Input.GetKey(KeyCode.E))
            {
                coin = coin + 1 ;
                coinsound.Play();
                other.transform.gameObject.tag = "NotCoinHolder"; //this makes it so you can only pick up coins once.
                Debug.Log("CoinHolder detected");

                if ( coin != 0 )
                {
                    CoinBarPanel.SetActive(true);
                }
                else CoinBarPanel.SetActive(false);
            }
        }
        else if (other.tag == "NotCoinHolder")
        {
            Debug.Log("Not coin holder detected.");
        }                

    *////MOVED TO NEW SCRIPT
}


