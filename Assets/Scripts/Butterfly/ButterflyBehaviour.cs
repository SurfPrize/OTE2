using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum States{
    Glide,
    FlapWings,
    OffCamera
};
public class ButterflyBehaviour : MonoBehaviour
{
    public float fallSpeed;
    public float glideSpeed;
    public float upSpeed;
    public float maxReactionTime;
    public float minReactionTime;
    public float flapWingTime;
    [HideInInspector] public Vector2 spawnDir;
    private float timeForNextReaction;
    private float currentFlapWingTime;
    //private int state;
    private States state;
    private Vector2 direction;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        timeForNextReaction = Random.Range(maxReactionTime,minReactionTime);
        currentFlapWingTime = flapWingTime;
        //direction = Vector2.left;
        direction = spawnDir;
        state = States.Glide;
        animator = gameObject.GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        switch (state)
        {
            case States.Glide:
                //gliding
                animator.SetBool("IsWingFlapping", false);
                transform.Translate((Vector2.down * Time.deltaTime * fallSpeed) + (direction * Time.deltaTime * glideSpeed));
                ChangeState();
                break;
            case States.FlapWings:
                //flap wings
                animator.SetBool("IsWingFlapping", true);
                
                transform.Translate(Vector2.up * Time.deltaTime * upSpeed);
                if (currentFlapWingTime > 0)
                {
                    currentFlapWingTime -= Time.deltaTime;
                }
                else
                {
                    currentFlapWingTime = flapWingTime;
                    ChangeDir();
                    state = States.Glide;
                }
                break;
            default:
                Debug.Log("Wrong number");
                break;
        }
        //OnCamera();
    }
    private void ChangeState()
    {
        int stateId;
        if (timeForNextReaction > 0)
        {
            timeForNextReaction -= Time.deltaTime;
        }
        else {
            OnCameraCheck();
            stateId = Random.Range(1, 3);
            switch (stateId)
            {
                case 1:
                    state = States.Glide;
                    break;
                case 2:
                    state = States.FlapWings;
                    break;
            }
            //Debug.Log("state: " + state);
            timeForNextReaction = Random.Range(maxReactionTime, minReactionTime);
        }
    }
    private void ChangeDir()
    {
        if (Random.value < 0.5f)
        {
            
            direction = Vector2.right;
            if (spriteRenderer.flipX)
                spriteRenderer.flipX = false;
        }
        else
        {
           
            direction = Vector2.left;
            if (!spriteRenderer.flipX)
                spriteRenderer.flipX = true;
        }
    }
    void OnCameraCheck()
    {
        if (!GetComponent<Renderer>().isVisible)
        {
            Destroy(gameObject);
        }
    }
}
