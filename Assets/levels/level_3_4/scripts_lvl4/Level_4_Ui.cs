using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_4_Ui : MonoBehaviour
{
    public GameObject panel_InGame;
    public GameObject panel_start;
    Level_4_Controller control_script;
    public Level_4_Timer timer_script;
    // Start is called before the first frame update
    void Start()
    {
        control_script = FindObjectOfType<Level_4_Controller>();
    }

    public void btn_start()
    {
        control_script.game_run = true;
        control_script.start_game = true;


        panel_InGame.SetActive(true);
        panel_start.SetActive(false);
        timer_script.active = true;

    }
}
