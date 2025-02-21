using System.Collections;
using System.Collections.Generic;
using GameAnalyticsSDK;
using TMPro;
using UnityEngine;

public class Level_3_Start : MonoBehaviour
{
    public GameObject controls;
    public GameObject Tutorial_Panel;
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

        controls.SetActive(true);
        TextMeshProUGUI _GGCoinTextConsistent = GameObject.FindGameObjectWithTag("GGConsistenCoin").GetComponent<TextMeshProUGUI>();
        _GGCoinTextConsistent.text = GrandAdManager.TotalGGCoinsEarned.ToString();
        Tutorial_Panel.SetActive(false);


    }
}
