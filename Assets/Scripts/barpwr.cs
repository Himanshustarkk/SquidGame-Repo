using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class barpwr : MonoBehaviour
{
    Image img;
    public float powertime = 1f, timeat = 0;
    public bool starttime, backtoback, stop;
    public RectTransform rec;
    public Text coins;
    int t;
    Rigidbody rig;
    public GameObject coinspanel, rewardpanel;
    public ParticleSystem effect;
    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
        //t = Random.Range(150, 250);
        //coins.text = t.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(!stop)
        {
            if (img.fillAmount >= 0 && backtoback)
            {

                timeat -= Time.deltaTime;
                img.fillAmount = timeat / powertime;
                if (img.fillAmount <= 0f)
                {
                    backtoback = false;
                }

            }
            else if (img.fillAmount <= 1 && backtoback == false)
            {

                timeat += Time.deltaTime;
                img.fillAmount = timeat / powertime;
                if (img.fillAmount >= 1f)
                {
                    timeat = powertime;
                    backtoback = true;
                }

            }
            rec.anchoredPosition = new Vector2(img.fillAmount*660f, rec.anchoredPosition.y);
        }
        
    }

    public void multicoins()
    {
        if (GameObject.FindGameObjectWithTag("marble")!=null)
        {
            rig = GameObject.FindGameObjectWithTag("marble").GetComponent<Rigidbody>();
        }
        
        Vector3 v;
        if (!stop && !FindObjectOfType<lvl5marbleinstant>().win && !FindObjectOfType<lvl5marbleinstant>().lose)
        {
            stop = true;
            if(rec.anchoredPosition.x<264)
            {
                Random.Range(100, 150);
                v = new Vector3(Random.Range(-100, -150), 100, Random.Range(800, 1050));
                rig.isKinematic = false;
                rig.AddForce(v);
            }
            else if (rec.anchoredPosition.x >396)
            {
                v = new Vector3(Random.Range(100, 150), 100, Random.Range(800, 1050));
                rig.isKinematic = false;
                rig.AddForce(v);

            }
            else if (rec.anchoredPosition.x >= 264 && rec.anchoredPosition.x <= 396)
            {

                v = new Vector3(10, 100, 1000);
                rig.isKinematic = false;
                rig.AddForce(v);
            }
            FindObjectOfType<lvl5cammove>().followmbl = true;
            StartCoroutine(goreward());
        }
    }

    IEnumerator goreward()
    {
        
        yield return new WaitForSeconds(2f);
        
        
    }
}
