using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class AI_racket : MonoBehaviour
{
    Rigidbody Racket;

    public GameObject ball;

    Ball_movement Ball;

    public Vector3 position = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        Racket = GetComponent<Rigidbody>();
        Ball = ball.GetComponent<Ball_movement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(position != Vector3.zero)
        {
            MoveRacket(position);

        }
    }

    public void MoveRacket(Vector3 pos)
    {
        //Racket.position = Vector3.Lerp(transform.position, pos, 0.5f);

        Vector3 ToStart = Vector3.MoveTowards(transform.position, pos, 0.9f);
        Racket.MovePosition(ToStart);
        position = pos;

    }

}
