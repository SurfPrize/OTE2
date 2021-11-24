using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    // Player points
    PlayerPoints pp;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Um if, se o jogador pegar uma borboleta que dá pontos, addpoints inicia
        //Provavelmente quando pegas a borboleta, usas o botão esquerdo do rato
        /*
        if (Input.GetMouseButtonDown(1)) 
        {
            AddPoints();
        }
        */
    }
    // Method que adiciona pontos
    void AddPoints(int pontos)
    {
        pp.playerPoints += pontos;
    }
    void PercentagePoints(float percentage)
    {
        pp.playerPoints *=(int)(1f - percentage);
    }
    // Method que pega o tipo de borboleta e retorna o valor delas
    void ButterflyType()
    {

        //return tipo;
    }
}
