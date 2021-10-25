using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public AudioSource selectSound;
    // Start is called before the first frame update
    public void PlayGame()
    {
        selectSound.Play();
         SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        selectSound.Play();
        Application.Quit();
    }
   
}
