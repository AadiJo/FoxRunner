using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class usernameText : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] TMP_Text usernameDisplay;

    private void Update()
    {
        usernameDisplay.text = MainMenu.player_Name.ToString();
        transform.position = new Vector2(player.transform.position.x, player.transform.position.y + 0.7f);
        if (PlayerMovement.crouch && !CharacterController2D.isHurt)
        {
            if (CharacterController2D.m_FacingRight)
            {
                transform.position = new Vector2(player.transform.position.x + 0.15f, transform.position.y - 0.3f);
            }

            if (!CharacterController2D.m_FacingRight)
            {
                transform.position = new Vector2(player.transform.position.x - 0.15f, transform.position.y - 0.3f);
            }
        }

        if (CharacterController2D.levelComplete)
        {
            usernameDisplay.text = "";
        }
    }
}
