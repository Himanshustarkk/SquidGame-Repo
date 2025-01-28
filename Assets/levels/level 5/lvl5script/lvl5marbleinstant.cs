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
        }

        if(lose && !onetime)
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
        FindObjectOfType<UiManager>().startcount = false;
        SoundManager.instance.Play("lose");
        yield return new WaitForSeconds(5f);
        //AdManager.Instance.ShowVideo();
       // Gley.MobileAds.Internal.MobileAdsExample.Instance.ShowRewardedVideo();
        FindObjectOfType<UiManager>().losepanel.SetActive(true);
    }
}
