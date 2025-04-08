using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class PlayerPrompt : MonoBehaviour
{

    public TextMeshProUGUI Centre_text;
    public TextMeshProUGUI Tutorial_text;
    public TextMeshProUGUI Time_text;
    public TextMeshProUGUI You_lostText;

    float Delay;
    float Timer = 0;

    public int countdown;
    int TimeToStart = 30;
    int TimetoDisplay = 0;
    int inc = 0;

    bool ShouldFollow;
    bool textDelay;

    public GameObject centreText;
    public GameObject ball;
    public GameObject Pause_Screen;
    public GameObject UnpauseButton;

    Ball_movement Ball;
    public bool ShouldKick;


    // Start is called before the first frame update
    void Start()
    {
        Ball = ball.GetComponent<Ball_movement>();
        ShouldFollow = true;
        ShouldKick = false;
        textDelay = true;
        Tutorial_text.gameObject.SetActive(false);
        Pause_Screen.SetActive(false);
        You_lostText.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if(TimetoDisplay % 5 == 0 && textDelay)
        {
            tutorial_text();
            Delay = 2;
            textDelay = false;
        }
    }
    void FixedUpdate()
    {
        Timer += 1 * Time.deltaTime;
        countdown = (int)Timer;
        TimetoDisplay = (int)Timer;

        if(TimetoDisplay < 10)
        {
            Time_text.text = "Time: " + "0" + TimetoDisplay.ToString();

        }
        else
        {
            Time_text.text = "Time: " + TimetoDisplay.ToString();
        }

        if (TimeToStart > 0)
        {
            TimeToStart = 30;
            TimeToStart -= countdown;
            Centre_text.text = TimeToStart.ToString();

        }
        else if(TimeToStart == 0 && ShouldFollow)
        {
            Centre_text.gameObject.SetActive(false);
            Ball.KickOff();
            ShouldFollow = false;
        }

        //Delay funtion, If needed.
        if(Delay > 0)
        {
            Delay -= Time.deltaTime;

        }
        else
        {
            textDelay = true;
        }
    }

    public void OutComeScreen(string who)
    {
        if(who == "Player")
        {
            Centre_text.gameObject.SetActive(true);
            Centre_text.text = "You lose!";
            Pause();
            UnpauseButton.SetActive(false);
            You_lostText.gameObject.SetActive(true);
            You_lostText.text = "You lost.";

        }
        else if(who == "Opponent")
        {
            Centre_text.gameObject.SetActive(true);
            Centre_text.text = "You Win!";
            Pause();
            UnpauseButton.SetActive(false);
            You_lostText.gameObject.SetActive(true);
            You_lostText.text = "You won.";
        }

    }

    void tutorial_text()
    {
        if (inc == 1)
        {
            Tutorial_text.gameObject.SetActive(true);
            Tutorial_text.text = "Use your mouse to move the racket.";
        }
        else if (inc == 2)
        {
            Tutorial_text.text = "Press H to hide the cursor.";
        }
        else if (inc == 3)
        {
            Tutorial_text.text = "Use the scroll wheel to move the racket further or closer to the screen.";
        }
        else if (inc == 4)
        {
            Tutorial_text.text = "If the ball moves past you, you lose.";
        }
        else if (inc == 5)
        {
            Tutorial_text.text = "If it moves past your opponent, you win.";
        }
        else
        {
            Tutorial_text.gameObject.SetActive(false);
        }
        inc++;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Restart()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Main");
        
    }

    public void Pause()
    {
        Time.timeScale = 0;
        Pause_Screen.SetActive(true);
        centreText.SetActive(false);

    }

    public void Unpause()
    {
        Time.timeScale = 1;
        Pause_Screen.SetActive(false);
        centreText.SetActive(true);
    }
}
