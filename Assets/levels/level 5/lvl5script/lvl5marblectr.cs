using System.Collections;
using System.Collections.Generic;
using GameAnalyticsSDK;
using UnityEngine;

public class lvl5marblectr : MonoBehaviour
{
    Rigidbody rb;
    Vector3 v;
    bool stop, onetime, timerone, finish;

    // Start is called before the first frame update
    void Start()
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "Level" + gamemanager.instance.getLevel() + 1);

        Application.targetFrameRate = 60;
        v = new Vector3(0, 0, 0);
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        /// Debug.Log("Stop //  rig.velocity.z //!oneTime " + stop + " "+ rb.velocity.z +" "+  !onetime);
        if (stop && rb.velocity.z <= 0 && !onetime)
        {
            onetime = true;
            gameObject.tag = "Untagged";
            Debug.Log("Printing Values of i and Finish Bool   " + "   " + finish);
            if (FindObjectOfType<lvl5marbleinstant>().i < 5 && !finish)
            {
                FindObjectOfType<lvl5marbleinstant>().insmarble = false;
                FindObjectOfType<barpwr>().stop = false;
                int storeivalue = FindObjectOfType<lvl5marbleinstant>().i;
                Debug.Log("Inside the Condition of i<5 and !finsh making isinmarble=false so that iut can instantiate (This is i value)   " + storeivalue);

            }

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ground" && !timerone)
        {

            Debug.Log("On Collision");
            timerone = true;
            stop = true;
            rb.velocity = new Vector3(0, 0, 0);
            //FindObjectOfType<lvl5cammove>().followmbl = false;
            StartCoroutine(stopmarble());
            if (FindObjectOfType<lvl5marbleinstant>().i == 5 && !finish)
            {
                FindObjectOfType<lvl5marbleinstant>().lose = true;
                Debug.Log("I have lost the game inside if Conmdition");
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, "Level" + gamemanager.instance.getLevel() + 1, "Points", GrandAdManager.TotalScore);

            }

        }
    }



    IEnumerator stopmarble()
    {
        Debug.Log("Inside Stop Marble Function  ");

        yield return new WaitForSeconds(2f);
        rb.velocity = new Vector3(0, 0, 0);
        rb.rotation = Quaternion.Euler(0, 0, 0);
        rb.drag = 50;
        rb.rotation = Quaternion.identity;
        Debug.Log("Marble Function Executed " + rb.velocity.z);



    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Finish")
        {
            finish = true;
            FindObjectOfType<lvl5cammove>().followmbl = true;
            FindObjectOfType<lvl5marbleinstant>().win = true;
            StartCoroutine(winplayer());
        }
        Debug.Log("Here");

    }

    IEnumerator winplayer()
    {

        


        FindObjectOfType<UiManager>().wineffet.SetActive(true);
        FindObjectOfType<UiManager>().startcount = false;
        SoundManager.instance.Play("win");
        yield return new WaitForSeconds(3f);
        // Gley.MobileAds.Internal.MobileAdsExample.Instance.ShowInterstitial();


        //


        FindObjectOfType<UiManager>().winpanel.SetActive(true);
        //=========================================================================================================================================================== for Update GameScore and GG Coins

        GrandAdManager.isWinOrLoseLevel = "Win";
        APIManager.Instance.coinsEarningLevelBased(gamemanager.instance.getLevel() + 1);
        GrandAdManager.TotalScore += 100;

        APIManager.Instance.UpdateGameScore(GrandAdManager.TotalScore, GrandAdManager.isWinOrLoseLevel, gamemanager.instance.getLevel() + 1, GrandAdManager.TotalGGCoinsEarned);
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Level" + gamemanager.instance.getLevel() + 1, "Points", GrandAdManager.TotalScore);

        //=========================================================================================================================================================== 

    }
}
