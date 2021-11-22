using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int laranja, azul, castanho, roxo, current, maximum;

        current = 0;
        maximum = 255;
        laranja = 1;
        azul = laranja * 10;
        castanho = laranja--;
        roxo = (current / maximum) * 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (true)
        {

        }
    }
}
