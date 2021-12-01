using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreSystem : MonoBehaviour
{
    public int playerPoint = 0; 
    public TMP_Text scorePoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Orange Butterfly(Clone)")
        {
            Debug.Log("Detectei a borboleta");
            AddPoint(1);
            scorePoint.text = "" + playerPoint;
            DeleteButterfly();
        }
    }
    public void AddPoint(int pontos)
    {
        playerPoint += pontos;
        Debug.Log("Adicionei um ponto");
    }

    public void DeleteButterfly()
    {
        //Destroy(gameObject);
    }
}
