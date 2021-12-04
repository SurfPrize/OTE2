using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreSystem : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.name == "Orange Butterfly(Clone)")
        //{
        //    Debug.Log("Detectei a borboleta");
        //    AddPoint(1);
        //    scorePoint.text = "" + playerPoint;
        //    DeleteButterfly();
        //}
        ButterflyBehaviour este = collision.gameObject.GetComponent<ButterflyBehaviour>();
        if (este != null)
        {
            switch (este.Tipo)
            {
                case ButterflyType.ORANGE:
                    Debug.Log("Detectei a borboleta");
                    Score.AddPoint(1);
                    DeleteButterfly(este.gameObject);
                    break;
                case ButterflyType.RED:
                    break;
                case ButterflyType.BLUE:
                    break;
            }
        }

    }


    public void DeleteButterfly(GameObject borboleta)
    {
        Destroy(borboleta);
    }
}
