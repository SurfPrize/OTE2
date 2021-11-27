using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyBehaviour : MonoBehaviour
{
    public float fallSpeed;
    public float glideSpeed;
    public float upSpeed;
    public float reactionTime;
    public float flapWingTime;
    [HideInInspector] public Vector2 spawnDir;
    private float timeForNextReaction;
    private float currentFlapWingTime;
    private int state;
    private Vector2 direction;
    //DIRECTION
    // 
    void Start()
    {
        timeForNextReaction = reactionTime;
        currentFlapWingTime = flapWingTime;
        direction = Vector2.left;
        state = 1;
    }
    void Update()
    {
        switch (state)
        {
            case 1:
                //gliding
                transform.Translate((Vector2.down * Time.deltaTime * fallSpeed) + (direction * Time.deltaTime * glideSpeed));
                ChangeState();
                break;
            case 2:
                //flap wings
                ChangeDir();
                transform.Translate(Vector2.up * Time.deltaTime * upSpeed);
                if (currentFlapWingTime > 0)
                {
                    currentFlapWingTime -= Time.deltaTime;
                }
                else
                {
                    currentFlapWingTime = flapWingTime;
                    state = 1;
                }
                break;
            default:
                Debug.Log("Wrong number");
                break;
        }
    }
    private void ChangeState()
    {
        if (timeForNextReaction > 0)
        {
            timeForNextReaction -= Time.deltaTime;
        }
        else {
            state = Random.Range(1, 3);
            //Debug.Log("state: " + state);
            timeForNextReaction = reactionTime;
        }
    }
    private void ChangeDir()
    {
        if (Random.value < 0.5f)
        {
            direction = Vector2.right;
        }
        else
        {
            direction = Vector2.left;
        }
    }
}
