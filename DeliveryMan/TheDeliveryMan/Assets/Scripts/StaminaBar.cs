using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StaminaBar : MonoBehaviour
{
    public Slider staminaBar;

    private float maxStamina = 200;
    private float currentStamina;

    private WaitForSeconds regenTick = new WaitForSeconds(0.1f);
    private Coroutine regen;
    public PlayerController player;

    public static StaminaBar instance; 
    
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        GameObject g = GameObject.FindGameObjectWithTag ("Player");
        player = g.GetComponent<PlayerController> ();
        currentStamina = maxStamina;
        staminaBar.maxValue = maxStamina;
        staminaBar.value = maxStamina;
    }

    public void UseStamina(float amount)
    {
        if(currentStamina - amount >= 0)
        {
            currentStamina -= amount;
            staminaBar.value = currentStamina;
            player.cansprint = true;
            
            if(regen != null)
                StopCoroutine(regen);
            regen = StartCoroutine(RegenStamina());
        }
        else
        {
            Debug.Log("nostamina");
            player.cansprint = false;
        }
    }
    
    private IEnumerator RegenStamina()
    {
        yield return new WaitForSeconds(3);
        
        while (currentStamina < maxStamina)
        {

            currentStamina += maxStamina / 100;
            staminaBar.value = currentStamina;
            yield return regenTick;
            player.cansprint = true;
        }
        regen = null;
    }
 
}
