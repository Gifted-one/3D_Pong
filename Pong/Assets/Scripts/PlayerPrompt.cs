using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;


public class PlayerPrompt : MonoBehaviour
{

    public TextMeshProUGUI Centre_text;
    public TextMeshProUGUI Tutorial_text;
    public TextMeshProUGUI Time_text;
    public TextMeshProUGUI You_lostText;
    public TextMeshProUGUI specialAbilities;
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI LevelText;

    float Delay;
    public static float Timer = 0;
    float AbilitiesDelay = 0;

    public int countdown;
    public int TimeToStart; //Make 30 for on screen text
    int TimetoDisplay = 0;
    int inc = 0;
    static int Level = 1;
    static int score;
    public int Holder;


    public bool ShouldFollow;
    public bool ShouldFollow2;
    bool textDelay;
    bool applyAbility;
    bool applyDelay;
    bool incScore = true;
    public bool ShouldKick;
    public bool starting;
    //static bool restastin;

    public GameObject centreText;
    public GameObject ball;
    public GameObject Pause_Screen;
    public GameObject UnpauseButton;
    public GameObject Shop_Panel;

    Ball_movement Ball;

    public List<string> Abilities = new List<string>();

    public static string dir;


    // Start is called before the first frame update
    void Start()
    {
        Ball = ball.GetComponent<Ball_movement>();
        ShouldFollow = true;
        ShouldFollow2 = false;
        ShouldKick = false;
        textDelay = true;
        applyAbility = false;
        applyDelay = true;
        Tutorial_text.gameObject.SetActive(false);
        Pause_Screen.SetActive(false);
        Shop_Panel.SetActive(false);
        You_lostText.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {   

        if(TimetoDisplay % 5 == 0 && textDelay && starting)
        {
            tutorial_text();
            Delay = 2;
            textDelay = false;
        }

        if(Abilities.Count > 0) 
        {
            specialAbilities.text = "You've got " + Abilities.Count + " special abilities, press right button mouse to use.";
        }
        else
        {
            specialAbilities.text = "You've got no special abilities.";
        }

        if (Abilities.Count > 0 && Input.GetMouseButtonDown(1) || applyAbility)
        {
            applyAbility = true;
            if (Abilities.Count > 0 && Input.GetMouseButtonDown(1) && applyDelay)
            {
                AbilitiesDelay = 5;
                applyDelay = false;
            }

            if (Abilities[0] == "Time")
            {
                if (AbilitiesDelay > 0)
                {
                    Time.timeScale = 0.5f;
                    AbilitiesDelay -= Time.deltaTime;

                }
                else
                {
                    Time.timeScale = 1f;
                    applyDelay = true;
                    applyAbility = false;
                    Abilities.RemoveAt(0);
                }
            }
            else if (Abilities[0] == "Fire")
            {
                if (AbilitiesDelay > 0)
                {
                    Ball.ballSpeed = 150;
                    AbilitiesDelay -= Time.deltaTime;

                }
                else
                {
                    Ball.ballSpeed = 70;
                    applyDelay = true;
                    applyAbility = false;
                    Abilities.RemoveAt(0);
                }
            }
            else if (Abilities[0] == "Reflect")
            {
                if (AbilitiesDelay > 0)
                {
                    Ball.reflect();
                    AbilitiesDelay -= Time.deltaTime;

                }
                else
                {

                    applyDelay = true;
                    applyAbility = false;
                    Abilities.RemoveAt(0);
                }
            }
            else if (Abilities[0] == "Freeze")
            {
                if (AbilitiesDelay > 0)
                {
                    Ball.freeze();
                    AbilitiesDelay -= Time.deltaTime;

                }
                else
                {

                    applyDelay = true;
                    applyAbility = false;
                    Abilities.RemoveAt(0);
                }
            }
        }
        ScoreText.text = score.ToString();
        LevelText.text = Level.ToString();

        if(score == 5 && incScore)
        {
            Level = 2;
            incScore = false;
        }
        else if(score == 10 && incScore)
        {
            Level = 3;
            incScore = false;
        }
        else if(score == 15 && incScore)
        {
            Level = 4;
            incScore = false;
        }
        else if(score != 5 && score != 10 && score != 15)
        {
            incScore = true;
        }

        if(Level == 1)
        {
            Ball.Level_Speed = 5f;
            Ball.changeSpeed(0.5f);
        }
        else if(Level == 2)
        {
            Ball.Level_Speed = 10f;
            Ball.changeSpeed(0.7f);
        }
        else if(Level == 3)
        {
            Ball.Level_Speed = 15f;
            Ball.changeSpeed(0.9f);
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
            if (starting)
            {
                TimeToStart = 30;
                starting = true;
            }
            else if(!starting)
            {
                TimeToStart = Holder;
            }
            TimeToStart -= countdown;
            Centre_text.gameObject.SetActive(true);
            Centre_text.text = TimeToStart.ToString();

        }
        else if(TimeToStart == 0 && ShouldFollow)
        {
            Centre_text.gameObject.SetActive(false);
            Ball.KickOff(dir);
        }

        //Delay funtion, If needed.
        if (Delay > 0)
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
            score++;
            Centre_text.gameObject.SetActive(true);
            Centre_text.text = "You Win!";
            Pause();
            UnpauseButton.SetActive(false);
            You_lostText.gameObject.SetActive(true);
            You_lostText.text = "You won.";
        }

    }

    public void scoreKeeper(string who)
    {
        if (who == "Player")
        {
            if(score > 0)
            {
                score--;
            }
            Restart();
            starting = false;
            //TimeToStart = countdown + 5;
            dir = "player";
            //ShouldFollow2 = true;
        }
        else if (who == "Opponent")
        {
            Restart();
            score++;
            starting = false;
            //TimeToStart = countdown + 5;
            dir = "opponent";
            //ShouldFollow2 = true;
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
            Tutorial_text.text = "Press H to hide the cursor and Escape to unhide it";
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
        SceneManager.LoadScene("Main");
        Time.timeScale = 1.0f;

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

    public void Shop()
    {
        Pause_Screen.SetActive(false);
        Shop_Panel.SetActive(true);
    }
}
