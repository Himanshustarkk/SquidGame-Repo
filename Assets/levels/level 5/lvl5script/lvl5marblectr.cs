using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lvl5marblectr : MonoBehaviour
{
    Rigidbody rig;
    Vector3 v;
    bool stop,onetime,timerone,finish;
    
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        v = new Vector3(0, 0, 0);
        rig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(stop && rig.velocity.z<=0 && !onetime )
        {
            onetime = true;
            gameObject.tag = "Untagged";
            if(FindObjectOfType<lvl5marbleinstant>().i < 5 && !finish)
            {
                FindObjectOfType<lvl5marbleinstant>().insmarble = false;
                FindObjectOfType<barpwr>().stop = false;
            }
            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="ground" && !timerone)
        {
            timerone = true;
            stop = true;
            //FindObjectOfType<lvl5cammove>().followmbl = false;
            StartCoroutine(stopmarble());
            if(FindObjectOfType<lvl5marbleinstant>().i==5 && !finish)
            {
                FindObjectOfType<lvl5marbleinstant>().lose = true;
            }
        }
    }

    

    IEnumerator stopmarble()
    {
        yield return new WaitForSeconds(2f);
        rig.velocity =new Vector3(0,0,0);
        rig.drag = 50;
        rig.velocity =Vector3.zero;
        print("entre 000000000000000");
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
    }

    IEnumerator winplayer()
    {

        
        FindObjectOfType<UiManager>().wineffet.SetActive(true);
        FindObjectOfType<UiManager>().startcount=false;
        SoundManager.instance.Play("win");
        yield return new WaitForSeconds(3f);
       // Gley.MobileAds.Internal.MobileAdsExample.Instance.ShowInterstitial();
        FindObjectOfType<UiManager>().winpanel.SetActive(true);

    }
}
