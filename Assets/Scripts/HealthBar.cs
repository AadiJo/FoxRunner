using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    CharacterController2D playercontroller;
    [SerializeField] GameObject player;


    private void Awake()
    {
        playercontroller = player.GetComponent<CharacterController2D>();
        
    }

    public void SetHealth()
    {
        if (!CharacterController2D.levelComplete)
        {
            slider.value = playercontroller.health;
        }
        
    }

    private void Update()
    {
        SetHealth();
    }
}
