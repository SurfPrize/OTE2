using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPoints : MonoBehaviour
{
    private int _playerPoints;
    TextMeshPro textScore;
    public int playerPoints
    {
        get { return _playerPoints; }

        set
        {
            _playerPoints = value;


            textScore.text = _playerPoints.ToString();

        }
    }
    // Start is called before the first frame update
    void Awake()
    {
        textScore = gameObject.GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void UIAddPoint(int pontos)
    {
        playerPoints += pontos;
    }
    void UpdateUI()
    {

    }
}
