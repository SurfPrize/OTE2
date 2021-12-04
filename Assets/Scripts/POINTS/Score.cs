using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static int playerPoint = 0;
    public static TMP_Text scorePoint;
    [SerializeField] private TMP_Text scoreref;
    // Start is called before the first frame update
    private void Awake()
    {
        scorePoint = scoreref;
    }
    public static void AddPoint(int pontos)
    {
        playerPoint += pontos;
        scorePoint.text = "Player Points: " + playerPoint;
        Debug.Log("Adicionei um ponto");
    }
}
