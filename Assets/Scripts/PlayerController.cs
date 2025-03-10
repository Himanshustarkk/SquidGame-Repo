using System.Collections;
using UnityEngine;
using CnControls;
using DG.Tweening.Core.Easing;
using GameAnalyticsSDK;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{

    [Header(" Control Settings ")]
    public Rigidbody thisRigidbody;
    public SimpleJoystick joystick;
    public float moveSpeed;
    public float RotSpeed;
    public float maxX;
    public float maxZ;
    bool move;
    public static bool canMove;
    public bool GmRun, die, chwya, win;

    public static bool IsGamestart;

    public GameObject AdsWarning;
    //[Header(" Rotation Control ")]	


    // Use this for initialization
    void Start()
    {

        // Store some values
        canMove = true;
        IsGamestart = false;
        GetComponent<Animator>().Play("idle3");
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "Level" + gamemanager.instance.getLevel() + 1);

        GrandAdManager.TotalGGCoinsEarned = 0;
        Debug.Log("Starting From Level_1 my Gg coin value is " + GrandAdManager.TotalGGCoinsEarned);

    }

    // Update is called once per frame
    void Update()
    {
        //joystick.
        if (GmRun && !die && !win && IsGamestart)
        {
            if (joystick.HorizintalAxis.Value != 0 || joystick.VerticalAxis.Value != 0)
            {
                if (FindObjectOfType<enemCtr>().animcor)
                {
                    GetComponent<HighlightPlus.HighlightEffect>().highlighted = true;
                    die = true;
                    StartCoroutine(dieplayer());
                }
                // Move Player
                move = true;
                GetComponent<Animator>().Play("run");
                transform.forward = new Vector3(joystick.HorizintalAxis.Value * Time.deltaTime, 0, joystick.VerticalAxis.Value * Time.deltaTime);
                GetComponent<Animator>().speed = 1;
            }
            else
            {
                move = false;
                GetComponent<Animator>().Play("Stop");
            }
        }

        if (die && !chwya)
        {
            if (joystick.HorizintalAxis.Value != 0 || joystick.VerticalAxis.Value != 0)
            {
                // Move Player
                move = true;
                GetComponent<Animator>().Play("run");
                transform.forward = new Vector3(joystick.HorizintalAxis.Value * Time.deltaTime, 0, joystick.VerticalAxis.Value * Time.deltaTime);
                GetComponent<Animator>().speed = 1;
            }
            else
            {
                move = false;
                GetComponent<Animator>().speed = 0;
            }
        }

        if (FindObjectOfType<UiManager>().t <= 0 && !win && !die)
        {
            die = true;
            StartCoroutine(dieplayer());
        }

    }

    private void FixedUpdate()
    {
        if (GmRun && !die && !win)
        {
            Vector3 pos = transform.position;
            pos.x = Mathf.Clamp(pos.x, -maxX, maxX);
            pos.z = Mathf.Clamp(pos.z, -maxZ, maxZ);
            transform.position = pos;

            if (canMove)
            {
                if (move)
                    Move();
                else
                    thisRigidbody.velocity = Vector3.zero;
            }
        }


        if (die && !chwya)
        {
            Vector3 pos = transform.position;
            pos.x = Mathf.Clamp(pos.x, -maxX, maxX);
            pos.z = Mathf.Clamp(pos.z, -maxZ, maxZ);
            transform.position = pos;

            if (canMove)
            {
                if (move)
                    Move();
                else
                    thisRigidbody.velocity = Vector3.zero;
            }
        }


    }


    public void Move()
    {
        Vector3 movement = new Vector3(joystick.HorizintalAxis.Value, 0, joystick.VerticalAxis.Value);
        movement *= moveSpeed * Time.deltaTime;

        thisRigidbody.velocity = movement;
    }

    IEnumerator dieplayer()
    {
        //=========================================================================================================================================================== GG Coins and GAmeAnalytics


        GrandAdManager.isWinOrLoseLevel = "loss";
        APIManager.Instance.UpdateGameScore(GrandAdManager.TotalScore, GrandAdManager.isWinOrLoseLevel, gamemanager.instance.getLevel() + 1, GrandAdManager.TotalGGCoinsEarned);
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, "Level" + gamemanager.instance.getLevel() + 1, "Points", GrandAdManager.TotalScore);

       

        //============================================================================================================================================================ 




        yield return new WaitForSeconds(0.5f);
        chwya = true;
        GetComponent<BoxCollider>().isTrigger = true;
        int t = Random.Range(1, 3);
        SoundManager.instance.Play("fire" + t.ToString());
        yield return new WaitForSeconds(0.1f);
        SoundManager.instance.Play("hit2");
        GameObject gm = Instantiate(FindObjectOfType<UiManager>().bloodeefect, transform);
        gm.transform.localPosition = new Vector3(0, 1.3f, 0);
        int bb = Random.Range(1, 5);
        GetComponent<HighlightPlus.HighlightEffect>().highlighted = false;
        GetComponent<Animator>().Play("die" + bb.ToString());
        GetComponent<Animator>().speed = 1;
        SoundManager.instance.Play("lose");
        yield return new WaitForSeconds(2f);
        //AdManager.Instance.ShowInterstitial();
        //Gley.MobileAds.Internal.MobileAdsExample.Instance.ShowInterstitial();
        FindObjectOfType<UiManager>().losepanel.SetActive(true);

      



        // For Showing Ads
        Debug.Log("I have Died");

        // To show Ads 
        GrandAdManager.counter += 1;
        if (GrandAdManager.counter == 2)
        {
            Debug.Log("Counter Value" + GrandAdManager.counter);
            AdsWarning.SetActive(true);

        }
        else
        {
            AdsWarning.SetActive(false);
        }

       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "win")
        {
            Debug.Log("****** Collided With Win ");
            win = true;
            StartCoroutine(winplayer());
        }
    }


    IEnumerator winplayer()
    {
        Debug.Log("Win Player Claled 2 Timnes");
     
        transform.eulerAngles = new Vector3(0, 180, 0);
        FindObjectOfType<UiManager>().wineffet.SetActive(true);
        GetComponent<Animator>().Play("win");
        GetComponent<Animator>().speed = 1;
        SoundManager.instance.Play("win");
        yield return new WaitForSeconds(4f);


       
   


        //Gley.MobileAds.Internal.MobileAdsExample.Instance.ShowInterstitial();
        FindObjectOfType<UiManager>().winpanel.SetActive(true);
        Debug.Log("Inside WinPlayer");


        //=========================================================================================================================================================== for Update GameScore and GG Coins
        APIManager.Instance.coinsEarningLevelBased(gamemanager.instance.getLevel() + 1);
        GrandAdManager.isWinOrLoseLevel = "win";
        GrandAdManager.TotalScore += 100;
        APIManager.Instance.UpdateGameScore(GrandAdManager.TotalScore, GrandAdManager.isWinOrLoseLevel, gamemanager.instance.getLevel() + 1, GrandAdManager.TotalGGCoinsEarned);


        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Level" + gamemanager.instance.getLevel() + 1, "Points", GrandAdManager.TotalScore);


        Debug.Log("Total Score value which is send " + GrandAdManager.TotalScore);
        Debug.Log("Win Player Status " + GrandAdManager.isWinOrLoseLevel);

        //=========================================================================================================================================================== for Update GameScore and GG Coins

    }
}