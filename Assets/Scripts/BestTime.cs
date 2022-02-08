using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using System.Linq;

public class BestTime : MonoBehaviour
{
    public static List<double> Level1BestTimes = new List<double>();
    public static List<double> Level2BestTimes = new List<double>();
    public static List<double> Level3BestTimes = new List<double>();
    public static string Lvl1finalTime = "00:00:00";
    public static string Lvl2finalTime = "00:00:00";
    public static string Lvl3finalTime = "00:00:00";
    [SerializeField] TextMeshProUGUI bestTimeText;
    [SerializeField] int bestTimeID;


    private void Awake()
    {
        if (PlayerPrefs.GetString("Level1Best") != "00:00:00" && PlayerPrefs.GetString("Level1Best") != "")
        {
            Lvl1finalTime = PlayerPrefs.GetString("Level1Best");
        }
        if (PlayerPrefs.GetString("Level2Best") != "00:00:00" && PlayerPrefs.GetString("Level2Best") != "")
        {
            Lvl2finalTime = PlayerPrefs.GetString("Level2Best");
        }

        if (PlayerPrefs.GetString("Level3Best") != "00:00:00" && PlayerPrefs.GetString("Level3Best") != "")
        {
            Lvl3finalTime = PlayerPrefs.GetString("Level3Best");
        }
    }

    private void Update()
    {
        PlayerPrefs.SetString("Level1Best", Lvl1finalTime);
        PlayerPrefs.SetString("Level2Best", Lvl2finalTime);
        PlayerPrefs.SetString("Level3Best", Lvl3finalTime);

        GetTime(bestTimeID);

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            

            if (CharacterController2D.levelComplete)
            {
                Level1BestTimes.Add(Timer.timerText);

            }
            
            TimeSpan time = TimeSpan.FromSeconds(Level1BestTimes.Min());
            Lvl1finalTime = time.ToString(@"mm\:ss\:ff");
        }

        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            

            if (CharacterController2D.levelComplete)
            {
                Level2BestTimes.Add(Timer.timerText);

            }



            TimeSpan time = TimeSpan.FromSeconds(Level2BestTimes.Min());
            Lvl2finalTime = time.ToString(@"mm\:ss\:ff");

        }

        if(SceneManager.GetActiveScene().buildIndex == 3)
        {


            if (CharacterController2D.levelComplete)
            {
                Level3BestTimes.Add(Timer.timerText);

            }



            TimeSpan time = TimeSpan.FromSeconds(Level3BestTimes.Min());
            Lvl3finalTime = time.ToString(@"mm\:ss\:ff");

        }

        




    }

    public void GetTime(int id)
    {
        if (id == 1)
        {
            if (Lvl1finalTime != "00:00:00")
            {
                bestTimeText.text = Lvl1finalTime;
                PlayerPrefs.SetString("Level1Best", Lvl1finalTime);
                
            }

            else
            {
                bestTimeText.text = "00:00:00";
            }
            
        }

        if (id == 2)
        {
            if (Lvl2finalTime != "00:00:00")
            {
                bestTimeText.text = Lvl2finalTime;
                PlayerPrefs.SetString("Level2Best", Lvl2finalTime);
            }

            else
            {
                bestTimeText.text = "00:00:00";
            }
        }

        if (id == 3)
        {
            if (Lvl3finalTime != "00:00:00")
            {
                bestTimeText.text = Lvl3finalTime;
                PlayerPrefs.SetString("Level3Best", Lvl3finalTime);
            }

            else
            {
                bestTimeText.text = "00:00:00";
            }
        }
    }
}
