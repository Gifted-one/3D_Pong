using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AI_racket : MonoBehaviour
{
    Rigidbody Racket;

    public GameObject ball;

    Ball_movement Ball;

    public Vector3 To_position = Vector3.zero;

    public float reactionSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        Racket = GetComponent<Rigidbody>();
        Ball = ball.GetComponent<Ball_movement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (To_position != Vector3.zero)
        {
            MoveRacket(To_position);

        }
    }

    public void MoveRacket(Vector3 pos)
    {
        //Racket.position = Vector3.Lerp(transform.position, pos, 0.5f);

        Vector3 ToStart = Vector3.Lerp(transform.position, pos, 0.05f);
        Racket.MovePosition(ToStart);
        To_position = pos;


    }

}
