using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_movement : MonoBehaviour
{
    Rigidbody Ball;
    Rigidbody P_racket;
    Rigidbody AI_racket_rb;

    public GameObject ai_racket;
    public GameObject p_racket;
    public GameObject prompt;
    

    public Vector3 ToStart = Vector3.zero;
    public Vector3 Target = Vector3.zero;

    public AI_racket AI;
    public PlayerPrompt Prompt;

    public AudioSource Sound;
    public AudioClip SoundClip;
    // Start is called before the first frame update
    void Start()
    {
        Ball = GetComponent<Rigidbody>();
        P_racket = p_racket.GetComponent<Rigidbody>();
        AI_racket_rb = ai_racket.GetComponent<Rigidbody>();
        AI = ai_racket.GetComponent<AI_racket>();
        Prompt = prompt.GetComponent<PlayerPrompt>();
        


    }

    // Update is called once per frame
    void Update()
    {
        ToStart = P_racket.position - Ball.position;

        if(transform.position.x >= 30 && transform.position.x <=35)
        {
            AI.MoveRacket(CalculatePointOnX());
        }

        if (transform.position.x < P_racket.transform.position.x - 5)
        {
            Prompt.OutComeScreen("Player");
        }
        else if(transform.position.x > AI_racket_rb.position.x + 5)
        {
            Prompt.OutComeScreen("Opponent");
        }




    }

    public void KickOff()
    {
        ToStart = P_racket.position - transform.position;
        Ball.AddForce(ToStart.normalized*60f, ForceMode.VelocityChange);
    }

    public Vector3 CalculatePointOnX()
    {

        Vector3 reaction_plane = Vector3.zero;
        reaction_plane.x = 40;

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
        Sound.clip = SoundClip;
        Sound.Play();
        if (collision.transform.tag == "racket_P")
        {

            Target = new Vector3(0, 4, 0) - p_racket.transform.position;
            if (Ball.position.y < 4)
            {
                Target.y = 2f;
            }
            Ball.velocity = Target*0.5f;
        }
        else if(collision.transform.tag == "racket_AI")
        {
            Prompt.ShouldKick = false;

            Target = new Vector3(0, 4, 0) - AI_racket_rb.transform.position;
            if(Ball.position.y < 4)
            {
                Target.y = 2f;
            }
            Ball.velocity = Target*0.5f;
        }

        
    }

}
