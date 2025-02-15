using System.Collections;
using DG.Tweening.Core.Easing;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;
    public Text LevelText, secondmessage;
    public bool skinEnter;
    public GameObject red, green;
    public GameObject ingamepanel;
    public GameObject playerSelectionPanel;
    public GameObject startpanel, gameplaypanel, losepanel, winpanel;
    public GameObject wineffet;
    public Text timershow;
    public float t = 65;
    public bool startcount;
    public GameObject bloodeefect;
    float ts = 1;
    public TextMeshProUGUI txtpr;
    [SerializeField] GameObject ScoreUI_Count;




    public Color clr;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {

        SoundManager.instance.Play("start");
        // Advertisements.Instance.Initialize();
        // Gley.MobileAds.Internal.MobileAdsExample.Instance.ShawBanner();
        //LevelText.text = "Level " + (gamemanager.instance.getLevel() + 1);
    }

    private void Update()
    {
        if (startcount)
        {
            t -= Time.deltaTime;
            ts -= Time.deltaTime;
            if (ts <= 0 && t >= 0)
            {
                ts = 1;
               /// SoundManager.instance.Play("click");
            }
            int a = (int)t;
            //StartCoroutine(timeconting());`
            if (a >= 0)
            {
                timershow.text = a.ToString();
                /*   if(gamemanager.instance.getLevel()<1)
                   {
                       txtpr.text = a.ToString();
                   }*/

            }

        }
    }

    //public void skinmenu()
    //{
    //    // sound
    //    SoundManager.instance.Play("click");
    //    skinEnter = true;
    //    playerSelectionPanel.SetActive(true);
    //    ingamepanel.SetActive(false);
    //}

    public void btn_retry()
    {
        print(gamemanager.instance.getlive());
        gamemanager.instance.setlive(gamemanager.instance.getlive() + 1);
        if (gamemanager.instance.getlive() > 2)
        {
            gamemanager.instance.setLevel(0);
            gamemanager.instance.setlive(0);
            SceneManager.LoadScene(1);
        }
        else
        {
            SceneManager.LoadScene(gamemanager.instance.getLevel() + 1);
        }
        SoundManager.instance.Stop("click");
        // sound
        //SoundManager.instance.Play("click");
        //gamemanager.instance.setLevel(gamemanager.instance.getLevel()-1);
        //SceneManager.LoadScene(gamemanager.instance.getLevel()+1);
    }

    public void nextlvl()
    {
        //GrandAdManager.instance.ShowAd("startAd");

        Debug.Log("This is Current Level " + gamemanager.instance.getLevel()); 
        SoundManager.instance.Stop("click");

        int nextLevel = gamemanager.instance.getLevel() + 1;

        if (nextLevel > 7)
        {
            nextLevel = 0; // Reset if exceeding max level
        }

        gamemanager.instance.setLevel(nextLevel);
        PlayerPrefs.Save(); // Ensure PlayerPrefs is updated before loading the new scene

        SceneManager.LoadScene(nextLevel + 1);
    }

    public void btnstart()
    {

        PlayerController.IsGamestart = true;
        SoundManager.instance.Play("click");
        FindObjectOfType<PlayerController>().GmRun = true;
        startpanel.SetActive(false);
        gameplaypanel.SetActive(true);
        startcount = true;
    }

    public void btnstart2()
    {

        SoundManager.instance.Stop("start");
        SoundManager.instance.Play("click");
        FindObjectOfType<lvl2playerctr>().GmRun = true;
        startpanel.SetActive(false);
        gameplaypanel.SetActive(true);
        startcount = true;
    }

    public void btnstartlvl5()
    {

      /*  SoundManager.instance.Stop("start");
        SoundManager.instance.Play("click");*/
        startpanel.SetActive(false);
        gameplaypanel.SetActive(true);
        startcount = true;
    }

    IEnumerator timeconting()
    {
        while (t >= 0)
        {
            yield return new WaitForSeconds(1f);
            SoundManager.instance.Play("click");
        }

    }
    public void TotalScoreUI()
    {
        ScoreUI_Count.GetComponent<TextMeshProUGUI>().text=GrandAdManager.TotalScore.ToString();
    }
}
