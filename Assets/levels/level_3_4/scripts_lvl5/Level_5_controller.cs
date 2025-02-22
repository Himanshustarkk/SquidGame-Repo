using System.Collections;
using System.Collections.Generic;
using GameAnalyticsSDK;
using UnityEngine;

public class Level_5_controller : MonoBehaviour
{
    public bool active;
    public Camera cam;
    public LayerMask box_layer;
    public Level_5_Control_Dalgona control_dalgona_chosen;
    public GameObject win_panel, lose_panel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!active) return;

        if (Input.GetMouseButtonDown(0))
        {
            //raycast
            choose_box();
        }
    }

    void choose_box()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 2f); // Red line for visualization


        if (Physics.Raycast(ray, out hit, Mathf.Infinity, box_layer))
        {
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 2f); // Red line for visualization

            Debug.Log("Hit with "+hit.collider.name+"this is layer name "+box_layer);
            active = false;
            Level_5_boxControl box_script = hit.collider.GetComponent<Level_5_boxControl>();

            box_script.hide_other_boxes();
            box_script.hide_msg_choose_box();
            control_dalgona_chosen = box_script.get_active_dalgona();
            box_script.show_dagona();

            box_script.move_cam_move_cover();
            
        }

    }

    public void start_game()
    {
        Debug.Log("Isnide the start _game");
        Invoke(nameof(ActiveDalgon),1f);
        Debug.Log("After the start _game");

    }

    void ActiveDalgon()
    {
        control_dalgona_chosen.active = true;
    }

    public void show_win_panel()
    {
        win_panel.SetActive(true);
        if(win_panel==null)
        {
            Debug.Log("Win Panel is Null");
        }
        else
        {
            Debug.Log("Found the Win Panel");
        }
        //=========================================================================================================================================================== for Update GameScore and GG Coins
        Debug.Log("GG Coins BEfooe" + GrandAdManager.TotalGGCoinsEarned);
        GrandAdManager.isWinOrLoseLevel = "win";
        APIManager.Instance.coinsEarningLevelBased(gamemanager.instance.getLevel() + 1);
        GrandAdManager.TotalScore += 100;

        APIManager.Instance.UpdateGameScore(GrandAdManager.TotalScore, GrandAdManager.isWinOrLoseLevel, gamemanager.instance.getLevel() + 1, GrandAdManager.TotalGGCoinsEarned);
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Level" + gamemanager.instance.getLevel() + 1, "Points", GrandAdManager.TotalScore);


        Debug.Log("Total GGCoins Earned" + GrandAdManager.TotalGGCoinsEarned);
        //=========================================================================================================================================================== 

    }

    public void show_lose_panel()
    {
        lose_panel.SetActive(true);
        if (lose_panel == null)
        {
            Debug.Log("Lose Panel is Null");
        }
        else
        {
            Debug.Log("Found the loss Panel");
        }
    }

}
