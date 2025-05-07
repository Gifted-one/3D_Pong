using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop_Manager : MonoBehaviour
{
    public bool slow_time;
    public bool reflect;
    public bool fire;
    public bool freeze;

    PlayerPrompt prompt;
    //Ball_movement Ball;

    public GameObject Prompt;
    public GameObject Shop_panel;
    public GameObject Pause_Panel;
    //public GameObject ball;

    // Start is called before the first frame update
    void Start()
    {
        slow_time = false;
        reflect = false; 
        fire = false;
        freeze = false;
        prompt = Prompt.GetComponent<PlayerPrompt>();
        //Ball = ball.GetComponent<Ball_movement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Manager(string Item)
    {
        if(Item == "Time" && Ball_movement.Point_int > 10)
        {
            slow_time = true;
            prompt.Abilities.Add(Item);
            Ball_movement.Point_int -= 10;
        }

        if (Item == "Reflect" && Ball_movement.Point_int > 20)
        {
            reflect = true;
            prompt.Abilities.Add(Item);
            Ball_movement.Point_int -= 20;
        }

        if (Item == "Fire" && Ball_movement.Point_int > 10)
        {
            fire = true;
            prompt.Abilities.Add(Item);
            Ball_movement.Point_int -= 10;
        }

        if (Item == "Freeze" && Ball_movement.Point_int > 20)
        {
            freeze = true;
            prompt.Abilities.Add(Item);
            Ball_movement.Point_int -= 20;
        }
    }
    public void Hide()
    {
        Shop_panel.SetActive(false);
        Prompt.SetActive(true);
        Pause_Panel.SetActive(true);

    }
}
