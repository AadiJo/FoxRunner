using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class username : MonoBehaviour
{
    [SerializeField] TMP_Text nameInput;
    [SerializeField] TextMeshProUGUI nameDisplay;
    [SerializeField] GameObject deleteNameButton;


    private void Awake()
    {
        if (PlayerPrefs.GetString(name) != "")
        {
            MainMenu.player_Name = PlayerPrefs.GetString(name);
        }

    }
        
    public void UpdateName()
    {   
        MainMenu.player_Name = nameInput.text;
        PlayerPrefs.SetString(name, MainMenu.player_Name);
    }

    public void DeleteName()
    {
        PlayerPrefs.SetString(name, "");
        MainMenu.player_Name = "";
        
        
    }

}
