using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflySpawner : MonoBehaviour
{
    public GameObject[] butterfly;
    public float spawnRate;
    public int maxButterfliesOnScreen;
    public bool spawnLeft;
    private float timeToSpawn;
    public float spawnRadius;
    private int nrButterfliesOnScreen;
    void Start()
    {
        timeToSpawn = spawnRate;   
    }

    void Update()
    {
        if (timeToSpawn > 0)
        {
            timeToSpawn -= Time.deltaTime;
        }
        else
        {
            SpawnButterfly();
        }
    }
    void SpawnButterfly()
    {
        CountButterflies();
        if (maxButterfliesOnScreen > nrButterfliesOnScreen)
        {
            //Debug.Log("spawn " + (maxButterfliesOnScreen - nrButterfliesOnScreen) + "bt's");
            for(int i=0; i < (maxButterfliesOnScreen - nrButterfliesOnScreen); i++)
            {
                GameObject butterflySpawn = Instantiate(butterfly[Random.Range(0, butterfly.Length)],
                    new Vector2(transform.position.x, transform.position.y + Random.Range(-spawnRadius, spawnRadius)),
                    transform.rotation
                    ) ;
                ButterflyBehaviour butterflyBehaviour = butterflySpawn.GetComponent<ButterflyBehaviour>();
                if (spawnLeft)
                {
                    butterflyBehaviour.spawnDir = Vector2.left;
                }
                else
                {
                    butterflyBehaviour.spawnDir = Vector2.right;
                }
            }
        }
        timeToSpawn = spawnRate;
    }
    private void CountButterflies()
    {
        GameObject[] butterfliesOnScreen;
        butterfliesOnScreen = GameObject.FindGameObjectsWithTag("Butterfly");
        nrButterfliesOnScreen = butterfliesOnScreen.Length;
        //Debug.Log("there are " + nrButterfliesOnScreen + " bt's on screen");
    }
}
