using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public CharacterController2D CharacterController2D;
    public void Setup()
    {
        gameObject.SetActive(true);
        
       
    }

    public void ShutDown()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        CharacterController2D.health = 10;
        
    }

}
