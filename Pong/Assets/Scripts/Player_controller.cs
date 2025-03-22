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

    int Speed = 10;
    int MouseSpeed = 4;

    Vector3 GetMouseP = Vector3.zero;
    Vector3 MoveRacket = Vector3.zero;
    Vector3 TempVector = Vector3.zero;
    Vector3 Look = Vector3.zero;

    float Scroll_Legnth;
    float LowerLimit;

    public Racket_Script Notifier;
    
    

    void Start()
    {
        player = GetComponent<Rigidbody>();
        Racket = racket.GetComponent<Rigidbody>();
        Notifier = racket.GetComponent<Racket_Script>();
        Ball = ball.GetComponent<Rigidbody>();


        TempVector.x = 90;
        Scroll_Legnth = 7;
        LowerLimit = -5;
    }

    // Update is called once per frame
    void Update()
    {

        //Attempt 1 at making the racket move using the mouse
        /*
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit hit) )
        {
            TempVector = hit.point;
        }
        Debug.Log(TempVector);
        GetMouseP = new Vector3(Camera.main.transform.position.x, TempVector.y, TempVector.z);
        
        MoveRacket = Vector3.Lerp(Racket.position, GetMouseP, 0.5f);
        */
        //Did not work



        //Attempt 2 at making the racket move using the mouse
        //Reusing code I wrote for a previous project (:
        //For Up and down movement
        if (TempVector.y < 70 && TempVector.y > LowerLimit)
        {
            TempVector.y += Input.GetAxis("Mouse Y") * MouseSpeed;
        }
        else if(TempVector.y > 70)
        {
            TempVector.y -= 0.1f;
        }
        else
        {
            TempVector.y += 0.1f;
        }
        //For Left and Right movement
        if (TempVector.x < 120 && TempVector.x > 60)
        {
            TempVector.x += Input.GetAxis("Mouse X") * MouseSpeed;
        }
        else if (TempVector.x > 120)
        {
            TempVector.x -= 0.1f;
        }
        else
        {
            TempVector.x += 0.1f;
        }

        //Hide cursor with H key on the keyboard
        if (Input.GetKeyDown(KeyCode.H))
        {

            Cursor.lockState = CursorLockMode.Locked;
        }



        if (Scroll_Legnth < 7)
        {
            Scroll_Legnth = 7;

        }
        else if(Scroll_Legnth > 16)
        {
            Scroll_Legnth = 16;
        }



        Scroll_Legnth += Input.GetAxis("Mouse ScrollWheel") * Speed;
        GetMouseP = Camera.main.transform.position - Racket.position;

        if (Scroll_Legnth != 0 && Scroll_Legnth > 7 && Scroll_Legnth < 16)
        {
            GetMouseP = GetMouseP.normalized * Scroll_Legnth;

            MoveRacket = Camera.main.transform.position - GetMouseP;
            Racket.MovePosition(MoveRacket);
        }

        //Look = Ball.position - Racket.position;
        //Quaternion target = Quaternion.LookRotation(Look);
        //Racket.rotation = target;







    }

    private void FixedUpdate()
    {
        //Part of attempt 1
        //player.velocity = new Vector3(Input.GetAxisRaw("Vertical") * Speed, 0, Input.GetAxisRaw("Horizontal") * Speed * -1);
        //Racket.MovePosition(MoveRacket);

        //Part of attempt 2

        Camera.main.transform.localRotation = Quaternion.Euler(-TempVector.y, TempVector.x, 0);




    }

}
