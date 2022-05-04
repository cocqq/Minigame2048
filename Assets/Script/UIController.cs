using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UIController : MonoBehaviour
{   
    private AudioSource audioBackGround;
    private void Start() 
    {
        audioBackGround = gameObject.GetComponent<AudioSource>();
    }
   public void PlayButton()
   {
       SceneManager.LoadScene(1);
   }
   public void QuitButton()
   {
       Application.Quit();
   }
}
