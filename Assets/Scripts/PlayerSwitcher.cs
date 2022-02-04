using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerSwitcher : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public TMP_Text Initialtext;
    void Start()
    {
        Initialtext.text = MainMenu.player_Name;
        dropdown.options.Clear();
        dropdown.options.Add(new TMP_Dropdown.OptionData() { text = MainMenu.player_Name });


    }

    private void Update()
    {
        if (Initialtext.text != MainMenu.player_Name)
        {
            Initialtext.text = MainMenu.player_Name;
            dropdown.options.Clear();
            dropdown.options.Add(new TMP_Dropdown.OptionData() { text = MainMenu.player_Name });

        }
    }


}
