using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyBehaviour : MonoBehaviour
{
    public float fallSpeed;
    public float glideSpeed;
    //REACTION TIME
    //DIRECTION
    // 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //gliding
        transform.Translate((Vector2.down * Time.deltaTime * fallSpeed) + (Vector2.left * Time.deltaTime * glideSpeed));
    }
}
