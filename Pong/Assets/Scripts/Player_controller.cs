using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player_controller : MonoBehaviour
{
    Rigidbody player;
    Rigidbody Racket;
    Rigidbody Ball;

    public GameObject racket;
    public GameObject ball;
    public GameObject prompt;

    float Speed = 0.1f;

    Vector3 GetMouseP = Vector3.zero;
    Vector3 MoveRacket = Vector3.zero;

    Vector3 Look = Vector3.zero;
    Vector3 Movement = Vector3.zero;

    float Scroll_Legnth;

    public Racket_Script Notifier;
    public PlayerPrompt Prompt;




    void Start()
    {
        player = GetComponent<Rigidbody>();
        Racket = racket.GetComponent<Rigidbody>();
        Notifier = racket.GetComponent<Racket_Script>();
        Ball = ball.GetComponent<Rigidbody>();
        Prompt = prompt.GetComponent<PlayerPrompt>();


        //TempVector.x = 90;

        Movement = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Camera.main.transform.localRotation = Quaternion.Euler(-TempVector.y, TempVector.x, 0);



        /*
        if (Input.GetAxis("Horizontal") != 0 && Movement.z < 15 && Movement.z > -15)
        {
            Movement.z += Input.GetAxis("Horizontal") * -Speed;
        }
        else if (Movement.z >= 15)
        {
            Movement.z -= 0.2f;
        }
        else if (Movement.z < -15)
        {
            Movement.z += 0.2f;
        }

        if (Input.GetAxis("Vertical") != 0 && Movement.x > -65 && Movement.x < 5)
        {
            Movement.x += Input.GetAxis("Vertical") * Speed;

        }
        else if (Movement.x >= 5)
        {
            Movement.x -= 0.2f;
        }
        else if (Movement.x < -65)
        {
            Movement.x += 0.2f;
        }

        transform.position = Movement;
        */

        //Hide cursor with H key on the keyboard
        if (Input.GetKeyDown(KeyCode.H))
        {

            Cursor.lockState = CursorLockMode.Locked;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }


        /*
        if (Scroll_Legnth < 7)
        {
            Scroll_Legnth = 7;

        }
        else if(Scroll_Legnth > 16)
        {
            Scroll_Legnth = 16;
        }

        Scroll_Legnth += Input.GetAxis("Mouse ScrollWheel") * 20;
        GetMouseP = Camera.main.transform.position - Racket.position;

        if (Scroll_Legnth != 0 && Scroll_Legnth > 7 && Scroll_Legnth < 16)
        {
            GetMouseP = GetMouseP.normalized * Scroll_Legnth;

            MoveRacket = Camera.main.transform.position - GetMouseP;
            Racket.MovePosition(MoveRacket);
        }
        */


    }

}
