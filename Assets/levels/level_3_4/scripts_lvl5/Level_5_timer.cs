using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Level_5_timer : MonoBehaviour
{
    public GameObject lose_panel, InGame_panel;

    public bool active;
    public Text text_timer;
    public float total_time, max_time, timer;
    Level_5_controller control_script;
    public GameObject guide , btn_start, MainInfo;

    // Start is called before the first frame update
    void Start()
    {
        control_script = FindObjectOfType<Level_5_controller>();
        if(control_script == null )
        {
            Debug.Log("Script is nul");
        }    
        else
        {
            text_timer.text = total_time.ToString();

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            if (timer >= max_time)
            {
                total_time -= 1f;
                text_timer.text = total_time.ToString();
                timer = 0f;
            }
            else
            {
                timer += Time.deltaTime;
            }

            if (total_time <= 0)
            {
                total_time = 0f;
                text_timer.text = total_time.ToString();
                active = false;

                StartCoroutine(show_lose_panel());
            }
        }
    }

    IEnumerator show_lose_panel()
    {
        print("lose timeout");
        //msg timeout

        Level_5_cam cam_script = FindObjectOfType<Level_5_cam>();
        cam_script.lose_move();


        yield return new WaitForSeconds(3.5f);

        lose_panel.SetActive(true);
        InGame_panel.SetActive(false);

        Level_5_controller controller_script = FindObjectOfType<Level_5_controller>();
        controller_script.show_lose_panel();
    }

    public void btn_start_game()
    {
        if (Level_5_boxControl.boxClicked)
        {
            TextMeshProUGUI _GGCoinTextConsistent = GameObject.FindGameObjectWithTag("GGConsistenCoin").GetComponent<TextMeshProUGUI>();
            if (_GGCoinTextConsistent != null)
            {
                _GGCoinTextConsistent.text = GrandAdManager.TotalGGCoinsEarned.ToString();
            }
            else
            {


            }
            active = true;

            btn_start.SetActive(false);
            InGame_panel.SetActive(true);
            if (control_script == null)
            {
                Debug.Log("Control script is null");
            }
            else
            {
                control_script.start_game();

            }
        }
    }
}
