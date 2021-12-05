using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerLives : MonoBehaviour
{
    public int playerHP = 10;
    public TMP_Text playerHP_text;
    public TMP_Text gameOver_text;


    private void OnTriggerEnter2D(Collider2D collision)
    {
       // Debug.Log("Encontrado" + collision.gameObject.name);
        if (collision.gameObject.name == "Red Butterfly(Clone)")
        {
            Debug.Log("RemoveHP detected");
            RemovePlayerLife(1);
            playerHP_text.text = "" + playerHP;
        }
    }
    public void RemovePlayerLife(int HP)
    {
        playerHP -= HP;
        Debug.Log("Removi uma vida");
        if (playerHP < 0)
            gameOver_text.gameObject.SetActive(true);
    }
}
