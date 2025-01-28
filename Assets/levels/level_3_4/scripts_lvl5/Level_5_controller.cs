using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_5_controller : MonoBehaviour
{
    public bool active;
    public Camera cam;
    public LayerMask box_layer;
    public Level_5_Control_Dalgona control_dalgona_chosen;
    [SerializeField] GameObject win_panel, lose_panel;
    [SerializeField] GameObject Xwin_panel;

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
        Debug.Log("CHOOSE BOS");
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Debug.Log("box_layer" + box_layer);
        Debug.DrawRay(ray.origin, ray.direction * 1000000, Color.red);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, box_layer))
        {
            Debug.Log("HIT : " + hit.collider.name);
            active = false;
            Level_5_boxControl box_script = hit.collider.GetComponent<Level_5_boxControl>();

            box_script.hide_other_boxes();
            box_script.hide_msg_choose_box();
            control_dalgona_chosen = box_script.get_active_dalgona();
            box_script.show_dagona();

            box_script.move_cam_move_cover();
        }
        else
        {
            Debug.Log("box_layer" + box_layer);
            //Debug.Log("WRONG HIT : "+hit.collider.name);
        }
    }

    public void start_game()
    {
        control_dalgona_chosen.active = true;
    }

    public void show_win_panel()
    {
        Xwin_panel.SetActive(true);
    }

    public void show_lose_panel()
    {
        Xwin_panel.SetActive(true);
    }

}
