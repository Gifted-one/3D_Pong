using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class Ball_movement : MonoBehaviour
{
    public Rigidbody Ball;
    Rigidbody P_racket;
    Rigidbody AI_racket_rb;

    public GameObject ai_racket;
    public GameObject p_racket;
    public GameObject prompt;

    public static int Point_int;
    public int Y_pos;
    public int ballSpeed = 70;

    public Vector3 ToStart = Vector3.zero;
    public Vector3 Target = Vector3.zero;
    public Vector3 middle = new Vector3(0, 0, 0);
    public Vector3 TheRacket;

    public AI_racket AI;
    public PlayerPrompt Prompt;

    public AudioSource Sound;
    public AudioClip SoundClip;

    //Sphere cast variables
    public float sphereRadius = 0.5f;
    public float castDistance = 1.5f;
    public float Level_Speed = 1.0f;
    public float value = 0f;
    float targetValue = 50f;
    float velocity = 0f;
    float smoothTime = 1f;

    bool ShouldMove = true;
    bool ShouldFill = true;
    public bool shouldCalculate;

    //Line renderer
    private LineRenderer line;

    public TextMeshProUGUI Points;


    // Start is called before the first frame update
    void Start()
    {
        Ball = GetComponent<Rigidbody>();
        P_racket = p_racket.GetComponent<Rigidbody>();
        AI_racket_rb = ai_racket.GetComponent<Rigidbody>();
        AI = ai_racket.GetComponent<AI_racket>();
        Prompt = prompt.GetComponent<PlayerPrompt>();

        //Line renderer
        line = gameObject.AddComponent<LineRenderer>();
        line.startWidth = 0.02f;
        line.endWidth = 0.02f;
        line.material = new Material(Shader.Find("Sprites/Default"));
        line.positionCount = 2;
        line.startColor = Color.red;
        line.endColor = Color.red;

        if (Point_int > 0)
        {
            Prompt.TimeToStart = (int)PlayerPrompt.Timer + 5;
            Prompt.Holder = (int)PlayerPrompt.Timer + 5;
            Prompt.starting = false;

        }
        else
        {
            Prompt.TimeToStart = 30; //Make 30 for on screen text
            Prompt.starting = true;
            PlayerPrompt.dir = "player";
        }

        Prompt.progressBar.value = value;



    }

    // Update is called once per frame
    void Update()
    {
        ToStart = P_racket.position - Ball.position;
        Y_pos = 4;
        if (Ball.linearVelocity.x > 0)
        {
            line.SetPosition(0, transform.position);
            line.SetPosition(1, CalculatePointOnY(Y_pos));
        }
        else if (Ball.linearVelocity.x < 0)
        {
            line.SetPosition(0, transform.position);
            line.SetPosition(1, CalculatePointOnY((int)TheRacket.y));
        }

        Prompt.progressBar.value = value;
        if (Input.GetMouseButton(0) && ShouldFill)
        {
            value = Mathf.SmoothDamp(value, targetValue, ref velocity, smoothTime);
        }


        if (transform.position.x > 4 && ShouldMove)
        {
            Vector3 point = CalculatePointOnY(Y_pos);

            if (point != Vector3.positiveInfinity && point.x > 90)
            {
                int maxTries = 100; // prevent infinite loop
                int tries = 0;

                while (point.x > 90 && tries < maxTries)
                {
                    Y_pos++;
                    point = CalculatePointOnY(Y_pos);

                    if (point == Vector3.positiveInfinity)
                        break;

                    tries++;
                }
            }


            //Vector3 pos = CalculatePointOnY(Y_pos) - transform.position;
            //line.SetPosition(0, transform.position);
            //line.SetPosition(1, CalculatePointOnY(Y_pos));

            if (transform.position.x > 5 && ShouldMove)
            {
                Vector3 predictedPosition = CalculatePointOnY(Y_pos);

                if (predictedPosition != Vector3.positiveInfinity && IsValidVector3(predictedPosition))
                {
                    AI.MoveRacket(predictedPosition);
                    ShouldMove = false;
                }
            }


        }


        if (transform.position.x < P_racket.transform.position.x - 5)
        {
            //Ball.velocity = Vector3.zero;
            //Ball.transform.position = middle;
            Prompt.scoreKeeper("Player");

        }
        else if (transform.position.x > AI_racket_rb.transform.position.x + 5)
        {
            Prompt.scoreKeeper("Opponent");
            //Ball.MovePosition(middle);
        }

        Vector3 direction = Ball.linearVelocity.normalized;
        if (Physics.SphereCast(transform.position, sphereRadius, direction, out RaycastHit hit, castDistance))
        {
            if (hit.transform.tag == "racket_P")
            {
                Point_int++;
                Prompt.ShouldFollow = false;
                Prompt.ShouldFollow2 = false;
                Target = AI_racket_rb.transform.position;
                Target.y = Random.Range(3f, 6f);
                Target.z = Random.Range(-7f, 7f);
                Ball.linearVelocity = CalculateLaunchVelocity(p_racket.transform.position, Target, 40f + value, Physics.gravity.y);
                ShouldMove = true;
                Sound.clip = SoundClip;
                Sound.Play();
                value = 0;
                ShouldFill = false;
            }
            else if (hit.transform.tag == "racket_AI")
            {
                Prompt.ShouldFollow = false;
                Prompt.ShouldFollow2 = false;
                Target = p_racket.transform.position;
                Target.y = 5;
                Target.z = Random.Range(-5f, 5f);
                Target.x = Random.Range(-46f, -35f);
                Ball.linearVelocity = CalculateLaunchVelocity(AI_racket_rb.transform.position, Target, 50f, Physics.gravity.y);
                Sound.clip = SoundClip;
                Sound.Play();
                shouldCalculate = true;
                ShouldFill = true;

            }
        }

        Points.text = Point_int.ToString();

    }

    private void FixedUpdate()
    {

    }



    public void KickOff(string dir)
    {

        if (dir == "player")
        {
            ToStart = P_racket.transform.position - transform.position;
            Ball.linearVelocity = ToStart.normalized;
            ToStart = Vector3.Lerp(transform.position, P_racket.position, 0.1f);
            Ball.MovePosition(ToStart);
        }
        else
        {
            ShouldMove = false;
            ToStart = AI_racket_rb.transform.position - transform.position;
            Ball.linearVelocity = ToStart.normalized;
            ToStart = Vector3.Lerp(transform.position, AI_racket_rb.transform.position, 0.1f);
            Ball.MovePosition(ToStart);
        }


    }


    public Vector3 CalculatePointOnX()
    {

        Vector3 reaction_plane = Vector3.zero;
        reaction_plane.x = 40;

        reaction_plane.y = transform.position.y + Ball.linearVelocity.y / Ball.linearVelocity.x * (reaction_plane.x - transform.position.x);
        reaction_plane.z = transform.position.z + Ball.linearVelocity.z / Ball.linearVelocity.x * (reaction_plane.x - transform.position.x);

        return reaction_plane;
    }

    public Vector3 CalculatePointOnY(int Y_pos)
    {
        float t = 0;
        float g = Physics.gravity.y;
        float Yin = transform.position.y;
        float Vin = Ball.linearVelocity.y;

        Vector3 reaction_plane = Vector3.zero;
        reaction_plane.y = Y_pos;

        float a = 0.5f * g;
        float b = Vin;
        float c = Yin - reaction_plane.y;

        float discriminant = b * b - 4 * a * c;

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

        Vector3 gravityVec = new Vector3(0, g, 0);
        Vector3 predictedPosition = transform.position + Ball.linearVelocity * t + 0.5f * gravityVec * t * t;

        return predictedPosition;
    }

    public static Vector3 CalculateLaunchVelocity(Vector3 startPos, Vector3 targetPos, float horizontalSpeed, float gravity)
    {
        // Get horizontal displacement (XZ plane)
        Vector3 horizontalDisplacement = new Vector3(targetPos.x - startPos.x, 0f, targetPos.z - startPos.z);
        float horizontalDistance = horizontalDisplacement.magnitude;

        // Calculate time to reach target horizontally
        float time = horizontalDistance / horizontalSpeed;

        // Calculate required vertical velocity using kinematic formula
        float verticalDisplacement = targetPos.y - startPos.y;
        float verticalVelocity = (verticalDisplacement - 0.5f * gravity * time * time) / time;

        // Compose full launch velocity vector
        Vector3 launchVelocity = horizontalDisplacement.normalized * horizontalSpeed + Vector3.up * verticalVelocity;

        return launchVelocity;
    }

    private void OnCollisionEnter(Collision collision)
    {

        Sound.clip = SoundClip;
        Sound.Play();

    }

    public void reflect()
    {
        if (transform.position.x < p_racket.transform.position.x)
        {
            Point_int++;
            Prompt.ShouldFollow = false;
            Target = new Vector3(0, 4, 0) - transform.position;
            Target.y = 5;
            Ball.linearVelocity = Target.normalized * ballSpeed;
            ShouldMove = true;
            Sound.clip = SoundClip;
            Sound.Play();
        }

    }

    public void freeze()
    {
        AI_racket_rb.position = Vector3.zero;
    }

    public void changeSpeed(float speed)
    {
        AI.reactionSpeed = speed;
    }

    bool IsValidVector3(Vector3 v)
    {
        return !(float.IsNaN(v.x) || float.IsNaN(v.y) || float.IsNaN(v.z));
    }


}
