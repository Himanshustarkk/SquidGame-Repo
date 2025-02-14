using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class endplayerctr : MonoBehaviour
{
    public float speed, speedForward;
    public bool gamerun, winlevel,stopfolow;
    Vector3 direction, pressPos, presspos, actualpos;
    public Image powerbar;
    public Transform boss,campos,look;
    bool die,kick,win;
    public GameObject fightpanel;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (Input.GetMouseButtonDown(0) && !gamerun)
        {
            gamerun = true;
            
            GetComponent<Animator>().Play("run");
            GetComponent<Animator>().speed=1.2f;
            
            FindObjectOfType<UiManager>().startpanel.SetActive(false);
            //gamemanager.instance.gamePlaypanel.SetActive(true);
            //gamemanager.instance.gamePlaypanel.SetActive(true);
        }
        if (gamerun && !winlevel)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + speedForward * Time.deltaTime);
        }

        if (Input.GetMouseButtonDown(0))
        {
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                pressPos = hit.point;
            }


        }



        if (Input.GetMouseButton(0) && transform.childCount > 0 && !winlevel)
        {
            if (Input.GetMouseButtonDown(0))
            {
                presspos = Input.mousePosition;
                //is_tap = true;
            }

            if (Input.GetMouseButton(0)/* && is_tap*/)
            {
                actualpos = Input.mousePosition;

                float ss = actualpos.x - presspos.x;
                ss = Mathf.Clamp(ss, -1, 1);
                //print(ss);

                float xdiff = (actualpos.x - presspos.x) * Time.deltaTime * speed;

                Vector3 tmp = transform.position;

                tmp.x += xdiff;
                tmp.x = Mathf.Clamp(tmp.x, -4.5f, 3f);
                transform.position = Vector3.Lerp(transform.position,tmp,speed * Time.deltaTime);

                presspos = actualpos;


            }



        }




        if (winlevel)
        {
            if (Vector3.Distance(transform.position, boss.position) >= 1.2f)
            {
                GetComponent<Animator>().Play("run");
                transform.LookAt(boss);
                transform.position = Vector3.MoveTowards(transform.position, boss.position, 7 * Time.deltaTime);
            }
            else
            {
                if(!stopfolow)
                {
                    fightpanel.SetActive(true);
                    GetComponent<Animator>().Play("idle1");
                    FindObjectOfType<enemfight>().fight = true;
                    
                }
                stopfolow = true;
                transform.LookAt(boss);
                
                //txt.text = playerScore.ToString();
                Vector3 v = boss.position;
                
                Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, campos.position, 7 * Time.deltaTime);
                Camera.main.transform.LookAt(look);

                //StartCoroutine(fineshpart());
            }

        }


        if(boss.GetChild(1).GetChild(0).GetChild(0).gameObject.GetComponent<Image>().fillAmount<=0 && !FindObjectOfType<enemfight>().die && winlevel)
        {
            FindObjectOfType<enemfight>().die = true;
            FindObjectOfType<enemfight>().fight = false;
            FindObjectOfType<enemfight>().GetComponent<Animator>().Play("die1");
            kick = true;
            win = true;
            StartCoroutine(winplayer());
        }

        if(transform.GetChild(2).GetChild(0).GetChild(0).gameObject.GetComponent<Image>().fillAmount<=0 && !die && winlevel)
        {
            FindObjectOfType<enemfight>().win = true;
            FindObjectOfType<enemfight>().fight = false;
            FindObjectOfType<enemfight>().GetComponent<Animator>().Play("win");
            FindObjectOfType<enemfight>().GetComponent<Animator>().speed=1;
            kick = true;
            die = true;
            StartCoroutine(dieplayer());
        }

    }



    public void kicking()
    {
        
        StartCoroutine(keppkicking());
        
    }

    IEnumerator keppkicking()
    {
        float a = 0;
        while (!kick && !die && !win)
        {
            kick = true;
            int bb = Random.Range(1, 4);
            GetComponent<Animator>().Play("kick" + bb.ToString());
            GetComponent<Animator>().speed = 2;
            
            boss.GetChild(1).GetChild(0).GetChild(0).gameObject.GetComponent<Image>().fillAmount -= 0.085f;
            
            yield return new WaitForSeconds(0.6f);
            kick = false;
        }

    }




    private void LateUpdate()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="food")
        {
            Destroy(collision.gameObject);
            powerbar.fillAmount += 0.05f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="win")
        {
            winlevel = true;
        }
    }


    IEnumerator winplayer()
    {
        //=========================================================================================================================================================== for Update GameScore and GG Coins

        GrandAdManager.isWinOrLoseLevel = "win";
        GrandAdManager.TotalGGCoinsEarned += 1;
        GrandAdManager.TotalScore += 100;

        APIManager.Instance.UpdateGameScore(GrandAdManager.TotalScore, GrandAdManager.isWinOrLoseLevel, gamemanager.instance.getLevel() + 1, GrandAdManager.TotalGGCoinsEarned);

        //=========================================================================================================================================================== 


        FindObjectOfType<UiManager>().wineffet.SetActive(true);
        GetComponent<Animator>().Play("win");
        GetComponent<Animator>().speed = 1;
        SoundManager.instance.Play("win");
        yield return new WaitForSeconds(7f);
     //   Gley.MobileAds.Internal.MobileAdsExample.Instance.ShowInterstitial();
        FindObjectOfType<UiManager>().winpanel.SetActive(true);




    }


    IEnumerator dieplayer()
    {

        //=========================================================================================================================================================== for Update GameScore and GG Coins

        GrandAdManager.isWinOrLoseLevel = "loss";
        APIManager.Instance.UpdateGameScore(GrandAdManager.TotalScore, GrandAdManager.isWinOrLoseLevel, gamemanager.instance.getLevel() + 1, GrandAdManager.TotalGGCoinsEarned);

        //============================================================================================================================================================ 


        //FindObjectOfType<UiManager>().wineffet.SetActive(true);
        GetComponent<Animator>().Play("die1");
        GetComponent<Animator>().speed = 1;
        SoundManager.instance.Play("lose");
        yield return new WaitForSeconds(7f);
       // Gley.MobileAds.Internal.MobileAdsExample.Instance.ShowInterstitial();
        FindObjectOfType<UiManager>().losepanel.SetActive(true);

    }
}
