using UnityEngine;
using UnityEngine.UI;

public class Level_3_Arrow : MonoBehaviour
{

    public float arrow_back_speed, arrow_forward_speed;
    public float max_dist_arrow, min_dist_arrow;
    public float dist_between_arrows;
    public Transform Arrow_Min_pos;
    public Image filled_bg;
    public RectTransform PowerZone;
    public Color[] clrs;
    public RectTransform[] list_target_positions;
    public Image red_bg, white_bg;
    public int Actual_pos;
    public float timer, max_time , speed_target;
    public bool can_move;
    Level_3_Conroller control_script;

    // Start is called before the first frame update
    void Start()
    {
        control_script = FindObjectOfType<Level_3_Conroller>();
    }

    // Update is called once per frame
    void Update()
    {
        float Arrow_X_Pos = transform.localPosition.x;
     //   Debug.Log(my_x_pos + "My X pos");
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(transform.localPosition + "Before");

            float inc = arrow_forward_speed * Time.deltaTime;

            Vector3 tmp = transform.localPosition;
            tmp.x += inc;

            tmp.x = Mathf.Clamp(tmp.x, min_dist_arrow, max_dist_arrow);

            transform.localPosition = tmp;
            Debug.Log(transform.localPosition+"After");

           // Debug.Log(tmp);

            //Debug.Log("Getting Input");
        }
        else
        {
            Debug.Log(transform.localPosition + "iNSIDE eLESE");
            // decrease on x arrow
            decrease_on_x_arrow();
           // Debug.Log("No  Mouse Input ");

        }


        // filled the bar
        filled_bar();
        //check arrow if is in centre
        check_between_arrows();
        // change target everytime
        change_target_position();

        //if (my_x_pos <= min_dist_arrow)
        //{
        //    Vector3 tmp = transform.localPosition;
        //    tmp.x = min_dist_arrow;
        //    transform.localPosition = tmp;

        //}
        //else if(my_x_pos >= max_dist_arrow)
        //{
        //    Vector3 tmp = transform.localPosition;
        //    tmp.x = max_dist_arrow;
        //    transform.localPosition = tmp;
        //}



    }

    public void decrease_on_x_arrow()
    {
        float inc = arrow_back_speed * Time.deltaTime;

        Vector3 tmp = transform.localPosition;
        tmp.x -= inc;

        tmp.x = Mathf.Clamp(tmp.x, min_dist_arrow, max_dist_arrow);

        transform.localPosition = tmp;
    }

    public void filled_bar()
    {
        Vector2 posA = Arrow_Min_pos.GetComponent<RectTransform>().localPosition; // Storing the Position of Initial bar 
        Vector2 posB = GetComponent<RectTransform>().localPosition; // Get the Moving Arrow position
        float distance = Vector2.Distance(posA, posB);

        float percent = distance / 500;

        filled_bg.fillAmount = percent;

    }

    public void check_between_arrows()
    {
        Vector2 Powerzone_position = PowerZone.GetComponent<RectTransform>().localPosition;
        Vector2 MovingArrow_pos = GetComponent<RectTransform>().localPosition;
        Debug.Log("PowerZone Positions" + Powerzone_position);
        Debug.Log("MovingArrow Positions" + MovingArrow_pos);

        if (MovingArrow_pos.x >= (Powerzone_position.x - dist_between_arrows) && MovingArrow_pos.x <= (Powerzone_position.x + dist_between_arrows))
        {
            red_bg.gameObject.SetActive(false);
            white_bg.gameObject.SetActive(true);

            filled_bg.color = clrs[1];
            control_script.can_pull = true;
            //Debug.Log("******If");
            Debug.Log(control_script.can_pull+"CanPull"); 

        }
        else
        {
            red_bg.gameObject.SetActive(true);
            white_bg.gameObject.SetActive(false);

            filled_bg.color = clrs[0];
            control_script.can_pull = false;
            //Debug.Log("********Else");
        }

    }

    public void change_target_position()
    {
        Vector2 target_vr = list_target_positions[Actual_pos].GetComponent<RectTransform>().localPosition;
        Vector2 my_pos = PowerZone.GetComponent<RectTransform>().localPosition;

        if (timer >= max_time)
        {
            can_move = true;
            timer = 0f;
            Actual_pos++;
            if(Actual_pos >= list_target_positions.Length - 1)
            {
                Actual_pos = 0;
            }
        }
        else
        {
            timer += Time.deltaTime;

            if(Vector2.Distance(target_vr, my_pos) <= .02f && can_move)
            {
                can_move = false;
                PowerZone.GetComponent<RectTransform>().localPosition = target_vr;
            }
            else if(can_move)
            {
                my_pos = Vector2.MoveTowards(my_pos, target_vr, speed_target * Time.deltaTime);
                PowerZone.GetComponent<RectTransform>().localPosition = my_pos;
            }
        }
    }
}
