using System.Collections;
using System.Collections.Generic;
using GameAnalyticsSDK;
using UnityEngine;

public class Level_3_Start : MonoBehaviour
{
    public GameObject panel_arrow;
    public GameObject panel_start;
    Level_3_Conroller control_script;

    // Start is called before the first frame update
    void Start()
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "Level" + gamemanager.instance.getLevel() + 1);

        control_script = FindObjectOfType<Level_3_Conroller>();
    }
    
    public void btn_start()
    {
        control_script.game_run = true;

        panel_arrow.SetActive(true);
        panel_start.SetActive(false);

    }
}
