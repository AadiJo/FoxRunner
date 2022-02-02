using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public static float currentTime;
    public TextMeshProUGUI timeDisplay;
    public TextMeshProUGUI finalTimeDisplay;
    public static bool timerActive;
    public static double timerText;

    void Start()
    {
        if (PlayerPrefs.GetFloat("CurrentTime") > 0)
        {
            currentTime = PlayerPrefs.GetFloat("CurrentTime");
        }
        else
        {
            currentTime = 0;
        }
        timerActive = true;
    }

    void Update()
    {   
        if (timerActive)
        {
            currentTime += Time.deltaTime;
        }
        TimeSpan time = TimeSpan.FromSeconds(currentTime);

        timerText = Convert.ToDouble(currentTime.ToString());

        timeDisplay.text = time.ToString(@"mm\:ss\:ff");
        finalTimeDisplay.text = time.ToString(@"mm\:ss\:ff");
        PlayerPrefs.SetFloat("CurrentTime", currentTime);
    }

    public void Stop()
    {
        timerActive = false;
    }
}
