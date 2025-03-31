using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Racket_Script : MonoBehaviour
{
    

    public Transform Ball;

    public GameObject Ball_Script;

    Ball_movement ball;

    // Start is called before the first frame update
    void Start()
    {
        ball = Ball_Script.GetComponent<Ball_movement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(ball.ToStart.magnitude > 10)
        {
            transform.LookAt(Ball);
            transform.rotation *= Quaternion.Euler(90, 0, 0);
            
        }
        

    }
    private void OnCollisionEnter(Collision collision)
    {
        
    }


}
