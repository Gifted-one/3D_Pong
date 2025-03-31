using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_racket : MonoBehaviour
{
    Rigidbody Racket;

    public GameObject ball;

    Ball_movement Ball;
    // Start is called before the first frame update
    void Start()
    {
        Racket = GetComponent<Rigidbody>();
        Ball = ball.GetComponent<Ball_movement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
