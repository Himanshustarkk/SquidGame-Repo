using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemfight : MonoBehaviour
{
    public Transform mnplayer;
    public bool kick,fight,die,win;
    // Start is called before the first frame update
    void Start()
    {
        mnplayer = FindObjectOfType<endplayerctr>().GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(fight)
        {
            kicking();
        }
        else if(die)
        {
            kick=true;
            GetComponent<Animator>().Play("die1");
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

            mnplayer.GetChild(2).GetChild(0).GetChild(0).gameObject.GetComponent<Image>().fillAmount -= Random.Range(0.1f, 0.025f);

            yield return new WaitForSeconds(0.6f);
            kick = false;
        }

    }


}
