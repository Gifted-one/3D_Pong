using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player_controller : MonoBehaviour
{
    Rigidbody player;
    Rigidbody Racket;
    public GameObject racket;
    int Speed = 10;
    Vector3 GetMouseP = Vector3.zero;
    Vector3 MoveRacket = Vector3.zero;
    Vector3 TempVector = Vector3.zero;

    void Start()
    {
        player = GetComponent<Rigidbody>();
        Racket = racket.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit hit) )
        {
            TempVector = hit.point;
        }
        Debug.Log(TempVector);
        GetMouseP = new Vector3(Camera.main.transform.position.x, TempVector.y, TempVector.z);
        
        MoveRacket = Vector3.Lerp(Racket.position, GetMouseP, 0.5f);


    }

    private void FixedUpdate()
    {
        player.velocity = new Vector3(Input.GetAxisRaw("Vertical") * Speed, 0, Input.GetAxisRaw("Horizontal") * Speed * -1);
        Racket.MovePosition(MoveRacket);
    }
}
