using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{   
    public static string player_Name;
    [SerializeField] TMP_Text nameDisplay;
    [SerializeField] TMP_Text playerSwapDisplay;
    [SerializeField] GameObject namePanel;

    private void Awake()
    {
        PlayerPrefs.SetFloat("RespawnX", CharacterController2D.initialCoords.x);
        PlayerPrefs.SetFloat("RespawnY", CharacterController2D.initialCoords.y);
        PlayerPrefs.SetFloat("CurrentTime", 0);
        CharacterController2D.noWarp = true;
        CharacterController2D.levelComplete = false;
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void DeleteData()
    {
        BestTime.Lvl1finalTime = "00:00:00";
        BestTime.Lvl2finalTime = "00:00:00";
        BestTime.Lvl3finalTime = "00:00:00";
        BestTime.Level1BestTimes.Clear();
        BestTime.Level2BestTimes.Clear();
        PlayerPrefs.SetString("Level1Best", "00:00:00");
        PlayerPrefs.SetString("Level2Best", "00:00:00");
        PlayerPrefs.SetString("Level3Best", "00:00:00");
    }

    private void Update()
    {
        nameDisplay.text = player_Name;
        playerSwapDisplay.text = player_Name;
        if (player_Name != null && player_Name != "") 
        {
            namePanel.SetActive(false);
        }

        else
        {
            namePanel.SetActive(true);
        }
    }
}
