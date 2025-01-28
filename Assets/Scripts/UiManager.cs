using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;
    public Text LevelText,secondmessage;
    public bool skinEnter;
    public GameObject red, green;
    public GameObject ingamepanel;
    public GameObject playerSelectionPanel;
    public GameObject startpanel,gameplaypanel,losepanel,winpanel;
    public GameObject wineffet;
    public Text timershow;
    public float t = 65;
    public bool startcount;
    public GameObject bloodeefect;
    float ts = 1;
    public TextMeshProUGUI txtpr;

    public Color clr;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {

        //SoundManager.instance.Play("start");
        // Advertisements.Instance.Initialize();
      // Gley.MobileAds.Internal.MobileAdsExample.Instance.ShawBanner();
        //LevelText.text = "Level " + (gamemanager.instance.getLevel() + 1);
    }

    private void Update()
    {
        if(startcount)
        {
            t -= Time.deltaTime;
            ts -= Time.deltaTime;
            if(ts<=0 && t>=0)
            {
                ts = 1;
                SoundManager.instance.Play("click");
            }
            int a = (int)t;
            //StartCoroutine(timeconting());`
            if(a>=0)
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
        gamemanager.instance.setlive(gamemanager.instance.getlive()+1);
        if(gamemanager.instance.getlive()>2)
        {
            gamemanager.instance.setLevel(0);
            gamemanager.instance.setlive(0);
            SceneManager.LoadScene(1);
        }
        else
        {
            SceneManager.LoadScene(gamemanager.instance.getLevel() + 1);
        }
        SoundManager.instance.stop("click");
        // sound
        //SoundManager.instance.Play("click");
        //gamemanager.instance.setLevel(gamemanager.instance.getLevel()-1);
        //SceneManager.LoadScene(gamemanager.instance.getLevel()+1);
    }

    public void nextlvl()
    {
        
        SoundManager.instance.stop("click");
        gamemanager.instance.setLevel(gamemanager.instance.getLevel() + 1);
        if (gamemanager.instance.getLevel()+1>7)
        {
            gamemanager.instance.setLevel(0);
        }

        Debug.Log(gamemanager.instance.getLevel());
        SceneManager.LoadScene(gamemanager.instance.getLevel() + 1);
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

        SoundManager.instance.stop("start");
        SoundManager.instance.Play("click");
        FindObjectOfType<lvl2playerctr>().GmRun = true;
        startpanel.SetActive(false);
        gameplaypanel.SetActive(true);
        startcount = true;
    }

    public void btnstartlvl5()
    {

        SoundManager.instance.stop("start");
        SoundManager.instance.Play("click");
        startpanel.SetActive(false);
        gameplaypanel.SetActive(true);
        startcount = true;
    }

    IEnumerator timeconting()
    {
        while (t>=0)
        {
            yield return new WaitForSeconds(1f);
            SoundManager.instance.Play("click");
        }
        
    }
}
