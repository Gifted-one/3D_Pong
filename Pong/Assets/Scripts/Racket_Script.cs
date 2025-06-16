using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Racket_Script : MonoBehaviour
{


    public Transform Ball;

    public GameObject Ball_Script;
    public GameObject prompt;

    Ball_movement ball;
    PlayerPrompt Prompt;

    Vector3 TempVector = Vector3.zero;

    float MouseSpeed = 0.3f;

    float LowerLimit;




    // Start is called before the first frame update
    void Start()
    {
        ball = Ball_Script.GetComponent<Ball_movement>();
        ball.shouldCalculate = true;
        Prompt = prompt.GetComponent<PlayerPrompt>();

        TempVector = transform.position;
        LowerLimit = -10;
    }

    // Update is called once per frame
    void Update()
    {
        if (ball.ToStart.magnitude > 10)
        {
            transform.LookAt(Ball);
            transform.rotation *= Quaternion.Euler(90, 0, 0);

        }

        if (ball.transform.position.x < -5 && ball.shouldCalculate)
        {
            int Y_pos = (int)transform.position.y;
            Vector3 point = ball.CalculatePointOnY(Y_pos);

            if (point == Vector3.positiveInfinity || point.x > -90)
            {
                int maxTries = 100; // prevent infinite loop
                int tries = 0;

                while ((point.x > -90 || point == Vector3.positiveInfinity) && tries < maxTries)
                {
                    Y_pos--;
                    point = ball.CalculatePointOnY(Y_pos);

                    tries++;
                }
            }
            ball.TheRacket = ball.CalculatePointOnY(Y_pos);
            //Debug.Log(ball.CalculatePointOnY(Y_pos));
            ball.shouldCalculate = false;
        }

        if (TempVector.z < 10 && TempVector.z > LowerLimit && Prompt.countdown >= 1)
        {
            TempVector.z -= Input.GetAxis("Mouse X") * MouseSpeed;
        }
        else if (TempVector.z > 10 && Prompt.countdown >= 1)
        {
            TempVector.z -= 0.5f;
        }
        else if (Prompt.countdown >= 1)
        {
            TempVector.z += 0.5f;
        }
        //For Left and Right movement
        if (TempVector.x < -10 && TempVector.x > -45 && Prompt.countdown >= 1)
        {
            TempVector.x += Input.GetAxis("Mouse Y") * MouseSpeed;
        }
        else if (TempVector.x < -45 && Prompt.countdown >= 1)
        {
            TempVector.x += 0.5f;
        }
        else if (Prompt.countdown >= 1)
        {
            TempVector.x -= 0.5f;
        }

        transform.position = TempVector;


    }
    private void OnCollisionEnter(Collision collision)
    {

    }


}
