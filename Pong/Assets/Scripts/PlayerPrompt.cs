using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PlayerPrompt : MonoBehaviour
{

    public TextMeshProUGUI Centre_text;

    float Delay;
    float Timer = 0;

    public int countdown;
    int TimeToStart = 5;

    bool ShouldFollow;

    public GameObject centreText;
    public GameObject ball;

    Ball_movement Ball;
    public bool ShouldKick;


    // Start is called before the first frame update
    void Start()
    {
        Ball = ball.GetComponent<Ball_movement>();
        ShouldFollow = true;
        ShouldKick = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (ShouldKick)
        {
            //Ball.KickOff();
        }
    }
    void FixedUpdate()
    {
        Timer += 1 * Time.deltaTime;
        countdown = (int)Timer;
        

        if(TimeToStart > 0)
        {
            TimeToStart = 5;
            TimeToStart -= countdown;
            Centre_text.text = TimeToStart.ToString();

        }
        else if(TimeToStart == 0 && ShouldFollow)
        {
            centreText.SetActive(false);
            //ShouldKick = true;
            Ball.KickOff();
            ShouldFollow = false;
        }

        //Delay funtion
        if(Delay > 0)
        {
            Delay -= Time.deltaTime;

        }
        else
        {

        }
    }
}
