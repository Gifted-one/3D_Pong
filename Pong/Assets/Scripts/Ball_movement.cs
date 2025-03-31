using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_movement : MonoBehaviour
{
    Rigidbody Ball;
    Rigidbody P_racket;
    Rigidbody AI_racket;

    public GameObject ai_racket;
    public GameObject p_racket;

    public Vector3 ToStart = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        Ball = GetComponent<Rigidbody>();
        P_racket = p_racket.GetComponent<Rigidbody>();
        AI_racket = ai_racket.GetComponent<Rigidbody>();

        
    }

    // Update is called once per frame
    void Update()
    {
        ToStart = P_racket.position - Ball.position;
    }

    public void KickOff()
    {
        ToStart = P_racket.position - transform.position;
        Ball.AddForce(ToStart*250f);
    }

    public Vector3 CalculatePointOnX()
    {

        Vector3 reaction_plane = Vector3.zero;
        reaction_plane.x = 26;

        reaction_plane.y = transform.position.y + Ball.velocity.y / Ball.velocity.x * (reaction_plane.x - transform.position.x);
        reaction_plane.z = transform.position.z + Ball.velocity.z / Ball.velocity.x * (reaction_plane.x - transform.position.x);

        return reaction_plane;
    }

    public Vector3 CalculatePointOnY()
    {

        Vector3 reaction_plane = Vector3.zero;
        reaction_plane.y = -2;

        reaction_plane.x = transform.position.x + Ball.velocity.x / Ball.velocity.y * (reaction_plane.y - transform.position.y);
        reaction_plane.z = transform.position.z + Ball.velocity.z / Ball.velocity.y * (reaction_plane.y - transform.position.y);

        return reaction_plane;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "racket")
        {
            
        }
    }

    void Shoot()
    {
        //Ball.AddForce();
    }
}
