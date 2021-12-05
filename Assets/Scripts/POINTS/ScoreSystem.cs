using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreSystem : MonoBehaviour
{
    public int NormalScore = 1;
    public int BlueMultiplier = 10;
    public AudioClip deathSound;
    public GameObject audioSourceOrigin;
    private AudioSource audioSource;
    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }
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
                    // Debug.Log("Detectei a borboleta");
                    Score.AddPoint(NormalScore);
                    DeleteButterfly(este.gameObject);
                    break;
                case ButterflyType.RED:
                    Score.Hp -= 1;
                    DeleteButterfly(este.gameObject);
                    break;
                case ButterflyType.BLUE:
                    Score.AddPoint(NormalScore * BlueMultiplier);
                    DeleteButterfly(este.gameObject);
                    break;
            }
        }

    }
    public void DeleteButterfly(GameObject borboleta)
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = deathSound;
            audioSource.Play();
        }
        Destroy(borboleta);
    }
}
