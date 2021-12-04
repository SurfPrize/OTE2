using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static int playerPoint = 0;
    public static TMP_Text scorePoint;
    private static TMP_Text HPText;
    private static int _hp = 10;

    [SerializeField] private TMP_Text scoreref;
    [SerializeField] private TMP_Text hpref;

    public static int Hp
    {
        get => _hp;

        set
        {
            _hp = value;
            HPText.text = "Player HP: " + Hp;
        }
    }

    // Start is called before the first frame update
    private void Awake()
    {
        scorePoint = scoreref;
        HPText = hpref;
    }
    public static void AddPoint(int pontos)
    {
        playerPoint += pontos;
        scorePoint.text = "Player Points: " + playerPoint;
        Debug.Log("Adicionei um ponto");
    }
}
