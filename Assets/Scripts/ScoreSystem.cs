using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    // Player points
    private int player;
    // Script para definir o valor das borboletas
    public int pontos, tipo;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    // Method que adiciona pontos
    void AddPoints()
    {
        player += pontos;
    }
    // Method que pega o tipo de borboleta e retorna o valor delas
    void ButterflyType()
    {

        //return tipo;
    }
}
