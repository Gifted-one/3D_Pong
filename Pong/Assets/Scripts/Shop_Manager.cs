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

    public GameObject Prompt;
    public GameObject Shop_panel;
    public GameObject Pause_Panel;

    // Start is called before the first frame update
    void Start()
    {
        slow_time = false;
        reflect = false; 
        fire = false;
        freeze = false;

        //Shop_panel.SetActive(false);

        prompt = Prompt.GetComponent<PlayerPrompt>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Manager(string Item)
    {
        if(Item == "Time")
        {
            slow_time = true;
            prompt.Abilities.Add(Item);
        }

        if (Item == "Reflect")
        {
            reflect = true;
            prompt.Abilities.Add(Item);
        }

        if (Item == "Fire")
        {
            fire = true;
            prompt.Abilities.Add(Item);
        }

        if (Item == "Freeze")
        {
            freeze = true;
            prompt.Abilities.Add(Item);
        }
    }
    public void Hide()
    {
        Shop_panel.SetActive(false);
        Prompt.SetActive(true);
        Pause_Panel.SetActive(true);

    }
}
