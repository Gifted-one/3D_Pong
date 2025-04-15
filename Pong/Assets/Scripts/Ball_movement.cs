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

    //Sphere cast variables
    public float sphereRadius = 0.5f;
    public float castDistance = 1f;

    bool ShouldMove = true;


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

        if(transform.position.x > 0)
        {
            Time.timeScale = 0.1f;

            Vector3 pos = CalculatePointOnY() - transform.position;
            Debug.DrawLine(transform.position, CalculatePointOnY(), Color.red);
            Debug.DrawRay(CalculatePointOnY(), Vector3.up * 5f, Color.green);

            
            if (transform.position.x > 5 && CalculatePointOnY() != Vector3.positiveInfinity && ShouldMove)
            {
                AI.MoveRacket(CalculatePointOnY());
                ShouldMove = false;
            }
        }

        if (transform.position.x < P_racket.transform.position.x - 5)
        {
            Prompt.OutComeScreen("Player");
        }
        else if(transform.position.x > AI_racket_rb.transform.position.x + 10)
        {
            Prompt.OutComeScreen("Opponent");
;
        }

    }

    private void FixedUpdate()
    {
        Vector3 direction = Ball.velocity.normalized;
        if(Physics.SphereCast(transform.position, sphereRadius, direction, out RaycastHit hit, castDistance))
        {
            if(hit.transform.tag == "racket_P")
            {
                //Debug.Log("SphereCast: " + hit.collider.name);

                Prompt.ShouldFollow = false;
                Target = new Vector3(0, 4, 0) - p_racket.transform.position;
                Target.y = 5;
                Ball.velocity = Target.normalized * 70f;
                ShouldMove = true;
            }
            else if (hit.transform.tag == "racket_AI")
            {
                Target = new Vector3(0, 4, 0) - AI_racket_rb.transform.position;
                Target.y = 7;
                Ball.velocity = Target.normalized * 70f;
                
            }
            
        }
    }

    void OnDrawGizmos()
    {
        Rigidbody tempRb = GetComponent<Rigidbody>();
        if (tempRb != null)
        {
            Gizmos.color = Color.red;
            Vector3 direction = tempRb.velocity.normalized;
            Vector3 endPoint = transform.position + direction * castDistance;

            Gizmos.DrawWireSphere(endPoint, sphereRadius);
            Gizmos.DrawLine(transform.position, endPoint);
        }

    }

    public void KickOff()
    {
        ToStart = P_racket.position - transform.position;
        Ball.velocity = ToStart.normalized;
        //Ball.AddForce(ToStart.normalized * 70f, ForceMode.VelocityChange);
        ToStart = Vector3.Lerp(transform.position, P_racket.position, 0.1f);
        Ball.MovePosition(ToStart);
        
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
        float t = 0;
        float g = Physics.gravity.y;
        float Yin = transform.position.y;
        float Vin = Ball.velocity.y;

        Vector3 reaction_plane = Vector3.zero;
        reaction_plane.y = 3;

        float a = 0.5f * g;
        float b = Vin;
        float c = Yin - reaction_plane.y;

        float discriminant =b * b - 4 * a * c;

        if (discriminant < 0)
        {
            return Vector3.positiveInfinity;
        }

        float sqrtDisc = Mathf.Sqrt(discriminant);
        float t1 = (-b + sqrtDisc) / (2 * a);
        float t2 = (-b - sqrtDisc) / (2 * a);

        t = Mathf.Min(t1, t2);
        if (t < 0) t = Mathf.Max(t1, t2);
        if (t < 0) return Vector3.positiveInfinity;

        reaction_plane.x = Ball.velocity.x/t + transform.position.x;
        reaction_plane.z = Ball.velocity.z/t + transform.position.z;

        /*
        reaction_plane.x = transform.position.x + Ball.velocity.x / Ball.velocity.y * (reaction_plane.y - transform.position.y);
        reaction_plane.z = transform.position.z + Ball.velocity.z / Ball.velocity.y * (reaction_plane.y - transform.position.y);

        */
        return reaction_plane;
    }

    private void OnCollisionEnter(Collision collision)
    {
    
        Sound.clip = SoundClip;
        Sound.Play();

    }

}
