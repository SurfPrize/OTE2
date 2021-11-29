using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    // Player points
    PlayerPoints pp;
    public int pontos = 4;
    private GameObject score;

    // Start is called before the first frame update
    void Start()
    {
        score = GameObject.FindGameObjectWithTag("Score");
        pp = score.GetComponent<PlayerPoints>();

    }

    // Update is called once per frame
    void Update()
    {
    
    }
    // Method que adiciona pontos
    void AddPoints(int pontos)
    {
        pp.UIAddPoint(pontos);
    }
    void PercentagePoints(float percentage)
    {
        pp.playerPoints *= (int)(1f - percentage);
    }
    // Method que pega o tipo de borboleta e retorna o valor delas
    void ButterflyType()
    {

        //return tipo;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Butterfly")
        {
            AddPoints(pontos);
            return;
        }
        print("BUTTERFLY ELIMINATED");
        Destroy(gameObject);
    }
}
