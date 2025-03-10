using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lvl5marbleinstant : MonoBehaviour
{
    public bool insmarble,lose,win,onetime;
    public GameObject mrbl;
    public int i;
    public GameObject[] mrbimg;
    public GameObject AdsWarning;
    // Start is called before the first frame update
    void Start()
    {
        if(!insmarble)
        {
            mrbimg[i].SetActive(false);
            i++;
            insmarble = true;
            Instantiate(mrbl, transform.position,Quaternion.identity);

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!insmarble && i<5)
        {
            mrbimg[i].SetActive(false);
            i++;
            insmarble = true;
            Instantiate(mrbl, transform.position, Quaternion.identity);
            Debug.Log("Inside the !isMarble && thee value of i     "+"  "+i);
        }
        Debug.Log("Outside the !isMarble && thee value of i     " + "  " + i);
        if (lose && !onetime)
        {
            onetime = true;
            StartCoroutine(dieplayer());
            
        }

        if(FindObjectOfType<UiManager>().t<=0 && !win)
        {
            lose = true;
        }
    }

    IEnumerator dieplayer()
    {
        //=========================================================================================================================================================== for Update GameScore and GG Coins

        GrandAdManager.isWinOrLoseLevel = "loss";
        APIManager.Instance.UpdateGameScore(GrandAdManager.TotalScore, GrandAdManager.isWinOrLoseLevel, gamemanager.instance.getLevel() + 1, GrandAdManager.TotalGGCoinsEarned);
      
        //============================================================================================================================================================ 


        FindObjectOfType<UiManager>().startcount = false;
        SoundManager.instance.Play("lose");
        yield return new WaitForSeconds(2f);
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
}
