using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class username : MonoBehaviour
{
    [SerializeField] TMP_Text nameInput;
    [SerializeField] TextMeshProUGUI nameDisplay;
    [SerializeField] GameObject namePanel;
    [SerializeField] GameObject deleteNameButton;


    private void Awake()
    {
        if (PlayerPrefs.GetString(name) != "")
        {
            MainMenu.player_Name = PlayerPrefs.GetString(name);
        }
    }
    private void Update()
    {
        if (MainMenu.player_Name != null)
        {
            namePanel.SetActive(false);
            deleteNameButton.SetActive(true);
        }

        else
        {
            namePanel.SetActive(true);
            deleteNameButton.SetActive(false);
        }
        
    }
    public void UpdateName()
    {   
        MainMenu.player_Name = nameInput.text;
        PlayerPrefs.SetString(name, MainMenu.player_Name);
        namePanel.SetActive(false);
    }

    public void DeleteName()
    {
        MainMenu.player_Name = "";
        PlayerPrefs.SetString(name, "");
        
    }

}
