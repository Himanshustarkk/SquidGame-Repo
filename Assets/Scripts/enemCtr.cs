using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class enemCtr : MonoBehaviour
{
    PlayerController pc;
    public float mytimer;
    public bool onetime, firsttime, endcount, startturn, animcor;
    public int i;
    public float a = 1;
    float timestage;
    // Start is called before the first frame update
    void Start()
    {
        pc = FindObjectOfType<PlayerController>();
        timestage = mytimer;
    }

    // Update is called once per frame
    void Update()
    {
        if(pc.GmRun)
        {
            if(!firsttime)
            {
                //FindObjectOfType<UiManager>().green.SetActive(true);
                //FindObjectOfType<UiManager>().red.SetActive(false);
                if (i<5)
                {
                    i++;
                }
                
                firsttime = true;
                SoundManager.instance.sPlay("enem",a);
                if(a<2)
                {
                    a = 1 + (0.2f * i);
                }
                
            }
            if(mytimer>=0 && !endcount)
            {

                FindObjectOfType<UiManager>().red.GetComponent<Image>().fillAmount += 1 / timestage * Time.deltaTime;
                mytimer -= Time.deltaTime;
                

            }

            if(mytimer<0 && !startturn)
            {
                endcount = true;
                
                if(transform.eulerAngles.y> 181f || transform.eulerAngles.y ==0)
                {
                    transform.eulerAngles = new Vector3(0, transform.eulerAngles.y - 75*i * Time.deltaTime, 0);
                }
                if(!animcor)
                {
                    StartCoroutine(singagain());
                }
            }


            
             
            if (transform.eulerAngles.y < 350f && startturn)
            {
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 75 *i * Time.deltaTime, 0);
            }

            if(transform.eulerAngles.y >= 349f && startturn)
            {
                timestage=mytimer = 4.7f/a;
                startturn = false;
                firsttime = false;
                endcount = false;
                animcor = false;
                FindObjectOfType<UiManager>().green.SetActive(true);
                FindObjectOfType<UiManager>().red.GetComponent<Image>().fillAmount = 0;
            }

            
        }
    }


    IEnumerator singagain()
    {
        animcor = true;
        //FindObjectOfType<UiManager>().green.SetActive(false);
        //FindObjectOfType<UiManager>().red.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        SoundManager.instance.Play("search");
        yield return new WaitForSeconds(3f);
        startturn = true;
        
    }
}
