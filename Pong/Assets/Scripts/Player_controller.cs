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
    int MouseSpeed = 4;

    Vector3 GetMouseP = Vector3.zero;
    Vector3 MoveRacket = Vector3.zero;
    Vector3 TempVector = Vector3.zero;
    Vector3 Look = Vector3.zero;
    Vector3 Movement = Vector3.zero;

    float Scroll_Legnth;
    float LowerLimit;

    public Racket_Script Notifier;
    public PlayerPrompt Prompt;

    
    

    void Start()
    {
        player = GetComponent<Rigidbody>();
        Racket = racket.GetComponent<Rigidbody>();
        Notifier = racket.GetComponent<Racket_Script>();
        Ball = ball.GetComponent<Rigidbody>();
        Prompt = prompt.GetComponent<PlayerPrompt>();


        TempVector.x = 90;
        Scroll_Legnth = 7;
        LowerLimit = -5;
        Movement = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Camera.main.transform.localRotation = Quaternion.Euler(-TempVector.y, TempVector.x, 0);

        if (TempVector.y < 70 && TempVector.y > LowerLimit && Prompt.countdown >= 1)
        {
            TempVector.y += Input.GetAxis("Mouse Y") * MouseSpeed;
        }
        else if(TempVector.y > 70 && Prompt.countdown >= 1)
        {
            TempVector.y -= 0.1f;
        }
        else if(Prompt.countdown >= 1)
        {
            TempVector.y += 0.1f;
        }
        //For Left and Right movement
        if (TempVector.x < 120 && TempVector.x > 60 && Prompt.countdown >= 1)
        {
            TempVector.x += Input.GetAxis("Mouse X") * MouseSpeed;
        }
        else if (TempVector.x > 120 && Prompt.countdown >= 1)
        {
            TempVector.x -= 0.1f;
        }
        else if(Prompt.countdown >= 1)
        {
            TempVector.x += 0.1f;
        }

        if(Input.GetAxis("Horizontal") != 0 && Movement.z < 15 && Movement.z > -15)
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
        else if(Movement.x >= 5)
        {
            Movement.x -= 0.2f;
        }
        else if(Movement.x < -65)
        {
            Movement.x += 0.2f;
        }

        transform.position = Movement;

        //Hide cursor with H key on the keyboard
        if (Input.GetKeyDown(KeyCode.H))
        {

            Cursor.lockState = CursorLockMode.Locked;
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }



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


    }

}
