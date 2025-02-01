using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MoreMountains.NiceVibrations;

public class Level_5_Control_Dalgona : MonoBehaviour
{
    public DOTweenPath dot_path;
    public Animator anim;
    public Transform[] list_dalgona_part_pos;
    public GameObject dalgona_center;
    public GameObject[] dalgona_parts , dalgona_break_parts , dalgona_center_break_parts;
    public int actual_part;
    public Transform needle;
    public bool active;

    public float max_time, timer;
    public string actual_anim;

    //vibrate setting
    public float vibrate_timer_success, vibrate_max_times_success , vibrate_timer_failure, vibrate_max_times_failure;

    // timer script
    Level_5_timer timer_script;

    // Start is called before the first frame update
    void Start()
    {
        actual_anim = "needle_idle";
    }

    // Update is called once per frame
    void Update()
    {
        if (!active) return;

        if (Input.GetMouseButtonDown(0))
        {
            // play
            dot_path.DOPlay();
            animate_needle("needle_zigzag_simple");
        }

        if (Input.GetMouseButtonUp(0))
        {
            // pause
            dot_path.DOPause();
            animate_needle("needle_idle");

            //reset timer
            timer = 0f;

            //reset vibrate timer
            vibrate_timer_success = 0f;
            vibrate_timer_failure = 0f;

        }

        if (Input.GetMouseButton(0))
        {
            // check click timeout
            check_click_timeout();
        }

        // check reached 
        check_reached_finish_part();

    }

    public void check_reached_finish_part()
    {
        //print(Vector3.Distance(needle.position, list_dalgona_part_pos[actual_part].position));
        //print("needle.position" + needle.position);
        //print("list_dalgona_part_pos" + list_dalgona_part_pos[actual_part].position);
        if (Vector3.Distance(needle.position , list_dalgona_part_pos[actual_part].position) <= .2f)
        {
            if(actual_part >= list_dalgona_part_pos.Length -1)
            {
                active = false;
                dalgona_parts[actual_part].SetActive(false);
                dalgona_break_parts[actual_part].SetActive(true);

                //animate to idle
                animate_needle("needle_idle");

                //win panel
                StartCoroutine(show_win());
            }
            else
            {
                dalgona_parts[actual_part].SetActive(false);
                dalgona_break_parts[actual_part].SetActive(true);
                actual_part++;
            }
        }


    }

    void check_click_timeout()
    {

        if(timer >= max_time)
        {
            active = false;
            print("timeout");

            // pause
            dot_path.DOPause();

            //animate to idle
            animate_needle("needle_idle");

            //lose panel
            StartCoroutine(show_lose());

        }
        else
        {
            timer += Time.deltaTime;

            if(timer >= max_time - 1.5f)
            {
                //change animation 
                animate_needle("needle_zigzag_timeout");
                failure_vibrate();
                //print("failure vibrate");
            }
            else
            {
                //print("success vibrate");
                success_vibrate();
            }
        }
    }

    void animate_needle(string name)
    {
        if(name != actual_anim)
        {
            anim.Play(name);
            actual_anim = name;
        }
    }

    IEnumerator show_lose()
    {
        print("you lose");

        timer_script = FindObjectOfType<Level_5_timer>();
        timer_script.active = false;
        timer_script.gameObject.SetActive(false);

        Level_5_cam cam_script = FindObjectOfType<Level_5_cam>();
        cam_script.lose_move();


        //// hide dalgona parts
        //for (int i = 0; i < dalgona_parts.Length; i++)
        //{
        //    dalgona_parts[i].SetActive(false);
        //}
        //// show break dalgona parts
        //for (int i = 0; i < dalgona_break_parts.Length; i++)
        //{
        //    dalgona_break_parts[i].SetActive(true);
        //}

        //hide dalgona center
        dalgona_center.SetActive(false);

        // show break dalgona parts
        for (int i = 0; i < dalgona_center_break_parts.Length; i++)
        {
            dalgona_center_break_parts[i].SetActive(true);
        }

        if(actual_part < list_dalgona_part_pos.Length - 1)
        {
            dalgona_parts[actual_part].SetActive(false);
            dalgona_break_parts[actual_part].SetActive(true);
        }

        //show lose panel
     //   Advertisements.Instance.ShowInterstitial();
        yield return new WaitForSeconds(3f);
        Level_5_controller controller_script = FindObjectOfType<Level_5_controller>();
        controller_script.show_lose_panel();

    }

    IEnumerator show_win()
    {
        print("you won");
        timer_script = FindObjectOfType<Level_5_timer>();
        timer_script.active = false;
        timer_script.gameObject.SetActive(false);

        Level_5_cam cam_script = FindObjectOfType<Level_5_cam>();
        cam_script.win_move();

        yield return new WaitForSeconds(3f);
        //show lose panel
      //  Advertisements.Instance.ShowInterstitial();
        Level_5_controller controller_script = FindObjectOfType<Level_5_controller>();
        controller_script.show_win_panel();
    }


    void success_vibrate()
    {
        if(vibrate_timer_success >= vibrate_max_times_success)
        {
            // success vibrate
            MMVibrationManager.Haptic(HapticTypes.Success, true, this);
            vibrate_timer_success = 0f;
        }
        else
        {
            vibrate_timer_success += Time.deltaTime;
        }
    }

    void failure_vibrate()
    {
        if (vibrate_timer_failure >= vibrate_max_times_failure)
        {
            // success vibrate
            MMVibrationManager.Haptic(HapticTypes.Failure, true, this);
            vibrate_timer_failure = 0f;
        }
        else
        {
            vibrate_timer_failure += Time.deltaTime;
        }
    }
}
